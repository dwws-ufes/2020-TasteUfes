using System;

namespace TasteUfes.Configurations
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }

        public string TokenType { get; set; }

        public TimeSpan TokenLifeTime { get; set; }

        public TimeSpan RefreshLifeTime { get; set; }
    }
}