using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TasteUfes.Data.Context.Extensions
{
    public static class VarcharMaxLengthExtension
    {
        public static void VarcharMaxLengthWhenUndefined(this ModelBuilder modelBuilder, int maxLength)
        {
            foreach (var property in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(e => e.GetProperties())
                .Where(p => p.ClrType == typeof(string)))
            {
                property.SetColumnType($"VARCHAR({property.GetMaxLength() ?? maxLength})");
            }
        }
    }
}