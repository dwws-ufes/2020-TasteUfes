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
        TEntity Add(TEntity entity, params string[] ruleSets);
        TEntity Update(TEntity entity, params string[] ruleSets);
        void Remove(Guid id);
        bool Exists(Guid id);
    }
}
