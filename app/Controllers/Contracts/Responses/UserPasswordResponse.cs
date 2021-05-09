using System.Text.Json.Serialization;

namespace TasteUfes.Controllers.Contracts.Responses
{
    public class UserPasswordResponse
    {
        [JsonPropertyName("old_password")]
        public string OldPassword { get; set; }

        [JsonPropertyName("new_password")]
        public string NewPassword { get; set; }
    }
}