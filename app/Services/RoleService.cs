using FluentValidation;
using Microsoft.Extensions.Logging;
using TasteUfes.Data.Interfaces;
using TasteUfes.Models;
using TasteUfes.Models.Validators;
using TasteUfes.Services.Notifications;

namespace TasteUfes.Services.Interfaces
{
    public class RoleService : EntityService<Role>, IRoleService
    {
        public RoleService(IUnitOfWork unitOfWork, RoleValidator validator, INotificator notificator, ILogger<EntityService<Role>> logger)
            : base(unitOfWork, validator, notificator, logger) { }
    }
}