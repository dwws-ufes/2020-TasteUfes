using TasteUfes.Models;

namespace TasteUfes.Services.Interfaces
{
    public interface ITokenService
    {
        Token GenerateAccessToken(User user, bool resetRefreshTokenExpiresIn = true);
        Token RefreshToken(string accessToken, string refreshToken);
    }
}