using TasteUfes.Data.Interfaces;
using TasteUfes.Data.Context;
using TasteUfes.Models;

namespace TasteUfes.Data
{
    public class RoleRepository : EntityRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context)
            : base(context) { }
    }
}