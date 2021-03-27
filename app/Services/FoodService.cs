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
    }
}