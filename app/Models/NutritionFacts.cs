using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasteUfes.Models
{
    public class NutritionFacts : Entity
    {
        [Required]
        public double ServingSize { get; set; }

        [Required]
        public Measures ServingSizeUnit { get; set; }

        [NotMapped]
        public double ServingEnergy { get; set; }

        [NotMapped]
        public double DailyValue { get; set; }

        [InverseProperty("NutritionFacts")]
        public ICollection<NutritionFactsNutrients> NutritionFactsNutrients { get; set; }

        [ForeignKey("FoodId")]
        public Food Food { get; set; }
        public Guid FoodId { get; set; }
    }
}