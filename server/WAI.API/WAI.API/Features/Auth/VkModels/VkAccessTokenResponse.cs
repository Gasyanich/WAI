using System.Text.Json.Serialization;

namespace WAI.API.Features.Auth.VkModels;

public class VkAccessTokenResponse
{
    [JsonPropertyName("access_token")] public string AccessToken { get; set; }

    [JsonPropertyName("user_id")] public long UserId { get; set; }

    [JsonPropertyName("expires_in")] public long ExpiresIn { get; set; }

    [JsonPropertyName("email")] public string Email { get; set; }
}