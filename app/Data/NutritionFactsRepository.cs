using TasteUfes.Data.Context;
using TasteUfes.Data.Interfaces;
using TasteUfes.Models;

namespace TasteUfes.Data
{
    public class NutritionFactsRepository : EntityRepository<NutritionFacts>, INutritionFactsRepository
    {
        public NutritionFactsRepository(ApplicationDbContext context)
            : base(context) { }
    }
}