using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Numus.Shared.Dtoes;
using Numus.Shared.Dtoes.Company;
using Numus.Shared.Dtoes.User;
using System.Net.Http.Json;

namespace Numus.Shared.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<string> Login(UserDto user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", user);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync("token", responseContent);
                await _authenticationStateProvider.GetAuthenticationStateAsync();
                responseContent = string.Empty;
            }

            return responseContent;
        }

        public async Task<string> Register(UserRegisterDto user, CompanyDto company)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", new RegisterDto { User = user, Company = company});
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync("token", responseContent);
                await _authenticationStateProvider.GetAuthenticationStateAsync();
                responseContent = string.Empty;
            }

            return responseContent;
        }
    }
}
