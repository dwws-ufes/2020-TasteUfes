using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TasteUfes.Data.Context.Extensions
{
    public static class DeleteBehaviorExtension
    {
        public static void OnDeleteClientSetNull(this ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }
        }
    }
}