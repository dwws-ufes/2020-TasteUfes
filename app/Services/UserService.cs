using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TasteUfes.Data.Interfaces;
using TasteUfes.Models;
using TasteUfes.Models.Validators;
using TasteUfes.Services.Interfaces;
using TasteUfes.Services.Notifications;

namespace TasteUfes.Services
{
    public class UserService : EntityService<User>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, UserValidator validator, INotificator notificator, ILogger<UserService> logger)
            : base(unitOfWork, validator, notificator, logger) { }

        public override User Add(User entity, params string[] ruleSets)
        {
            var hasher = new PasswordHasher<User>();

            entity.Password = hasher.HashPassword(entity, entity.Password);

            return base.Add(entity, ruleSets);
        }
    }
}