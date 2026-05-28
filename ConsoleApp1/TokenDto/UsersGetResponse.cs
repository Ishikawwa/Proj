using System.Text.Json.Serialization;

internal class UsersGetResponse
{
    [JsonPropertyName("response")]
    public List<VkUserData>? Response { get; set; }
}