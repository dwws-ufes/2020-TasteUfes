using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasteUfes.Models
{
    public class User : Entity
    {
        [Required]
        [StringLength(128)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(128)]
        public string LastName { get; set; }

        [Required]
        [StringLength(16)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        // Fluent API : Many-to-Many
        public IEnumerable<Role> Roles { get; set; }

        [InverseProperty("Users")]
        public IEnumerable<Recipe> Recipes { get; set; }
    }
}