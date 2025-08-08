﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Portal.Authentication
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationState _anonymous;

        public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrEmpty(token))
            {
                return _anonymous;
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            return new AuthenticationState(
                        new ClaimsPrincipal(
                             new ClaimsIdentity(JwtParser.ParsedClaimsFromJwt(token), "jwtAuthType")));

        }
        public void NotifyUserAuthentication(string token)
        {
            var autheticatedUser = new ClaimsPrincipal(
                             new ClaimsIdentity(JwtParser.ParsedClaimsFromJwt(token), "jwtAuthType"));

            var authState = Task.FromResult(new AuthenticationState(autheticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogOut()
        {
            var authState = Task.FromResult(_anonymous);
            NotifyAuthenticationStateChanged(authState);
        }

    }
}
