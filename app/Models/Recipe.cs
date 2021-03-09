using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasteUfes.Models
{
    public class Recipe : Entity
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        public int Servings { get; set; }

        public Preparation Preparation { get; set; }

        [InverseProperty("Recipe")]
        public IEnumerable<Ingredient> Ingredients { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public Guid UserId { get; set; }

        [NotMapped]
        public NutritionFacts NutritionFacts { get; set; }
    }
}