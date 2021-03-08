using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TasteUfes.Models;

namespace TasteUfes.Data.Interfaces
{
    public interface IEntityRepository<TEntity> : IDisposable where TEntity : Entity
    {
        TEntity Get(Guid id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        void Remove(Guid id);
        bool Exists(Guid id);
    }
}
