using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TasteUfes.Data.Interfaces;
using TasteUfes.Models;
using TasteUfes.Models.Validators;
using TasteUfes.Services.Interfaces;
using TasteUfes.Services.Notifications;
using VDS.RDF;
using VDS.RDF.Nodes;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Writing;

namespace TasteUfes.Services
{
    public class FoodService : EntityService<Food>, IFoodService
    {
        public FoodService(IUnitOfWork unitOfWork, FoodValidator validator, INotificator notificator, ILogger<FoodService> logger)
            : base(unitOfWork, validator, notificator, logger) { }

        private static double _dailyEnergy = 2000;

        public override Food Get(Guid id)
        {
            var food = UnitOfWork.Foods.Get(id);

            if (food == null)
            {
                Notify(NotificationType.ERROR, string.Empty, $"{nameof(Food)} not found.");
                return null;
            }

            if (food.NutritionFacts == null)
                return food;

            var totalEnergy = 0.0;

            foreach (var nfn in food.NutritionFacts.NutritionFactsNutrients)
            {
                nfn.DailyValue = nfn.AmountPerServing / nfn.Nutrient.DailyRecommendation;
                totalEnergy = totalEnergy + (nfn.AmountPerServing * nfn.Nutrient.EnergyPerGram);
            }

            food.NutritionFacts.ServingEnergy = totalEnergy;
            food.NutritionFacts.DailyValue = totalEnergy / _dailyEnergy;

            return food;
        }

        public override Food Add(Food entity, params string[] ruleSets)
        {
            if (UnitOfWork.Foods.Search(f => f.Name == entity.Name).Any())
            {
                Notify(NotificationType.ERROR, "Name", "Already exists a food with this name.");
                return null;
            }

            var food = base.Add(entity, ruleSets);

            if (Notificator.HasErrors())
                return null;

            return Get(food.Id);
        }

        public override Food Update(Food entity, params string[] ruleSets)
        {
            if (UnitOfWork.Foods.Search(f => f.Name == entity.Name && f.Id != entity.Id).Any())
            {
                Notify(NotificationType.ERROR, "Name", "Already exists a food with this name.");
                return null;
            }

            var food = UnitOfWork.Foods.Get(entity.Id);

            if (food == null)
            {
                Notify(NotificationType.ERROR, string.Empty, $"{nameof(Food)} not found.");
                return null;
            }

            using var transaction = UnitOfWork.BeginTransaction();

            try
            {
                if (food.NutritionFacts != null)
                {
                    UnitOfWork.NutritionFactsNutrients.Remove(food.NutritionFacts.NutritionFactsNutrients);
                    UnitOfWork.NutritionFacts.Remove(food.NutritionFacts);
                }

                UnitOfWork.SaveChanges();

                if (entity.NutritionFacts != null)
                {
                    food.NutritionFacts = UnitOfWork.NutritionFacts.Add(entity.NutritionFacts);
                }

                food.Name = entity.Name;

                food = base.Update(food, ruleSets);

                if (Notificator.HasErrors())
                {
                    transaction.Rollback();
                    return null;
                }

                UnitOfWork.SaveChanges();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                Logger.LogError(e.Message);
                Notify(NotificationType.ERROR, string.Empty, $"There was an error updating {nameof(Food)}.");

                return null;
            }

            return Get(food.Id);
        }

        public override void Remove(Guid id)
        {
            var food = Get(id);

            if (Notificator.HasErrors())
                return;

            if (UnitOfWork.Ingredients.Search(i => i.FoodId == food.Id).Any())
            {
                Notify(NotificationType.ERROR, string.Empty, "It is not possible to delete foods that belong to recipes.");
                return;
            }

            using var transaction = UnitOfWork.BeginTransaction();

            try
            {
                if (food.NutritionFacts != null)
                {
                    UnitOfWork.NutritionFactsNutrients.Remove(food.NutritionFacts.NutritionFactsNutrients);
                    UnitOfWork.NutritionFacts.Remove(food.NutritionFacts);
                }

                UnitOfWork.Foods.Remove(food);

                UnitOfWork.SaveChanges();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                Logger.LogError(e.Message);
                Notify(NotificationType.ERROR, string.Empty, $"There was an error removing '{nameof(Food)}'.");
            }
        }

