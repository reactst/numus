using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Numus.Shared
{
    public class NumusAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;

        public NumusAuthStateProvider(ILocalStorageService localStorage, HttpClient httpClient) 
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            var identity = new ClaimsIdentity();

            if (!string.IsNullOrEmpty(token))
            {
                var jwtToken = new JwtSecurityToken(token.Replace("\"", ""));
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            }

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;
        }
    }
}
