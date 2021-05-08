using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TasteUfes.Models;

namespace TasteUfes.Controllers.Contracts.Responses
{
    public class NutritionFactsNutrientsResponse : EntityResponse
    {
        [Range(0, Int16.MaxValue)]
        [JsonPropertyName("amount_per_serving")]
        public double AmountPerServing { get; set; }

        [Required]
        [Range(1, 7)]
        [JsonPropertyName("amount_per_serving_unit")]
        public Measures AmountPerServingUnit { get; set; }

        [JsonPropertyName("daily_value")]
        public double DailyValue { get; set; }

        [JsonPropertyName("nutrient")]
        public NutrientResponse Nutrient { get; set; }

        [Required]
        [JsonPropertyName("nutrient_id")]
        public Guid NutrientId { get; set; }
    }
}