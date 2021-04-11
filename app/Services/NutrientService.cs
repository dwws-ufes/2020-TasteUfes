using FluentValidation;
using Microsoft.Extensions.Logging;
using TasteUfes.Data.Interfaces;
using TasteUfes.Models;
using TasteUfes.Models.Validators;
using TasteUfes.Services.Notifications;

namespace TasteUfes.Services.Interfaces
{
    public class NutrientService : EntityService<Nutrient>, INutrientService
    {
        public NutrientService(IUnitOfWork unitOfWork, NutrientValidator validator, INotificator notificator, ILogger<EntityService<Nutrient>> logger)
            : base(unitOfWork, validator, notificator, logger) { }
    }
}