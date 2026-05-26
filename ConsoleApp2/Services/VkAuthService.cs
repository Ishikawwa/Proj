using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Services
{
    file class ExchangeTokenResponse
    {
        [JsonPropertyName("response")]
        public ExchangeTokenData? Response { get; set; }

        [JsonPropertyName("error")]
        public VkApiError? Error { get; set; }
    }

    file class ExchangeTokenData
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }
    }

    file class UsersGetResponse
    {
        [JsonPropertyName("response")]
        public List<VkUserData>? Response { get; set; }
    }

    file class VkUserData
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("photo_100")]
        public string? Photo { get; set; }
    }

    file class VkApiError
    {
        [JsonPropertyName("error_code")]
        public int ErrorCode { get; set; }

        [JsonPropertyName("error_msg")]
        public string ErrorMsg { get; set; } = string.Empty;
    }

    public class VkAuthService(HttpClient httpClient, IConfiguration configuration) : IVkAuthService
    {
        private const string ApiVersion = "5.131";

        public async Task<VkUserInfo?> ExchangeSilentTokenAsync(string silentToken, string uuid)
        {
            string serviceToken = configuration["VkOptions:ServiceToken"]!;

            string exchangeUrl = $"https://api.vk.com/method/auth.exchangeSilentAuthToken" +
                $"?v={ApiVersion}" +
                $"&token={Uri.EscapeDataString(silentToken)}" +
                $"&uuid={Uri.EscapeDataString(uuid)}" +
                $"&access_token={serviceToken}";

            ExchangeTokenResponse? exchangeResult =
                await httpClient.GetFromJsonAsync<ExchangeTokenResponse>(exchangeUrl);

            if (exchangeResult?.Response == null)
                return null;

            string userAccessToken = exchangeResult.Response.AccessToken;
            long userId = exchangeResult.Response.UserId;

            string usersGetUrl = $"https://api.vk.com/method/users.get" +
                $"?v={ApiVersion}" +
                $"&user_ids={userId}" +
                $"&fields=photo_100" +
                $"&access_token={userAccessToken}";

            UsersGetResponse? usersResult =
                await httpClient.GetFromJsonAsync<UsersGetResponse>(usersGetUrl);

            VkUserData? vkUser = usersResult?.Response?.FirstOrDefault();
            if (vkUser == null)
                return null;

            return new VkUserInfo
            {
                VkId = userId.ToString(),
                FirstName = vkUser.FirstName,
                LastName = vkUser.LastName,
                Photo = vkUser.Photo,
                Email = exchangeResult.Response.Email
            };
        }
    }
}