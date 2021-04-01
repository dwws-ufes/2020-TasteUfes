using TasteUfes.Data.Interfaces;
using TasteUfes.Data.Context;
using TasteUfes.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TasteUfes.Data
{
    public class UserRepository : EntityRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context)
            : base(context) { }

        public override User Add(User entity)
        {
            if (entity.Roles != null)
                _context.AttachRange(entity.Roles);

            return base.Add(entity);
        }

        public override User Get(Guid id)
        {
            return _context.Users
                .Include(u => u.Roles)
                .FirstOrDefault(u => u.Id == id);
        }

        // TODO: Transação
        public override void Remove(User user)
        {
            var roleAssocs = _context.Set<UserRole>().Where(ur => ur.UserId == user.Id);

            _context.Set<UserRole>().RemoveRange(roleAssocs);
            _context.Set<User>().Remove(user);
        }

        public override void Remove(IEnumerable<User> users)
        {
            foreach (var user in users) Remove(user);
        }

        public User GetWithRecipes(Guid id)
        {
            return _context.Users
                .Include(u => u.Roles)
                .Include(u => u.Recipes)
                    .ThenInclude(r => r.Preparation)
                .FirstOrDefault(u => u.Id == id);
        }

        public User GetByUsername(string username)
        {
            return _context.Users
                .Include(u => u.Roles)
                .FirstOrDefault(u => u.Username == username);
        }
    }
}