using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TasteUfes.Resources
{
    public class PreparationResource : EntityResource
    {
        [Required]
        [JsonPropertyName("preparation_time")]
        public TimeSpan PreparationTime { get; set; }

        [JsonPropertyName("recipe_id")]
        public Guid RecipeId { get; set; }

        [JsonPropertyName("steps")]
        public IEnumerable<PreparationStepResource> Steps { get; set; }
    }
}