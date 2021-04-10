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

            var food = base.Update(entity, ruleSets);

            if (Notificator.HasErrors())
                return null;

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
    }
}