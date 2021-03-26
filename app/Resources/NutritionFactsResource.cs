using System.Collections.Generic;
using System.Text.Json.Serialization;
using TasteUfes.Models;

namespace TasteUfes.Resources
{
    public class NutritionFactsResource : EntityResource
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
        public IEnumerable<NutritionFactsNutrientsResource> NutritionFactsNutrients { get; set; }
    }
}