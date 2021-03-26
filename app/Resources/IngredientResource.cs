using System;
using System.Text.Json.Serialization;
using TasteUfes.Models;

namespace TasteUfes.Resources
{
    public class IngredientResource : EntityResource
    {
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("quantity_unit")]
        public Measures QuantityUnit { get; set; }

        [JsonPropertyName("food")]
        public FoodResource Food { get; set; }

        [JsonPropertyName("food_id")]
        public Guid FoodId { get; set; }

        [JsonPropertyName("recipe_id")]
        public Guid RecipeId { get; set; }
    }
}