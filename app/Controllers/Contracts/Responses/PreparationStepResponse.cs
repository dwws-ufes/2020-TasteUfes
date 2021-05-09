using System.Text.Json.Serialization;

namespace TasteUfes.Controllers.Contracts.Responses
{
    public class PreparationStepResponse : EntityResponse
    {
        [JsonPropertyName("step")]
        public int Step { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}