using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TasteUfes.Data.Interfaces;
using TasteUfes.Data.Context;
using TasteUfes.Models;

namespace TasteUfes.Data
{
    public class RecipeRepository : EntityRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ApplicationDbContext context)
            : base(context) { }

        public Recipe GetDetailed(Guid id)
        {
            return _context.Set<Recipe>()
                .Include(r => r.Preparation)
                    .ThenInclude(r => r.Steps)
                .Include(r => r.Ingredients)
                    .ThenInclude(r => r.Food)
                        .ThenInclude(r => r.NutritionFacts)
                            .ThenInclude(r => r.NutritionFactsNutrients)
                                .ThenInclude(r => r.Nutrient)
                .Include(r => r.User)
                    .ThenInclude(r => r.Roles)
                .FirstOrDefault(r => r.Id == id);
        }
    }
}