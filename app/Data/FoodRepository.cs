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
    public class FoodRepository : EntityRepository<Food>, IFoodRepository
    {
        public FoodRepository(ApplicationDbContext context)
            : base(context) { }

        public override IEnumerable<Food> GetAll()
        {
            return _context.Foods
                .AsNoTracking()
                .Include(f => f.NutritionFacts)
                .ThenInclude(n => n.NutritionFactsNutrients)
                .ToList();
        }

        public override Food Get(Guid id)
        {
            return _context.Foods
                .Include(f => f.NutritionFacts)
                .ThenInclude(n => n.NutritionFactsNutrients)
                .ThenInclude(n => n.Nutrient)
                .FirstOrDefault(f => f.Id == id);
        }

        public override IEnumerable<Food> Search(Expression<Func<Food, bool>> predicate)
        {
            return _context.Set<Food>()
                .AsNoTracking()
                .Include(f => f.NutritionFacts)
                .ThenInclude(n => n.NutritionFactsNutrients)
                .ThenInclude(n => n.Nutrient)
                .Where(predicate)
                .ToList();
        }
    }
}