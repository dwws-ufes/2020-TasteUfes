using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TasteUfes.Controllers.Contracts.Requests
{
    public class PreparationRequest : EntityRequest
    {
        [Required]
        [JsonConverter(typeof(JsonTimeSpanConverter))]
        [JsonPropertyName("preparation_time")]
        public TimeSpan PreparationTime { get; set; }

        [JsonPropertyName("steps")]
        public IEnumerable<PreparationStepRequest> Steps { get; set; }
    }
}