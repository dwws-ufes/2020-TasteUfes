using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasteUfes.Models
{
    public class Ingredient : Entity
    {
        [Required]
        public double Quantity { get; set; }

        [Required]
        public Measures QuantityUnit { get; set; }

        [ForeignKey("FoodId")]
        public Food Food { get; set; }
        public Guid FoodId { get; set; }
        
        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }
        public Guid RecipeId { get; set; }
    }
}