        public IEnumerable<Food> GetAllLD(string foodName)
        {
            var sparqlEndpoint = new SparqlRemoteEndpoint(new Uri("http://dbpedia.org/sparql"), "http://dbpedia.org");

            var query = $@"PREFIX dbo: <http://dbpedia.org/ontology/>
                PREFIX dbp: <http://dbpedia.org/property/>
                PREFIX foaf: <http://xmlns.com/foaf/0.1/> 
                PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>

                SELECT DISTINCT ?ingredient ?name ?fat ?protein ?carbohydrate ?thumbnail ?servingSize WHERE
                {{
                    ?ingredient a dbo:Food ; rdfs:label ?name .
                    OPTIONAL {{ ?ingredient dbp:fat ?fat . }}
                    OPTIONAL {{ ?ingredient dbp:protein ?protein . }}
                    OPTIONAL {{ ?ingredient dbp:carbohydrate ?carbohydrate . }}
                    OPTIONAL {{ ?ingredient dbo:thumbnail ?thumbnail . }}
                    OPTIONAL {{ ?ingredient dbo:servingSize ?servingSize . }}
                    FILTER (LANG(?name) = 'en')
                    FILTER (REGEX(LCASE(STR(?name)), '{foodName.ToLower()}'))
                }}
                ORDER BY (!BOUND(?fat)) ASC (!BOUND(?carbohydrate)) ASC (!BOUND(?protein)) ASC (!BOUND(?servingSize)) DESC (STR(?fat) && STR(?carbohydrate) && STR(?protein) && STR(?servingSize))
                LIMIT 200";

            var foodList = new List<Food>();

            // Code-first distinct by name
            var result = sparqlEndpoint.QueryWithResultSet(query)
                .GroupBy(f => f["name"])
                .Select(f => f.First());

            if (!result.Any())
                return foodList;

            var nutrients = UnitOfWork.Repository<Nutrient>().GetAll();

            var totalFat = nutrients
                .FirstOrDefault(n => n.Name == "Total Fat");
            var carbohydrate = nutrients
                .FirstOrDefault(n => n.Name == "Carbohydrate");
            var protein = nutrients
                .FirstOrDefault(n => n.Name == "Protein");

            foreach (var ldFood in result)
            {
                var foodResource = new Food
                {
                    Name = GetStringOrDefault(ldFood["name"]),
                    NutritionFacts = new NutritionFacts
                    {
                        ServingSize = GetDoubleOrDefault(ldFood["servingSize"], 100),
                        ServingSizeUnit = Measures.g
                    }
                };

                var nutritionFactsNutrients = new List<NutritionFactsNutrients>();

                nutritionFactsNutrients.Add(new NutritionFactsNutrients
                {
                    AmountPerServing = GetDoubleOrDefault(ldFood["fat"]),
                    AmountPerServingUnit = Measures.g,
                    NutrientId = totalFat.Id
                });

                nutritionFactsNutrients.Add(new NutritionFactsNutrients
                {
                    AmountPerServing = GetDoubleOrDefault(ldFood["carbohydrate"]),
                    AmountPerServingUnit = Measures.g,
                    NutrientId = carbohydrate.Id
                });

                nutritionFactsNutrients.Add(new NutritionFactsNutrients
                {
                    AmountPerServing = GetDoubleOrDefault(ldFood["protein"]),
                    AmountPerServingUnit = Measures.g,
                    NutrientId = protein.Id
                });

                foodResource.NutritionFacts.NutritionFactsNutrients = nutritionFactsNutrients;
                foodList.Add(foodResource);
            }

            return foodList;
        }

        private string GetStringOrDefault(INode node, string defaultValue = null)
        {
            try
            {
                return node.AsValuedNode().AsString();
            }
            catch (Exception)
            {
                if (String.IsNullOrEmpty(defaultValue))
                    return node.ToString();

                return defaultValue;
            }
        }

        private double GetDoubleOrDefault(INode node, double defaultValue = 0)
        {
            if (node == null || node.NodeType != NodeType.Literal)
                return defaultValue;

            try
            {
                return node.AsValuedNode().AsDouble();
            }
            catch (Exception)
            {
                var doubleValue = 0.0;
                var tryParse = Double.TryParse(node.AsValuedNode().AsString(), out doubleValue);

                return tryParse ? doubleValue : 0.0;
            }
        }

