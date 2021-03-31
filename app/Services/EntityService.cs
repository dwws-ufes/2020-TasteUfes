using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using FluentValidation;
using FluentValidation.Results;
using TasteUfes.Models;
using TasteUfes.Data.Interfaces;
using TasteUfes.Services.Interfaces;
using TasteUfes.Services.Notifications;

namespace TasteUfes.Services
{
    public abstract class EntityService<TEntity> : IEntityService<TEntity> where TEntity : Entity
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly AbstractValidator<TEntity> DefaultValidator;
        protected readonly INotificator Notificator;
        protected readonly ILogger Logger;

        public EntityService(IUnitOfWork unitOfWork, AbstractValidator<TEntity> defaultValidator, INotificator notificator, ILogger<EntityService<TEntity>> logger)
        {
            UnitOfWork = unitOfWork;
            DefaultValidator = defaultValidator;
            Notificator = notificator;
            Logger = logger;
        }

        public virtual TEntity Get(Guid id)
        {
            var entity = UnitOfWork.Repository<TEntity>().Get(id);

            if (entity == null)
            {
                Notify(NotificationType.ERROR, nameof(TEntity), $"{nameof(TEntity)} not found.");
                return null;
            }

            return entity;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return UnitOfWork.Repository<TEntity>().GetAll();
        }

        public virtual TEntity Add(TEntity entity, params string[] ruleSets)
        {
            if (!IsValid(DefaultValidator, entity, ruleSets))
                return null;

            try
            {
                entity = UnitOfWork.Repository<TEntity>().Add(entity);

                UnitOfWork.SaveChanges();

                return entity;
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                Notify(NotificationType.ERROR, typeof(TEntity).Name, $"There was an error adding {typeof(TEntity).Name}.");

                return null;
            }
        }

        public virtual void Remove(Guid id)
        {
            try
            {
                var entity = UnitOfWork.Repository<TEntity>().Get(id);

                UnitOfWork.Repository<TEntity>().Remove(entity);
                UnitOfWork.SaveChanges();
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                Notify(NotificationType.ERROR, typeof(TEntity).Name, $"There was an error removing {typeof(TEntity).Name}.");
            }
        }

        public virtual TEntity Update(TEntity entity, params string[] ruleSets)
        {
            if (!IsValid(DefaultValidator, entity, ruleSets))
                return null;

            try
            {
                entity = UnitOfWork.Repository<TEntity>().Update(entity);

                UnitOfWork.SaveChanges();

                return entity;
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                Notify(NotificationType.ERROR, typeof(TEntity).Name, $"There was an error updating {typeof(TEntity).Name}.");

                return null;
            }
        }

        public bool Exists(Guid id)
        {
            return UnitOfWork.Repository<TEntity>().Exists(id);
        }

        protected virtual void Notify(NotificationType type, string property = "", string message = "")
        {
            Notificator.Handle(new Notification(type, property, message));
        }

        public virtual void Dispose()
        {
            UnitOfWork?.Dispose();
        }

        protected virtual void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(NotificationType.ERROR, error.PropertyName, error.ErrorMessage);
            }
        }

        protected virtual bool IsValid<TV, TE>(TV validation, TE entity, params string[] ruleSets)
            where TV : AbstractValidator<TE>
            where TE : Entity
        {
            if (entity == null)
            {
                Notify(NotificationType.ERROR, typeof(TE).Name, $"{typeof(TE).Name} not found.");
                return false;
            }

            var validator = validation.Validate(entity, options => options.IncludeRuleSets(ruleSets));

            if (validator.IsValid)
                return true;

            Notify(validator);
            return false;
        }
    }
}
