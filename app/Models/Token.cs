using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasteUfes.Models
{
    public class Token
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Mapped by Fluent API
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Required]
        public string TokenType { get; set; }

        [Required]
        public string AccessToken { get; set; }

        [Required]
        public int AccessTokenLifetime { get; set; }

        [Required]
        public string RefreshToken { get; set; }

        [Required]
        public DateTime RefreshTokenExpiresIn { get; set; }
    }
}