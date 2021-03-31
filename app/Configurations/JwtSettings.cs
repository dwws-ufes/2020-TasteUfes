namespace TasteUfes.Configurations
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }

        public string TokenType { get; set; }

        public int ExpiresIn { get; set; }
    }
}