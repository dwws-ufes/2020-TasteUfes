using Microsoft.EntityFrameworkCore;
using TasteUfes.Models;

namespace TasteUfes.Seeders
{
    public static class NutrientSeeder
    {
        public static void Seed<TEntity>(this ModelBuilder modelBuilder) where TEntity : Nutrient, new()
        {
            modelBuilder.Entity<TEntity>().HasData(
                new TEntity
                {
                    Name = "Carbohydrate",
                    EnergyPerGram = 4,
                    DailyRecommendation = 375
                },
                new TEntity
                {
                    Name = "Protein",
                    EnergyPerGram = 4,
                    DailyRecommendation = 50
                },
                new TEntity
                {
                    Name = "Total Fat",
                    EnergyPerGram = 9,
                    DailyRecommendation = 80
                }
            );
        }
    }
}