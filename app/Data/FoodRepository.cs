using Tasteufes.Data.Interfaces;
using TasteUfes.Data.Context;
using TasteUfes.Models;

namespace TasteUfes.Data
{
    public class FoodRepository : EntityRepository<Food>, IFoodRepository
    {
        public FoodRepository(ApplicationDbContext context)
            : base(context) { }
    }
}