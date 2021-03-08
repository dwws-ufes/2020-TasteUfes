using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasteUfes.Models
{
    public class Nutrient : Entity
    {
        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        public double EnergyPerGram { get; set; }

        [Required]
        public double DailyRecommendation { get; set; }

        [InverseProperty("Nutrient")]
        public IEnumerable<NutritionFactsNutrients> NutritionFactsNutrients { get; set; }
    }
}