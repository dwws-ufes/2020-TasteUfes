using System.Text.Json.Serialization;

namespace TasteUfes.Controllers.Contracts.Requests
{
    public class FoodRequest : EntityRequest
    {
        // [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("nutrition_facts")]
        public NutritionFactsRequest NutritionFacts { get; set; }
    }
}