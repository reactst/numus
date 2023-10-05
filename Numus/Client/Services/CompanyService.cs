using Numus.Shared.Dtoes.Company;
using Numus.Shared.Dtoes.User;
using System.Net.Http.Json;
using System.Text.Json;

namespace Numus.Client.Services
{
    public class CompanyService
    {
        private readonly HttpClient _httpClient;

        public CompanyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> UpdateCompanyAsync(CompanyDto companyDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/company/{companyDto.ExternalId}", companyDto);
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
        public async Task<CompanyDto?> GetCompanyByIdAsync(int? companyId)
        {
            var response = await _httpClient.GetAsync($"api/company/{companyId}");
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<CompanyDto>(responseContent);
            }
            else
            {
                return null;
            }
        }
    }
}
