using System;
using TasteUfes.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Tasteufes.Data.Interfaces;

namespace TasteUfes.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IFoodRepository Foods { get; }
        IUserRepository Users { get; }

        int Commit();
        IDbContextTransaction BeginTransaction();
        IEntityRepository<TEntity> Repository<TEntity>() where TEntity : Entity;
    }
}
