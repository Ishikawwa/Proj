using Application.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Infrastructure.Services
{
    public class VkAuthService(HttpClient httpClient, IOptions<VkOptions> options) : IVkAuthService
    {
        private const string ApiVersion = "5.131";
        private readonly VkOptions _options = options.Value;

        public async Task<VkUserInfo?> ExchangeSilentTokenAsync(string silentToken, string uuid)
        {
            string exchangeUrl = $"https://api.vk.com/method/auth.exchangeSilentAuthToken" +
                $"?v={ApiVersion}" +
                $"&token={Uri.EscapeDataString(silentToken)}" +
                $"&uuid={Uri.EscapeDataString(uuid)}" +
                $"&access_token={_options.ServiceToken}";

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