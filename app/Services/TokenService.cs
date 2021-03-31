using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TasteUfes.Configurations;
using TasteUfes.Data.Interfaces;
using TasteUfes.Models;
using TasteUfes.Services.Interfaces;

namespace TasteUfes.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwt;
        private readonly IUnitOfWork _unitOfWork;

        public TokenService(IOptions<JwtSettings> jwtSettings, IUnitOfWork unitOfWork)
        {
            _jwt = jwtSettings.Value;
            _unitOfWork = unitOfWork;
        }

        public Token GenerateAccessToken(User user)
        {
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

            var secretKey = Encoding.ASCII.GetBytes(_jwt.SecretKey);
            var securityKey = new SymmetricSecurityKey(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(_jwt.ExpiresIn),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            return new Token
            {
                AccessToken = accessToken,
                TokenType = _jwt.TokenType,
                ExpiresIn = _jwt.ExpiresIn,
                RefreshToken = "no time bro"
            };
        }
    }
}