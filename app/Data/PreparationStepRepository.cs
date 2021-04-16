using TasteUfes.Data.Interfaces;
using TasteUfes.Data.Context;
using TasteUfes.Models;

namespace TasteUfes.Data
{
    public class PreparationStepRepository : EntityRepository<PreparationStep>, IPreparationStepRepository
    {
        public PreparationStepRepository(ApplicationDbContext context)
            : base(context) { }
    }
}