using System;
using System.Text.Json.Serialization;

namespace TasteUfes.Controllers.Contracts.Responses
{
    public abstract class EntityResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }
}
