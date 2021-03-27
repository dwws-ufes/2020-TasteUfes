using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using TasteUfes.Data.Interfaces;
using TasteUfes.Models;
using TasteUfes.Models.Validators;
using TasteUfes.Services.Interfaces;
using TasteUfes.Services.Notifications;

namespace TasteUfes.Services
{
    public class FoodService : EntityService<Food>, IFoodService
    {
        public FoodService(IUnitOfWork unitOfWork, FoodValidator validator, INotificator notificator, ILogger<FoodService> logger)
            : base(unitOfWork, validator, notificator, logger) { }

        public override Food Add(Food entity, params string[] ruleSets)
        {
            if (!IsValid(DefaultValidator, entity, ruleSets))
                return null;

            try
            {
                entity = UnitOfWork.Foods.Add(entity);

                UnitOfWork.SaveChanges();

                return entity;
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                Notify(NotificationType.ERROR, nameof(Food), $"There was an error adding {nameof(Food)}.");

                return null;
            }
        }

        public override void Remove(Guid id)
        {
            var food = UnitOfWork.Foods.Get(id);

            if (UnitOfWork.Ingredients.Search(i => i.FoodId == food.Id).Any())
            {
                Notify(NotificationType.ERROR, nameof(Food), "It is not possible to delete foods that belong to recipes.");
                return;
            }

            using var transaction = UnitOfWork.BeginTransaction();

            try
            {
                if (food.NutritionFactsId.HasValue)
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
                Notify(NotificationType.ERROR, nameof(Food), $"There was an error removing '{nameof(Food)}'.");
            }
        }
    }
}