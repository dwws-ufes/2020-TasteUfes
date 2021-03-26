using System;
using System.Text.Json.Serialization;

namespace TasteUfes.Resources
{
    public class FoodResource : EntityResource
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("nutrition_facts")]
        public NutritionFactsResource NutritionFacts { get; set; }

        [JsonPropertyName("nutrition_facts_id")]
        public Guid NutritionFactsId { get; set; }
    }
}