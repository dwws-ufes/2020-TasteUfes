using TasteUfes.Data.Context;
using TasteUfes.Data.Interfaces;
using TasteUfes.Models;

namespace TasteUfes.Data
{
    public class IngredientRepository : EntityRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(ApplicationDbContext context)
            : base(context) { }
    }
}