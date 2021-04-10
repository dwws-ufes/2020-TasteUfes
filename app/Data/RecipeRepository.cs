using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TasteUfes.Data.Interfaces;
using TasteUfes.Data.Context;
using TasteUfes.Models;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TasteUfes.Data
{
    public class RecipeRepository : EntityRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ApplicationDbContext context)
            : base(context) { }

        public override IEnumerable<Recipe> GetAll()
        {
            return _context.Set<Recipe>()
                .AsNoTracking()
                .Include(r => r.Preparation)
                .ThenInclude(p => p.Steps)
                .Include(r => r.User)
                .ThenInclude(r => r.Roles)
                .ToList();
        }

        public override IEnumerable<Recipe> Search(Expression<Func<Recipe, bool>> predicate)
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
                .Where(predicate)
                .AsNoTracking()
                .ToList();
        }

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