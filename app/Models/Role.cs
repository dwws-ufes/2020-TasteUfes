using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TasteUfes.Models
{
    public class Role : Entity
    {
        [Required]
        [StringLength(16)]
        public string Name { get; set; }

        // Fluent API : Many-to-Many
        public ICollection<User> Users { get; set; }
    }
}