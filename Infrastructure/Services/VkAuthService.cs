using Application.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Infrastructure.Services
{
    public class VkAuthService(HttpClient httpClient, IOptions<VkOptions> options) : IVkAuthService
    {
        private const string ApiVersion = "5.131";
        private readonly VkOptions _options = options.Value;

        public async Task<VkUserInfo?> ExchangeCodeAsync(string code, string deviceId, string codeVerifier)
        {
            var formData = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "authorization_code",
                ["code"] = code,
                ["device_id"] = deviceId,
                ["code_verifier"] = codeVerifier,
                ["client_id"] = _options.ClientId,
                ["redirect_uri"] = _options.RedirectUri
            });

            HttpResponseMessage response = await httpClient.PostAsync("https://id.vk.ru/oauth2/auth", formData);
            string rawJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine("VK response: " + rawJson);
            ExchangeTokenResponse? exchangeResult = System.Text.Json.JsonSerializer.Deserialize<ExchangeTokenResponse>(rawJson);

            if (exchangeResult == null || !string.IsNullOrEmpty(exchangeResult.Error))
                return null;

            string userAccessToken = exchangeResult.AccessToken;
            long userId = exchangeResult.UserId;

            string usersGetUrl = $"https://api.vk.com/method/users.get" +
                $"?v={ApiVersion}" +
                $"&user_ids={userId}" +
                $"&fields=photo_100" +
                $"&access_token={userAccessToken}";

            UsersGetResponse? usersResult = await httpClient.GetFromJsonAsync<UsersGetResponse>(usersGetUrl);
            VkUserData? vkUser = usersResult?.Response?.FirstOrDefault();

            if (vkUser == null)
                return null;

            return new VkUserInfo
            {
                VkId = userId.ToString(),
                FirstName = vkUser.FirstName,
                LastName = vkUser.LastName,
                Photo = vkUser.Photo,
            };
        }
    }
}