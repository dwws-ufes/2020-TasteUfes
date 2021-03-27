using System;
using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tasteufes.Data.Interfaces;
using TasteUfes.Data.Context;
using TasteUfes.Models;

namespace TasteUfes.Data
{
    public class UserRepository : EntityRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context)
            : base(context) { }
    }
}