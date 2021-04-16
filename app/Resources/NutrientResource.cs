using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TasteUfes.Resources
{
    public class NutrientResource : EntityResource
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("energy_per_gram")]
        public double EnergyPerGram { get; set; }

        [JsonPropertyName("daily_recommendation")]
        public double DailyRecommendation { get; set; }
    }
}