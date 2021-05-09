using System;
using System.Text.Json.Serialization;
using TasteUfes.Models;

namespace TasteUfes.Controllers.Contracts.Responses
{
    public class NutritionFactsNutrientsResponse : EntityResponse
    {
        [JsonPropertyName("amount_per_serving")]
        public double AmountPerServing { get; set; }

        [JsonPropertyName("amount_per_serving_unit")]
        public Measures AmountPerServingUnit { get; set; }

        [JsonPropertyName("daily_value")]
        public double DailyValue { get; set; }

        [JsonPropertyName("nutrient")]
        public NutrientResponse Nutrient { get; set; }

        [JsonPropertyName("nutrient_id")]
        public Guid NutrientId { get; set; }
    }
}