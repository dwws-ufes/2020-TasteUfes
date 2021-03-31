using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TasteUfes.Configurations;
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

        public override User Add(User user, params string[] ruleSets)
        {
            var validator = new InlineValidator<User>();

            validator.RuleFor(u => u.Username)
                .Must(usn => !UnitOfWork.Users.Search(u => u.Username == usn).Any())
                .WithMessage("Already exists an user with this username.");

            validator.RuleFor(u => u.Email)
                .Must(usn => !UnitOfWork.Users.Search(u => u.Email == usn).Any())
                .WithMessage("Already exists an user with this email.");

            if (!IsValid(validator, user))
                return null;

            var hasher = new PasswordHasher<User>();

            user.Password = hasher.HashPassword(user, user.Password);

            if (!IsValid(DefaultValidator, user, ruleSets))
                return null;

            try
            {
                user = UnitOfWork.Users.Add(user);

                UnitOfWork.SaveChanges();

                return user;
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                Notify(NotificationType.ERROR, nameof(User), $"There was an error adding {nameof(User)}.");

                return null;
            }
        }

        public override User Update(User user, params string[] ruleSets)
        {
            var validator = new InlineValidator<User>();

            validator.RuleFor(u => u.Username)
                .Must(usn => !UnitOfWork.Users.Search(u => u.Username == usn && u.Id != user.Id).Any())
                .WithMessage("Already exists an user with this username.");

            validator.RuleFor(u => u.Email)
                .Must(usn => !UnitOfWork.Users.Search(u => u.Email == usn && u.Id != user.Id).Any())
                .WithMessage("Already exists an user with this email.");

            if (!IsValid(validator, user))
                return null;

            var persisted = base.Get(user.Id);

            if (Notificator.HasErrors())
                return null;

            // Não permite atualizar a senha.
            user.Password = persisted.Password;

            return base.Update(user, ruleSets);
        }

        public override void Remove(Guid id)
        {
            if (UnitOfWork.Recipes.Search(r => r.UserId == id).Any())
            {
                Notify(NotificationType.ERROR, nameof(User), $"It's not possible to remove a {nameof(User)} with recipes");
                return;
            }

            base.Remove(id);
        }

        public User UpdatePassword(Guid id, string newPassword)
        {
            var user = base.Get(id);

            if (Notificator.HasErrors())
                return null;

            var hasher = new PasswordHasher<User>();

            user.Password = hasher.HashPassword(user, user.Password);

            return base.Update(user, "default");
        }

        public User GetByUsername(string username)
        {
            var user = UnitOfWork.Users.GetByUsername(username);

            if (user == null)
            {
                Notify(NotificationType.ERROR, nameof(User), $"{nameof(User)} not found.");
                return null;
            }

            return user;
        }

        public User GetByCredentials(string username, string password)
        {
            var hasher = new PasswordHasher<User>();
            var user = UnitOfWork.Users.GetByUsername(username);

            if (user == null)
            {
                Notify(NotificationType.ERROR, string.Empty, "Username or password is invalid.");
                return null;
            }

            var verification = hasher.VerifyHashedPassword(user, user.Password, password);

            if (verification == PasswordVerificationResult.Failed)
            {
                Notify(NotificationType.ERROR, string.Empty, "Username or password is invalid.");
                return null;
            }

            return user;
        }
    }
}
