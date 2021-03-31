using System.ComponentModel.DataAnnotations.Schema;

namespace TasteUfes.Models
{
    [NotMapped]
    public class Token
    {
        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        public int ExpiresIn { get; set; }

        public string RefreshToken { get; set; }
    }
}