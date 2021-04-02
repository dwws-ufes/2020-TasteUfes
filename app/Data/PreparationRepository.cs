using TasteUfes.Data.Interfaces;
using TasteUfes.Data.Context;
using TasteUfes.Models;

namespace TasteUfes.Data
{
    public class PreparationRepository : EntityRepository<Preparation>, IPreparationRepository
    {
        public PreparationRepository(ApplicationDbContext context)
            : base(context) { }
    }
}