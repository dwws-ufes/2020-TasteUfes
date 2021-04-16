using System;
using Microsoft.EntityFrameworkCore;
using TasteUfes.Models;

namespace TasteUfes.Seeders
{
    public static class RoleSeeder
    {
        public static void Seed<TEntity>(this ModelBuilder modelBuilder) where TEntity : Role, new()
        {
            modelBuilder.Entity<TEntity>().HasData(
                new TEntity
                {
                    Id = Guid.Parse("D6742FBB-18AB-451B-A736-713B63B7A108"),
                    Name = "Admin"
                }
            );
        }
    }
}