using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Microsoft.Extensions.Logging;
using TasteUfes.Data.Interfaces;
using TasteUfes.Models;
using TasteUfes.Models.Validators;
using TasteUfes.Services.Interfaces;
using TasteUfes.Services.Notifications;

namespace TasteUfes.Services
{
    public class RecipeService : EntityService<Recipe>, IRecipeService
    {
        public RecipeService(IUnitOfWork unitOfWork, RecipeValidator validator, INotificator notificator, ILogger<RecipeService> logger)
            : base(unitOfWork, validator, notificator, logger) { }

        public Recipe GetDetailed(Guid id)
        {
            var recipe = UnitOfWork.Recipes.GetDetailed(id);

            if (recipe == null)
            {
                Notify(NotificationType.ERROR, string.Empty, $"{nameof(Recipe)} not found.");
                return null;
            }

            // TODO: Calcular a tabela nutricional da receita baseado nos ingredientes e seus alimentos.
            recipe.NutritionFacts = new NutritionFacts();

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