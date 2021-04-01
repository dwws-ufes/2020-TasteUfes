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
    }
}