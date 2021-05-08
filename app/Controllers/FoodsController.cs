using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasteUfes.Models;
using TasteUfes.Resources;
using TasteUfes.Services.Interfaces;
using TasteUfes.Services.Notifications;
using VDS.RDF;
using VDS.RDF.Nodes;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Writing;

namespace TasteUfes.Controllers
{
    public class FoodsController : EntityApiControllerV1<Food, FoodResource>
    {
        public FoodsController(IFoodService foodService, IMapper mapper, INotificator notificator)
            : base(foodService, mapper, notificator) { }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public override ActionResult<FoodResource> Post([FromBody] FoodResource resource)
            => base.Post(resource);

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public override ActionResult<FoodResource> Put([FromRoute] Guid id, [FromBody] FoodResource resource)
        {
            if (resource.NutritionFacts != null)
            {
                foreach (var nfn in resource.NutritionFacts.NutritionFactsNutrients)
                {
                    nfn.Nutrient = null;
                }
            }

            return base.Put(id, resource);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public override IActionResult Delete([FromRoute] Guid id)
            => base.Delete(id);

        [HttpGet("~/api/v1/nutrients")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<NutrientResource>> GetNutrients([FromServices] INutrientService nutrientService)
        {
            return Ok(Mapper.Map<IEnumerable<NutrientResource>>(nutrientService.GetAll()));
        }

        [HttpGet("ld/{foodName}")]
        public ActionResult<List<FoodResource>> GetLD([FromRoute] string foodName, [FromServices] INutrientService nutrientService)
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

            // Code-first distinct by name
            var result = sparqlEndpoint.QueryWithResultSet(query)
                .GroupBy(f => f["name"])
                .Select(f => f.First());

            if (!result.Any())
                return NotFound();

            var nutrients = nutrientService.GetAll();

            var totalFat = nutrients
                .FirstOrDefault(n => n.Name == "Total Fat");
            var carbohydrate = nutrients
                .FirstOrDefault(n => n.Name == "Carbohydrate");
            var protein = nutrients
                .FirstOrDefault(n => n.Name == "Protein");

            var foodList = new List<FoodResource>();

            foreach (var ldFood in result)
            {
                var foodResource = new FoodResource
                {
                    Name = GetStringOrDefault(ldFood["name"]),
                    NutritionFacts = new NutritionFactsResource
                    {
                        ServingSize = GetDoubleOrDefault(ldFood["servingSize"], 100),
                        ServingSizeUnit = Measures.g
                    }
                };

                var nutritionFactsNutrients = new List<NutritionFactsNutrientsResource>();

                nutritionFactsNutrients.Add(new NutritionFactsNutrientsResource
                {
                    AmountPerServing = GetDoubleOrDefault(ldFood["fat"]),
                    AmountPerServingUnit = Measures.g,
                    NutrientId = totalFat.Id
                });

                nutritionFactsNutrients.Add(new NutritionFactsNutrientsResource
                {
                    AmountPerServing = GetDoubleOrDefault(ldFood["carbohydrate"]),
                    AmountPerServingUnit = Measures.g,
                    NutrientId = carbohydrate.Id
                });

                nutritionFactsNutrients.Add(new NutritionFactsNutrientsResource
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

        [HttpGet("ld/rdf")]
        public ActionResult GetLD([FromServices] INutrientService nutrientService, [FromServices] IConfiguration configuration)
        {
            var foods = Service.GetAll();
            var foodsUriPrefix = configuration["APP_HOST"] + configuration["APP_FOOD_PATH"];
            var nutrients = nutrientService.GetAll();

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
                var foodSbj = g.CreateUriNode(new Uri(foodsUriPrefix + food.Id));

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

            var stringWriter = StringWriter.Write(g, new RdfXmlWriter());

            return Content(stringWriter.ToString(), "text/xml", System.Text.Encoding.UTF8);
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
