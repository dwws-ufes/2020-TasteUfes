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

        public override IEnumerable<User> GetAll()
        {
            return _context.Users.Include(u => u.Roles);
        }

        public override User Get(Guid id)
        {
            return _context.Users
                .Include(u => u.Roles)
                .Include(u => u.Tokens)
                .FirstOrDefault(u => u.Id == id);
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
                .Include(u => u.Tokens)
                .FirstOrDefault(u => u.Username == username);
        }
    }
}