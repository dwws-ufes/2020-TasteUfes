using System;
using System.Collections.Generic;
using System.Linq;
using UnitsNet;
using Microsoft.Extensions.Logging;
using TasteUfes.Data.Interfaces;
using TasteUfes.Models;
using TasteUfes.Models.Validators;
using TasteUfes.Services.Interfaces;
using TasteUfes.Services.Notifications;
using FluentValidation;

namespace TasteUfes.Services
{
    public class RecipeService : EntityService<Recipe>, IRecipeService
    {
        private readonly static string G = Enum.GetName(typeof(Measures), Measures.g);
        private readonly static string L = Enum.GetName(typeof(Measures), Measures.L);
        private readonly IFoodService _foodService;

        public RecipeService(IFoodService foodService,
            IUnitOfWork unitOfWork, RecipeValidator validator, INotificator notificator, ILogger<RecipeService> logger)
            : base(unitOfWork, validator, notificator, logger)
        {
            _foodService = foodService;
        }

        public Recipe GetDetailed(Guid id)
        {
            var recipe = UnitOfWork.Recipes.GetDetailed(id);

            if (recipe == null)
            {
                Notify(NotificationType.ERROR, string.Empty, $"{nameof(Recipe)} not found.");
                return null;
            }

            return CalculateAnonymousRecipe(recipe);
        }

        public override Recipe Add(Recipe entity, params string[] ruleSets)
        {
            ruleSets.Append("MassVolumeConflict");
            ruleSets.Append("default");

            var add = base.Add(entity, ruleSets);

            if (Notificator.HasErrors())
                return null;

            return GetDetailed(add.Id);
        }

        public override Recipe Update(Recipe entity, params string[] ruleSets)
        {
            ruleSets.Append("MassVolumeConflict");
            ruleSets.Append("default");

            var recipe = GetDetailed(entity.Id);

            using var transaction = UnitOfWork.BeginTransaction();

            try
            {
                // Remove modo de preparo e ingredientes
                if (recipe.Preparation != null)
                {
                    UnitOfWork.PreparationSteps.Remove(recipe.Preparation.Steps);
                    UnitOfWork.Preparations.Remove(recipe.Preparation);
                }

                UnitOfWork.Ingredients.Remove(recipe.Ingredients);
                UnitOfWork.SaveChanges();

                // Cria novo modo de preparo e ingredientes
                if (entity.Preparation != null)
                    recipe.Preparation = UnitOfWork.Preparations.Add(entity.Preparation);

                if (entity.Ingredients != null)
                {
                    foreach (var ingredient in entity.Ingredients)
                    {
                        recipe.Ingredients.Add(UnitOfWork.Ingredients.Add(ingredient));
                    }
                }

                recipe.Name = entity.Name;
                recipe.Servings = entity.Servings;

                // Valida e atualiza
                recipe = base.Update(recipe, ruleSets);

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
                Notify(NotificationType.ERROR, string.Empty, $"There was an error removing {nameof(Recipe)}.");

                return null;
            }

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
                if (recipe.Preparation != null)
                {
                    UnitOfWork.PreparationSteps.Remove(recipe.Preparation.Steps);
                    UnitOfWork.Preparations.Remove(recipe.Preparation);
                }

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
            var recipe = GetDetailed(id);

            if (Notificator.HasErrors())
                return null;

            var proportion = ((double)servings) / recipe.Servings;

            foreach (var ingredient in recipe.Ingredients)
            {
                ingredient.Quantity *= proportion;
            }

            recipe.NutritionFacts.ServingEnergy *= proportion;
            recipe.NutritionFacts.DailyValue *= proportion;
            recipe.NutritionFacts.ServingSize = servings;

            foreach (var nfn in recipe.NutritionFacts.NutritionFactsNutrients)
            {
                nfn.AmountPerServing *= proportion;
                nfn.DailyValue *= proportion;
            }

            return recipe;
        }

        public Recipe CalculateAnonymousRecipe(Recipe recipe)
        {
            var recipeNutritionFacts = new NutritionFacts();
            var recipeNutritionFactsRows = new List<NutritionFactsNutrients>();

            foreach (var ingredient in recipe.Ingredients)
            {
                var food = _foodService.Get(ingredient.FoodId);

                if (Notificator.HasErrors())
                    return null;

                if (food.NutritionFacts == null)
                    continue;

                ingredient.Food = food;

                if (!IsValid(new IngredientValidator(), ingredient, "MassVolumeConflict"))
                    return null;

                var foodServingSize = food.NutritionFacts.ServingSize;
                var foodServingSizeUnit = food.NutritionFacts.ServingSizeUnit;

                var quantity = 0.0;
                var ingredientProportion = 0.0;

                switch (ingredient.QuantityUnit)
                {
                    case Measures.g:
                    case Measures.mg:
                    case Measures.kg:
                        quantity = UnitConverter.ConvertByAbbreviation(ingredient.Quantity, "Mass", Enum.GetName(typeof(Measures), ingredient.QuantityUnit), G);
                        foodServingSize = UnitConverter.ConvertByAbbreviation(foodServingSize, "Mass", Enum.GetName(typeof(Measures), foodServingSizeUnit), G);
                        ingredientProportion = quantity / foodServingSize;
                        break;

                    case Measures.L:
                    case Measures.ml:
                        quantity = UnitConverter.ConvertByAbbreviation(ingredient.Quantity, "Volume", Enum.GetName(typeof(Measures), ingredient.QuantityUnit), L);
                        foodServingSize = UnitConverter.ConvertByAbbreviation(foodServingSize, "Volume", Enum.GetName(typeof(Measures), foodServingSizeUnit), L);
                        ingredientProportion = quantity / foodServingSize;
                        break;

                    case Measures.un:
                        quantity = ingredient.Quantity;
                        ingredientProportion = quantity;
                        break;

                    default:
                        Notify(NotificationType.ERROR, string.Empty, "Invalid unit of measurement.");
                        return null;
                }

                ingredientProportion /= recipe.Servings;

                recipeNutritionFacts.DailyValue += food.NutritionFacts.DailyValue * ingredientProportion;
                recipeNutritionFacts.ServingEnergy += food.NutritionFacts.ServingEnergy * ingredientProportion;

                foreach (var nfn in food.NutritionFacts.NutritionFactsNutrients)
                {
                    var recipeNfn = recipeNutritionFactsRows.FirstOrDefault(x => x.Nutrient.Id == nfn.Nutrient.Id);

                    if (recipeNfn != null)
                    {
                        recipeNfn.AmountPerServing += nfn.AmountPerServing * ingredientProportion;
                        recipeNfn.DailyValue += nfn.DailyValue * ingredientProportion;
                    }
                    else
                    {
                        recipeNutritionFactsRows.Add(new NutritionFactsNutrients
                        {
                            AmountPerServing = nfn.AmountPerServing * ingredientProportion,
                            DailyValue = nfn.DailyValue * ingredientProportion,
                            AmountPerServingUnit = nfn.AmountPerServingUnit,
                            Nutrient = nfn.Nutrient,
                            NutrientId = nfn.NutrientId
                        });
                    }
                }
            }

            recipe.NutritionFacts = recipeNutritionFacts;
            recipe.NutritionFacts.NutritionFactsNutrients = recipeNutritionFactsRows;

            return recipe;
        }

        public IEnumerable<Recipe> RecommendRecipesByFoods(IEnumerable<Food> foods)
        {
            var foodsId = foods.Select(f => f.Id);

            var recipes = UnitOfWork.Recipes
                .Search(r => r.Ingredients.All(i => foodsId.Contains(i.FoodId)));

            return recipes;
        }
    }
}