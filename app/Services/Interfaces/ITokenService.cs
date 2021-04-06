using TasteUfes.Models;

namespace TasteUfes.Services.Interfaces
{
    public interface ITokenService
    {
        Token GenerateAccessToken(User user, bool updateRefreshToken = true);
        Token RefreshToken(string accessToken, string refreshToken);
    }
}