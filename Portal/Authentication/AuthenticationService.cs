using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Portal.Authentication.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Portal.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient client, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            _client = client;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        public async Task<string> Login(AuthenticationUserModel userForAuthentication)
        {
            //var data = new FormUrlEncodedContent(new[]
            //{
            //    new KeyValuePair<string, string>("grant_type", "password"),
            //    new KeyValuePair<string, string>("username", userForAuthentication.Email),
            //    new KeyValuePair<string, string>("password", userForAuthentication.Password)
            // });

            var data = JsonSerializer.Serialize(userForAuthentication);
            var stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            var authResult = await _client.PostAsync("https://localhost:7042/api/Auth/Login", stringContent);
            var authContent = await authResult.Content.ReadAsStringAsync();

            if (authResult.IsSuccessStatusCode == false)
            {
                return null;
            }
            //var result = JsonSerializer.Deserialize<AuthenticatedUserModel>(
            //    authContent,
            //        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            await _localStorage.SetItemAsync("authToken", authContent);

            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(authContent);

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", authContent);

            return authContent;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogOut();
            _client.DefaultRequestHeaders.Authorization = null;

        }
    }
}
