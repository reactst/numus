using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Numus.Client.Pages;
using Numus.Shared.Dtoes.User;
using System.Net.Http.Json;
using System.Text.Json;

namespace Numus.Client.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDto?> GetUserByNameAsync(string name)
        {
            var response = await _httpClient.GetAsync($"api/user/name/{name}");
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<UserDto>(responseContent);
            }
            else
            {
                return null;
            }
        }
        public async Task<UserDto?> GetUserByExternalIdAsync(string externalId)
        {
            var response = await _httpClient.GetAsync($"api/user/{externalId}");
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<UserDto>(responseContent);
            }
            else
            {
                return null;
            }
        }
        public async Task<int?> GetUserIdAsync(string externalId)
        {
            var response = await _httpClient.GetAsync($"api/user/{externalId}/id");
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<int>(responseContent);
            }
            else
            {
                return null;
            }
        }
        public async Task<string?> UpdateUserAsync(UserDto user)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/user",user);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return responseContent;
            }
            else
            {
                return null;
            }
        }

        public async Task<string?> CreateUserAsync(UserDto user)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/user", user);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return responseContent;
            }
            else
            {
                return null;
            }
        }
        public async Task<List<UserDto>> GetUsersAsQueryableAsync()
        {
            var response = await _httpClient.GetAsync($"api/user/list");
            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<UserDto>>(responseContent);
        }

        public async void DeleteUser(UserDto userDto)
        {
            await _httpClient.DeleteAsync($"api/user/{userDto.ExternalId}");
        }
    }
}
