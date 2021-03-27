using TasteUfes.Data.Context;
using TasteUfes.Data.Interfaces;
using TasteUfes.Models;

namespace TasteUfes.Data
{
    public class NutritionFactsNutrientsRepository : EntityRepository<NutritionFactsNutrients>, INutritionFactsNutrientsRepository
    {
        public NutritionFactsNutrientsRepository(ApplicationDbContext context)
            : base(context) { }
    }
}