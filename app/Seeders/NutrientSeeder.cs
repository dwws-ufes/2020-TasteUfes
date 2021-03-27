using System;
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
                    Id = Guid.Parse("2B2DD419-C6B4-49CC-9BE8-50992E91F36C"),
                    Name = "Carbohydrate",
                    EnergyPerGram = 4,
                    DailyRecommendation = 375
                },
                new TEntity
                {
                    Id = Guid.Parse("829E1EB9-5EEA-4856-8906-74CFF3B95CB1"),
                    Name = "Protein",
                    EnergyPerGram = 4,
                    DailyRecommendation = 50
                },
                new TEntity
                {
                    Id = Guid.Parse("DB02FBBA-A1BB-4BF7-8411-69412B446F50"),
                    Name = "Total Fat",
                    EnergyPerGram = 9,
                    DailyRecommendation = 80
                }
            );
        }
    }
}