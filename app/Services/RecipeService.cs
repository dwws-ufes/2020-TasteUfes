using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using UnitsNet;
using UnitsNet.Units;
using Microsoft.Extensions.Logging;
using TasteUfes.Data.Interfaces;
using TasteUfes.Models;
using TasteUfes.Models.Validators;
using TasteUfes.Services.Interfaces;
using TasteUfes.Services.Notifications;

namespace TasteUfes.Services
{
    public class RecipeService: EntityService<Recipe>, IRecipeService
    {
        public RecipeService(IUnitOfWork unitOfWork, RecipeValidator validator, INotificator notificator, ILogger<RecipeService> logger, IFoodService foodService)
            : base(unitOfWork, validator, notificator, logger) {
                _foodService = foodService;
             }

        // TODO: No POST, aplicar validação no campo "quantity_unit" da lista "ingredients" para não permitir que a unidade seja de volume...
        // ...quando o ingredient estiver como unidade de massa e vice-versa.

        IFoodService _foodService {get; set;}

        public Recipe GetDetailed(Guid id)
        {
            var recipe = UnitOfWork.Recipes.GetDetailed(id);

            if (recipe == null)
            {
                Notify(NotificationType.ERROR, string.Empty, $"{nameof(Recipe)} not found.");
                return null;
            }

            NutritionFacts recipeNutritionFacts = new NutritionFacts();
            List<NutritionFactsNutrients> recipeNftList = new List<NutritionFactsNutrients>();
            foreach(var ingred in recipe.Ingredients)
            {
                Food detailedFood = _foodService.Get(ingred.Food.Id);

                //Calculate food proportion based on ingredient quantity.
                double quantity = 0;
                double ingredientProportion = 0;
                switch(ingred.QuantityUnit)
                {
                            case Measures.G:
                            case Measures.MG:
                            case Measures.KG:
                                quantity = UnitConverter.ConvertByAbbreviation(Convert.ToDouble(ingred.Quantity), "Mass", Enum.GetName(typeof(Measures), ingred.QuantityUnit), "g");
                                ingredientProportion = quantity / detailedFood.NutritionFacts.ServingSize;
                                break;
                            case Measures.L:
                            case Measures.ML:
                                quantity = UnitConverter.ConvertByAbbreviation(Convert.ToDouble(ingred.Quantity), "Volume", Enum.GetName(typeof(Measures), ingred.QuantityUnit), "L");
                                ingredientProportion = quantity / detailedFood.NutritionFacts.ServingSize;
                                break;
                            case Measures.UN:
                                quantity = ingred.Quantity;
                                ingredientProportion = quantity;
                                break;
                }

                recipeNutritionFacts.DailyValue += detailedFood.NutritionFacts.DailyValue * ingredientProportion;
                recipeNutritionFacts.ServingEnergy += detailedFood.NutritionFacts.ServingEnergy * ingredientProportion;

                foreach(var nfn in detailedFood.NutritionFacts.NutritionFactsNutrients)
                {
                    NutritionFactsNutrients recipeNfn = recipeNftList.FirstOrDefault(x => x.Nutrient.Id == nfn.Nutrient.Id);
                    if(recipeNfn != null)
                    {
                        recipeNfn.AmountPerServing += nfn.AmountPerServing * ingredientProportion;
                        recipeNfn.DailyValue += nfn.DailyValue * ingredientProportion;
                    }else
                    {
                        recipeNfn = new NutritionFactsNutrients();
                        recipeNfn.AmountPerServing = nfn.AmountPerServing * ingredientProportion;
                        recipeNfn.DailyValue = nfn.DailyValue * ingredientProportion;
                        recipeNfn.AmountPerServingUnit = nfn.AmountPerServingUnit;
                        recipeNfn.Nutrient = nfn.Nutrient;

                        recipeNftList.Add(recipeNfn);
                    }
                }
            }
            recipeNutritionFacts.NutritionFactsNutrients = recipeNftList;
            recipe.NutritionFacts = recipeNutritionFacts;

            return recipe;
        }

        public override void Remove(Guid id)
        {
            var recipe = GetDetailed(id);

            if (Notificator.HasErrors())
                return;

            using var transaction = UnitOfWork.BeginTransaction();

            try
            {
                UnitOfWork.PreparationSteps.Remove(recipe.Preparation.Steps);
                UnitOfWork.Preparations.Remove(recipe.Preparation);
                UnitOfWork.Ingredients.Remove(recipe.Ingredients);
                UnitOfWork.Recipes.Remove(recipe);

                UnitOfWork.SaveChanges();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                Logger.LogError(e.Message);
                Notify(NotificationType.ERROR, string.Empty, $"There was an error removing {nameof(Recipe)}.");
            }
        }

        public Recipe RecalculateRecipePerServings(Guid id, int servings)
        {
            var recipe = Get(id);

            if (Notificator.HasErrors())
                return null;

            // TODO: Balancear os ingredientes da receita e recalcular a tabela nutricional (não é pra salvar no banco).

            return recipe;
        }

        public Recipe CalculateAnonymousRecipe(Recipe recipe)
        {
            // TODO: Dado uma receita qualquer com seus ingredientes, calcular a tabela nutricional dela.
            // Obs: Será necessário consultar o banco para resgatar os alimentos, visto que
            // serão recebidos apenas os ids dos alimentos.
            throw new NotImplementedException();
        }

        public IEnumerable<Recipe> RecommendRecipesByIngredients(IEnumerable<Ingredient> ingredients)
        {
            // TODO: Dado um conjunto de ingredientes e seus alimentos, buscar receitas que os contenham.
            // Obs: Sobrescrevi o método "Search" do RecipeRepository para fazer buscas já com as associações
            // Recomendo que o use para facilitar a vida.
            throw new NotImplementedException();
        }
    }
}