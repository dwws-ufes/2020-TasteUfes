using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TasteUfes.Resources
{
    public class AnonymousRecipeResource
    {
        [JsonPropertyName("ingredients")]
        public IEnumerable<IngredientResource> Ingredients { get; set; }

        [JsonPropertyName("nutrition_facts")]
        public NutritionFactsResource NutritionFacts { get; set; }
    }
}