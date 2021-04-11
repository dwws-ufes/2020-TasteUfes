using System;
using TasteUfes.Data.Interfaces;
using TasteUfes.Models;

namespace TasteUfes.Services.Interfaces
{
    public interface IUserService : IEntityService<User>
    {
        User UpdatePassword(Guid id, string oldPassword, string newPassword);

        User GetByUsername(string username);

        User GetByCredentials(string username, string password);
    }
}