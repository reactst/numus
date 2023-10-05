using Numus.Shared.Dtoes.Invoice;
using QuestPDF.Infrastructure;

namespace Numus.Shared.Interfaces
{
    public interface IPdfService
    {
        public byte[] GeneratePdf(List<InvoiceDto> invoice);
    }
}
