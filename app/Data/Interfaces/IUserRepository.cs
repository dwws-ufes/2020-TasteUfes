using System;
using TasteUfes.Data.Interfaces;
using TasteUfes.Models;

namespace TasteUfes.Data.Interfaces
{
    public interface IUserRepository : IEntityRepository<User>
    {
        User GetWithRecipes(Guid id);
        User GetByUsername(string username);
    }
}