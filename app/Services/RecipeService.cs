using System;
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

            return recipe;
        }

        // TODO: Criar repositories para Preparation e PreparationStep.
        public override void Remove(Guid id)
        {
            var recipe = GetDetailed(id);

            if (Notificator.HasErrors())
                return;

            using var transaction = UnitOfWork.BeginTransaction();

            try
            {
                UnitOfWork.Repository<PreparationStep>().Remove(recipe.Preparation.Steps);
                UnitOfWork.Repository<Preparation>().Remove(recipe.Preparation);
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
    }
}