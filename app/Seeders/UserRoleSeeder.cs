using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TasteUfes.Models;

namespace TasteUfes.Seeders
{
    public static class UserRoleSeeder
    {
        private static Guid Admin = Guid.Parse("D6742FBB-18AB-451B-A736-713B63B7A108");

        public static void Seed<TEntity>(this ModelBuilder modelBuilder) where TEntity : UserRole, new()
        {
            modelBuilder.Entity<TEntity>().HasData(
                new TEntity
                {
                    RoleId = Admin,
                    UserId = Guid.Parse("CAB6B7AB-636C-4B3F-A549-7E5284A92848")
                }
            );
        }
    }
}