using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TasteUfes.Models;

namespace TasteUfes.Controllers.Contracts.Responses
{
    public class NutritionFactsResponse : EntityResponse
    {
        [JsonPropertyName("serving_size")]
        public double ServingSize { get; set; }

        [JsonPropertyName("serving_size_unit")]
        public Measures ServingSizeUnit { get; set; }

        [JsonPropertyName("serving_energy")]
        public double ServingEnergy { get; set; }

        [JsonPropertyName("daily_value")]
        public double DailyValue { get; set; }

        [JsonPropertyName("nutrition_facts_nutrients")]
        public IEnumerable<NutritionFactsNutrientsResponse> NutritionFactsNutrients { get; set; }

        [JsonPropertyName("food_id")]
        public Guid FoodId { get; set; }
    }
}