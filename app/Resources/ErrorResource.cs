using System.Text.Json.Serialization;

namespace TasteUfes.Resources
{
    public class ErrorResource
    {
        [JsonPropertyName("property")]
        public string Property { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}