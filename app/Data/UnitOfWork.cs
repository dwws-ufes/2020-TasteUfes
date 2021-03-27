using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage;
using TasteUfes.Models;
using TasteUfes.Data.Interfaces;
using TasteUfes.Data.Context;
using Tasteufes.Data.Interfaces;

namespace TasteUfes.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly Dictionary<string, dynamic> _repositories;

        public IFoodRepository Foods { get; }
        public IUserRepository Users { get; }

        public UnitOfWork(ApplicationDbContext context,
            IFoodRepository foods,
            IUserRepository users)
        {
            _context = context;
            _repositories = new Dictionary<string, dynamic>();

            _repositories[nameof(Food)] = Foods = foods;
            _repositories[nameof(User)] = Users = users;
        }

        public IEntityRepository<TEntity> Repository<TEntity>() where TEntity : Entity
        {
            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IEntityRepository<TEntity>)_repositories[type];
            }

            var repositoryType = typeof(EntityRepository<>).MakeGenericType(typeof(TEntity));

            _repositories.Add(type, Activator.CreateInstance(repositoryType, _context));

            return _repositories[type];
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
