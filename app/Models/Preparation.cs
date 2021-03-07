using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasteUfes.Models
{
    public class Preparation : Entity
    {
        [Required]
        public TimeSpan PreparationTime { get; set; }

        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }
        public Guid RecipeId { get; set; }

        [InverseProperty("Preparation")]
        public IEnumerable<PreparationStep> Steps { get; set; }
    }
}