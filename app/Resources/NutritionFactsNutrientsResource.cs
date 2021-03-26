using System;
using System.Text.Json.Serialization;
using TasteUfes.Models;

namespace TasteUfes.Resources
{
    public class NutritionFactsNutrientsResource : EntityResource
    {
        [JsonPropertyName("amount_per_serving")]
        public double AmountPerServing { get; set; }

        [JsonPropertyName("amount_per_serving_unit")]
        public Measures AmountPerServingUnit { get; set; }

        [JsonPropertyName("daily_value")]
        public double DailyValue { get; set; }

        [JsonPropertyName("nutrition_facts_id")]
        public Guid NutritionFactsId { get; set; }

        [JsonPropertyName("nutrient")]
        public NutrientResource Nutrient { get; set; }

        [JsonPropertyName("nutrient_id")]
        public Guid NutrientId { get; set; }
    }
}