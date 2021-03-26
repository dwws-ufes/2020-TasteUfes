using System;
using System.Text.Json.Serialization;

namespace TasteUfes.Resources
{
    public abstract class EntityResource
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }
}
