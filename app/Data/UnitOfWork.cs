using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage;
using TasteUfes.Models;
using TasteUfes.Data.Interfaces;
using TasteUfes.Data.Context;

namespace TasteUfes.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly Dictionary<string, dynamic> _repositories;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<string, dynamic>();
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

        public int SaveChanges()
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
