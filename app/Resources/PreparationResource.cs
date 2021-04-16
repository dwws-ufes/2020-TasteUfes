using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TasteUfes.Resources
{
    public class PreparationResource : EntityResource
    {
        [Required]
        [JsonConverter(typeof(JsonTimeSpanConverter))]
        [JsonPropertyName("preparation_time")]
        public TimeSpan PreparationTime { get; set; }

        [JsonPropertyName("steps")]
        public IEnumerable<PreparationStepResource> Steps { get; set; }
    }
}