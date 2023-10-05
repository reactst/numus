using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Numus.Shared.Dtoes.Category;
using Numus.Shared.Dtoes.Invoice;
using Numus.Shared.Dtoes.User;
using System.Net.Http.Json;
using System.Text.Json;

namespace Numus.Client.Services
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CategoryDto?> GetCategoryByExternalIdAsync(string externalId)
        {
            var response = await _httpClient.GetAsync($"api/categories/{externalId}");
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<CategoryDto>(responseContent);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            var response = await _httpClient.GetAsync($"api/categories");
            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<CategoryDto>>(responseContent);
        }

        public async Task<string?> UpdateCategoryAsync(CategoryDto categoryDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/categories", categoryDto);
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

        public async Task<string?> CreateCategoryAsync(CategoryDto categoryDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/categories", categoryDto);
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
        public async void DeleteCategory(CategoryDto categoryDto)
        {
            await _httpClient.DeleteAsync($"api/categories/{categoryDto.ExternalId}");
        }
    }
}
