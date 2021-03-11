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

        [Required]
        public double ServingEnergy { get; set; }

        [NotMapped]
        public double DailyValue { get; set; }

        [InverseProperty("NutritionFacts")]
        public IEnumerable<NutritionFactsNutrients> NutritionFactsNutrients { get; set; }

        public Food Food { get; set; }
    }
}