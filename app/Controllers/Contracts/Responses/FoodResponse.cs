using System.Text.Json.Serialization;

namespace TasteUfes.Controllers.Contracts.Responses
{
    public class FoodResponse : EntityResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("nutrition_facts")]
        public NutritionFactsResponse NutritionFacts { get; set; }
    }
}