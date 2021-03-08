using Microsoft.EntityFrameworkCore;
using TasteUfes.Models;

namespace TasteUfes.Seeders
{
    public static class RoleSeeder
    {
        public static void Seed<TEntity>(this ModelBuilder modelBuilder) where TEntity : Role, new()
        {
            modelBuilder.Entity<TEntity>().HasData(
                new TEntity { Name = "Admin" }
            );
        }
    }
}