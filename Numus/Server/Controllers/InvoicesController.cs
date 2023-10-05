using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Numus.Shared.Dtoes.Invoice;
using Numus.Shared.Interfaces;
using System.Text.Json;

namespace Numus.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<List<InvoiceDto>>> GetFiltered(int companyId, string barcode = "", DateTime? dateTimeFrom = null, DateTime? dateTimeTo = null, string invoicee = "", int? categoryFilter = null)
        {
            var invoices = await _invoiceService.GetFiltered(barcode, dateTimeFrom, dateTimeTo, invoicee, categoryFilter, companyId);
            return Ok(JsonSerializer.Serialize(invoices));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDto>> GetById(int id)
        {
            var invoice = await _invoiceService.GetById(id);
            if (invoice == null)
                return NotFound();

            return Ok(JsonSerializer.Serialize(invoice));
        }

        [HttpPost("pdf")]
        public async Task<ActionResult<byte[]>> GeneratePdf(List<InvoiceDto> invoiceDto)
        {
            var pdfData = await _invoiceService.GeneratePdf(invoiceDto);
            return Ok(JsonSerializer.Serialize(pdfData));
        }

        [HttpGet("externalId/{externalId}")]
        public async Task<ActionResult<InvoiceDto>> GetByExternalId(Guid externalId)
        {
            var invoice = await _invoiceService.GetByExternalId(externalId);
            if (invoice == null)
                return NotFound();

            return Ok(JsonSerializer.Serialize(invoice));
        }

        [HttpPost]
        public async Task<IActionResult> Create(InvoiceDto invoiceDto)
        {
            var createdInvoice = await _invoiceService.Create(invoiceDto);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<InvoiceDto>> Update(InvoiceDto invoiceDto)
        {
            var updatedInvoice = await _invoiceService.Update(invoiceDto);
            if (updatedInvoice == null)
                return NotFound();

            return Ok(JsonSerializer.Serialize(updatedInvoice));
        }

        [HttpDelete("{externalId}")]
        public async Task<IActionResult> Delete(Guid externalId)
        {
            await _invoiceService.Delete(externalId);
            return NoContent();
        }
    }
}
