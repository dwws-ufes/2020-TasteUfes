using System;
using System.Collections.Generic;
using FluentValidation;
using TasteUfes.Models;

namespace TasteUfes.Services.Interfaces
{
    public interface IEntityService<TEntity> : IDisposable where TEntity : Entity
    {
        TEntity Get(Guid id);
        IEnumerable<TEntity> GetAll();
        TEntity Add(TEntity entity, AbstractValidator<TEntity> validator = null);
        TEntity Update(TEntity entity, AbstractValidator<TEntity> validator = null);
        void Remove(Guid id);
        bool Exists(Guid id);
    }
}
