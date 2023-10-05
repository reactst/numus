using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Numus.Server.Data;
using Numus.Server.Entities;
using Numus.Shared.Dtoes.Invoice;
using Numus.Shared.Interfaces;

namespace Numus.Server.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly NumusServerContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPdfService _pdfService;

        public InvoiceService(NumusServerContext dbContext, IMapper mapper, IPdfService pdfService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _pdfService = pdfService;
        }

        public async Task<InvoiceDto> GetById(int id)
        {
            var invoice = await _dbContext.Set<Invoice>().FirstOrDefaultAsync(i => i.Id == id);

            return _mapper.Map<InvoiceDto>(invoice);
        }

        public async Task<InvoiceDto> GetByExternalId(Guid externalId)
        {
            var invoice = await _dbContext.Set<Invoice>().FirstOrDefaultAsync(i => i.ExternalId == externalId);

            return _mapper.Map<InvoiceDto>(invoice);
        }

        public async Task<List<InvoiceDto>> GetFiltered(string barcode, DateTime? dateTimeFrom, DateTime? dateTimeTo, string invoicee, int? categoryFilter, int companyId)
        {
            var invoices = await _dbContext.Set<Invoice>().Where(invoice => invoice.CompanyId == companyId).ToListAsync();
            if (!string.IsNullOrEmpty(barcode)) {
                invoices = invoices.Where(invoice => invoice.Barcode.Contains(barcode)).ToList();
            }
            if (dateTimeFrom.HasValue)
            {
                invoices = invoices.Where(invoice => invoice.DateTime >= dateTimeFrom).ToList();
            }
            if (dateTimeTo.HasValue)
            {
                invoices = invoices.Where(invoice => invoice.DateTime <= dateTimeTo).ToList();
            }
            if (!string.IsNullOrEmpty(invoicee))
            {
                invoices = invoices.Where(invoice => invoice.Invoicee.Contains(invoicee)).ToList();
            }
            if(categoryFilter != null)
            {
                invoices = invoices.Where(invoice => invoice.CategoryId == categoryFilter).ToList();
            }


            return _mapper.Map<List<InvoiceDto>>(invoices);
        }

        public async Task<InvoiceDto> Create(InvoiceDto invoiceDto)
        {
            var invoice = Invoice.Create(invoiceDto);
            _dbContext.Set<Invoice>().Add(invoice);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<InvoiceDto>(invoice);
        }

        public async Task<InvoiceDto> Update(InvoiceDto invoiceDto)
        {
            var existingInvoice = await _dbContext.Set<Invoice>().FirstOrDefaultAsync(invoice => invoice.ExternalId == invoiceDto.ExternalId);
            if (existingInvoice == null)
                return null;

            existingInvoice.Update(invoiceDto);

            await _dbContext.SaveChangesAsync();
            return _mapper.Map<InvoiceDto>(existingInvoice);
        }

        public async Task Delete(Guid externalId)
        {
            var invoice = await _dbContext.Set<Invoice>().FirstOrDefaultAsync(i => i.ExternalId == externalId);
            if (invoice != null)
            {
                _dbContext.Set<Invoice>().Remove(invoice);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<byte[]> GeneratePdf(List<InvoiceDto> invoiceDto)
        {
            return _pdfService.GeneratePdf(invoiceDto);
        }
    }
}

