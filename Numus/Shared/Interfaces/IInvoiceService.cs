using Numus.Shared.Dtoes.Invoice;

namespace Numus.Shared.Interfaces
{
    public interface IInvoiceService
    {
        Task<InvoiceDto> Create(InvoiceDto invoiceDto);
        Task Delete(Guid externalId);
        Task<InvoiceDto> GetByExternalId(Guid externalId);
        Task<InvoiceDto> GetById(int id);
        Task<InvoiceDto> Update(InvoiceDto invoiceDto);
        Task<List<InvoiceDto>> GetFiltered(string barcode, DateTime? dateTimeFrom, DateTime? dateTimeTo, string invoicee, int? categoryFilter, int companyId);
        Task<byte[]> GeneratePdf(List<InvoiceDto> invoiceDto);
    }
}