using System;
using System.Text.Json.Serialization;

namespace TasteUfes.Controllers.Contracts.Requests
{
    public abstract class EntityRequest
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }
}
