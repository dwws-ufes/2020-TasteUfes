using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TasteUfes.Models;

namespace TasteUfes.Seeders
{
    public static class UserSeeder
    {
        public static void Seed<TEntity>(this ModelBuilder modelBuilder) where TEntity : User, new()
        {
            var hasher = new PasswordHasher<User>();

            modelBuilder.Entity<TEntity>().HasData(
                new TEntity
                {
                    Id = Guid.Parse("CAB6B7AB-636C-4B3F-A549-7E5284A92848"),
                    FirstName = "Zé",
                    LastName = "Gonc",
                    Username = "admin",
                    Email = "admin@tasteufes.es",
                    Password = "AQAAAAEAACcQAAAAEM4y+xpWBmcmKPIBsBOrcTJiJ5I8NyphxIhDWYNlEQRsoTwJDTWUwiDoDqecgXCKxA=="
                }
            );
        }
    }
}