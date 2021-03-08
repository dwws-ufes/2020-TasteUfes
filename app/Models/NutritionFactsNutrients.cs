using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasteUfes.Models
{
    public class NutritionFactsNutrients : Entity
    {
        [Required]
        public double AmountPerServing { get; set; }

        [Required]
        public Measures AmountPerServingUnit { get; set; }

        [Required]
        public double DailyValue { get; set; }

        [ForeignKey("NutritionFactsId")]
        public NutritionFacts NutritionFacts { get; set; }
        public Guid NutritionFactsId { get; set; }

        [ForeignKey("NutrientId")]
        public Nutrient Nutrient { get; set; }
        public Guid NutrientId { get; set; }
    }
}