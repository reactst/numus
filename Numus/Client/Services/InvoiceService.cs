using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Numus.Shared.Dtoes.Invoice;
using Numus.Shared.Dtoes.User;
using System.Net.Http.Json;
using System.Text.Json;

namespace Numus.Client.Services
{
    public class InvoiceService
    {
        private readonly HttpClient _httpClient;


        public InvoiceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<InvoiceDto?> GetInvoiceByExternalIdAsync(string externalId)
        {
            var response = await _httpClient.GetAsync($"api/invoices/externalId/{externalId}");
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<InvoiceDto>(responseContent);
            }
            else
            {
                return null;
            }
        }
        public async Task<string?> UpdateInvoiceAsync(InvoiceDto invoiceDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/invoices", invoiceDto);
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

        public async Task<string?> CreateInvoiceAsync(InvoiceDto invoiceDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/invoices", invoiceDto);
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

        public async Task<byte[]?> GeneratePdfAsync(List<InvoiceDto> invoiceDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/invoices/pdf", invoiceDto);
            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<byte[]>(responseContent);

        }

        public async Task<List<InvoiceDto>> GetInvoicesFilteredAsync(string? barcode, DateTime? dateTimeFrom, DateTime? dateTimeTo, string? invoicee, int? categoryFilter, int companyId)
        {
            var response = await _httpClient.GetAsync($"api/invoices/list?barcode={barcode}&dateTimeFrom={dateTimeFrom}&dateTimeTo={dateTimeTo}&invoicee={invoicee}&categoryFilter={categoryFilter}&companyId={companyId}");
            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<InvoiceDto>>(responseContent);
        }
        public async void DeleteInvoice(InvoiceDto invoiceDto)
        {
            await _httpClient.DeleteAsync($"api/invoices/{invoiceDto.ExternalId}");
        }
    }
}