        public IGraph GetGraph(string foodUriPrefix)
        {
            var foods = GetAll();
            var nutrients = UnitOfWork.Repository<Nutrient>().GetAll();

            var totalFat = nutrients
                .FirstOrDefault(n => n.Name == "Total Fat");
            var carbohydrate = nutrients
                .FirstOrDefault(n => n.Name == "Carbohydrate");
            var protein = nutrients
                .FirstOrDefault(n => n.Name == "Protein");

            var g = new Graph();
            g.NamespaceMap.AddNamespace("dbp", new Uri("https://dbpedia.org/property/"));
            g.NamespaceMap.AddNamespace("dbo", new Uri("https://dbpedia.org/ontology/"));

            var t = new List<Triple>();

            foreach (var food in foods)
            {
                var foodSbj = g.CreateUriNode(new Uri(foodUriPrefix + food.Id));

                // Type
                var rdfType = g.CreateUriNode(new Uri(RdfSpecsHelper.RdfType));
                var dboFood = g.CreateUriNode("dbo:Food");
                t.Add(new Triple(foodSbj, rdfType, dboFood));

                // Name
                var rdfsLabel = g.CreateUriNode("rdfs:label");
                var foodName = g.CreateLiteralNode(food.Name);
                t.Add(new Triple(foodSbj, rdfsLabel, foodName));

                // ServingSize
                var dboServingSize = g.CreateUriNode("dbo:servingSize");
                var foodServingSize = food.NutritionFacts?.ServingSize.ToString();

                if (String.IsNullOrEmpty(foodServingSize))
                {
                    t.Add(new Triple(foodSbj, dboServingSize, g.CreateBlankNode()));
                }
                else
                {
                    var dataType = GetDataTypeMeasure(food.NutritionFacts.ServingSizeUnit);
                    var literal = g.CreateLiteralNode(foodServingSize, dataType);
                    t.Add(new Triple(foodSbj, dboServingSize, literal));
                }

                // Fat
                var dbpFat = g.CreateUriNode("dbp:fat");
                var foodFat = food.NutritionFacts?.NutritionFactsNutrients.FirstOrDefault(n => n.NutrientId == totalFat.Id);
                var foodFatStr = foodFat?.AmountPerServing.ToString();

                if (String.IsNullOrEmpty(foodFatStr))
                {
                    t.Add(new Triple(foodSbj, dbpFat, g.CreateBlankNode()));
                }
                else
                {
                    var dataType = GetDataTypeMeasure(foodFat.AmountPerServingUnit);
                    var literal = g.CreateLiteralNode(foodFatStr, dataType);
                    t.Add(new Triple(foodSbj, dbpFat, literal));
                }

                // Carbohydrate
                var dbpCarbohydrate = g.CreateUriNode("dbp:carbohydrate");
                var foodCarbohydrate = food.NutritionFacts?.NutritionFactsNutrients.FirstOrDefault(n => n.NutrientId == carbohydrate.Id);
                var foodCarbohydrateStr = foodCarbohydrate?.AmountPerServing.ToString();

                if (String.IsNullOrEmpty(foodCarbohydrateStr))
                {
                    t.Add(new Triple(foodSbj, dbpCarbohydrate, g.CreateBlankNode()));
                }
                else
                {
                    var dataType = GetDataTypeMeasure(foodCarbohydrate.AmountPerServingUnit);
                    var literal = g.CreateLiteralNode(foodCarbohydrateStr, dataType);
                    t.Add(new Triple(foodSbj, dbpCarbohydrate, literal));
                }

                // Protein
                var dbpProtein = g.CreateUriNode("dbp:protein");
                var foodProtein = food.NutritionFacts?.NutritionFactsNutrients.FirstOrDefault(n => n.NutrientId == protein.Id);
                var foodProteinStr = foodProtein?.AmountPerServing.ToString();

                if (String.IsNullOrEmpty(foodProteinStr))
                {
                    t.Add(new Triple(foodSbj, dbpProtein, g.CreateBlankNode()));
                }
                else
                {
                    var dataType = GetDataTypeMeasure(foodProtein.AmountPerServingUnit);
                    var literal = g.CreateLiteralNode(foodProteinStr, dataType);
                    t.Add(new Triple(foodSbj, dbpProtein, literal));
                }
            }

            g.Assert(t);

            return g;
        }

        private Uri GetDataTypeMeasure(Measures measure)
        {
            switch (measure)
            {
                case Measures.g:
                    return new Uri("http://dbpedia.org/datatype/gram");
                case Measures.mg:
                    return new Uri("http://dbpedia.org/datatype/milligram");
                case Measures.kg:
                    return new Uri("http://dbpedia.org/datatype/kilogram");
                case Measures.L:
                    return new Uri("http://dbpedia.org/datatype/litre");
                case Measures.ml:
                    return new Uri("http://dbpedia.org/datatype/millilitre");
                default:
                    return new Uri(string.Empty);
            }
        }
    }
}