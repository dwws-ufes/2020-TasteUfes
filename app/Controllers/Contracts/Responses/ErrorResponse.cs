using System.Text.Json.Serialization;

namespace TasteUfes.Controllers.Contracts.Responses
{
    public class ErrorResponse
    {
        [JsonPropertyName("property")]
        public string Property { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}