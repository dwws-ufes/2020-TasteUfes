using System;

namespace TasteUfes.Configurations
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }

        public string TokenType { get; set; }

        public TimeSpan TokenLifetime { get; set; }

        public TimeSpan RefreshLifetime { get; set; }
    }
}