using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TasteUfes.Resources
{
    public class FoodResource : EntityResource
    {
        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("nutrition_facts")]
        public NutritionFactsResource NutritionFacts { get; set; }
    }
}