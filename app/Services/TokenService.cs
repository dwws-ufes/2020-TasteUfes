using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TasteUfes.Configurations;
using TasteUfes.Data.Context;
using TasteUfes.Models;
using TasteUfes.Services.Interfaces;
using TasteUfes.Services.Notifications;

namespace TasteUfes.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwt;
        private readonly ApplicationDbContext _context;
        private readonly INotificator _notificator;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IOptions<JwtSettings> jwtSettings, ApplicationDbContext context, INotificator notificator, ILogger<TokenService> logger)
        {
            _jwt = jwtSettings.Value;
            _context = context;
            _notificator = notificator;
            _logger = logger;
        }

        public Token GenerateAccessToken(User user, bool resetRefreshTokenExpiresIn = true)
        {
            var now = DateTime.UtcNow;

            var tokenExpiresIn = now.Add(_jwt.TokenLifetime);
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwt.SecretKey));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName)
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var accessTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = tokenExpiresIn,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var accessTokenHandler = new JwtSecurityTokenHandler();
            var accessTokenSecurity = accessTokenHandler.CreateToken(accessTokenDescriptor);
            var accessToken = accessTokenHandler.WriteToken(accessTokenSecurity);

            try
            {
                EntityEntry<Token> token = null;

                var refreshToken = System.Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                var userToken = _context.Tokens.FirstOrDefault(t => t.UserId == user.Id);

                if (userToken != null)
                {
                    userToken.AccessToken = accessToken;
                    userToken.TokenType = _jwt.TokenType;
                    userToken.AccessTokenLifetime = (int)_jwt.TokenLifetime.TotalSeconds;
                    userToken.RefreshToken = refreshToken;

                    if (resetRefreshTokenExpiresIn)
                    {
                        userToken.RefreshTokenExpiresIn = now.Add(_jwt.RefreshLifetime);
                    }

                    token = _context.Tokens.Update(userToken);
                }
                else
                {
                    token = _context.Tokens.Add(new Token
                    {
                        UserId = user.Id,
                        TokenType = _jwt.TokenType,
                        AccessToken = accessToken,
                        AccessTokenLifetime = (int)_jwt.TokenLifetime.TotalSeconds,
                        RefreshToken = refreshToken,
                        RefreshTokenExpiresIn = now.Add(_jwt.RefreshLifetime)
                    });
                }

                _context.SaveChanges();

                return token.Entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _notificator.Handle(new Notification(NotificationType.ERROR, string.Empty,
                    "An unexpected error occurred while attempting to login, please try again."));

                return null;
            }
        }

        public Token RefreshToken(string accessToken, string refreshToken)
        {
            var token = _context.Tokens.AsNoTracking()
                .Include(t => t.User)
                    .ThenInclude(u => u.Roles)
                .FirstOrDefault(t => t.AccessToken == accessToken && t.RefreshToken == refreshToken);

            if (token == null)
            {
                _notificator.Handle(new Notification(NotificationType.ERROR, string.Empty, "Invalid access or refresh token."));
                return null;
            }

            if (token.RefreshTokenExpiresIn < DateTime.UtcNow)
            {
                _notificator.Handle(new Notification(NotificationType.ERROR, string.Empty, "This refresh token has expired."));
                return null;
            }

            return GenerateAccessToken(token.User, resetRefreshTokenExpiresIn: false);
        }
    }
}
