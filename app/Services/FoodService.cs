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
        protected override AbstractValidator<Food> DefaultValidator => new FoodValidator();

        public FoodService(IUnitOfWork unitOfWork, INotificator notificator, ILogger<FoodService> logger)
            : base(unitOfWork, notificator, logger) { }
    }
}