using System;
using System.Linq;
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
                user.Roles = user.Roles
                    .Select(r => UnitOfWork.Roles.Get(r.Id))
                    .ToList();

                user = UnitOfWork.Users.Add(user);

                UnitOfWork.SaveChanges();

                return user;
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                Notify(NotificationType.ERROR, string.Empty, $"There was an error adding {nameof(User)}.");

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

            var persisted = Get(user.Id);

            if (Notificator.HasErrors())
                return null;

            persisted.Email = user.Email;
            persisted.FirstName = user.FirstName;
            persisted.LastName = user.LastName;
            persisted.Username = user.Username;
            persisted.Roles = user.Roles
                .Select(r => UnitOfWork.Roles.Get(r.Id))
                .ToList();

            return base.Update(persisted, ruleSets);
        }

        public override void Remove(Guid id)
        {
            if (UnitOfWork.Recipes.Search(r => r.UserId == id).Any())
            {
                Notify(NotificationType.ERROR, string.Empty, $"It's not possible to remove a {nameof(User)} with recipes");
                return;
            }

            var user = Get(id);

            if (Notificator.HasErrors())
                return;

            using var transaction = UnitOfWork.BeginTransaction();

            try
            {
                user.Roles.Clear();
                UnitOfWork.SaveChanges();

                UnitOfWork.Users.Remove(user);
                UnitOfWork.SaveChanges();

                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                Logger.LogError(e.Message);
                Notify(NotificationType.ERROR, string.Empty, $"There was an error removing {nameof(User)}.");
            }
        }

        public User UpdatePassword(Guid id, string oldPassword, string newPassword)
        {
            var user = UnitOfWork.Users.Get(id);

            if (Notificator.HasErrors())
                return null;

            var hasher = new PasswordHasher<User>();
            var verify = hasher.VerifyHashedPassword(user, user.Password, oldPassword);

            if (PasswordVerificationResult.Failed == verify)
            {
                Notify(NotificationType.ERROR, string.Empty, "Old password is invalid.");
                return null;
            }

            user.Password = hasher.HashPassword(user, newPassword);

            return base.Update(user, "default");
        }

        public User GetByUsername(string username)
        {
            var user = UnitOfWork.Users.GetByUsername(username);

            if (user == null)
            {
                Notify(NotificationType.ERROR, string.Empty, $"{nameof(User)} not found.");
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
