using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Numus.Shared.Dtoes.Invoice;
using Numus.Shared.Interfaces;
using System.Text.Json;
using QuestPDF;
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using Numus.Server.Entities;

namespace Numus.Server.Services

{
    public class PdfService : IPdfService
    {
        private List<InvoiceDto> _invoiceDto;

        public byte[] GeneratePdf(List<InvoiceDto> invoice)
        {
            _invoiceDto = invoice;

            return Document.Create(documentContainer =>
            {
                ;
                documentContainer.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.Content().Element(ComposeContent);
                });

            }).GeneratePdf();
        }

        void ComposeContent(IContainer container)
        {
            container.Table(table =>
            {

                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("Barcode");
                    header.Cell().Element(CellStyle).Text("Invoicee");
                    header.Cell().Element(CellStyle).Text("Description");
                    header.Cell().Element(CellStyle).Text("DateTime");
                    header.Cell().Element(CellStyle).Text("Status");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                foreach (var invoice in _invoiceDto)
                {
                    table.Cell().Element(CellStyle).Text(invoice.Barcode);
                    table.Cell().Element(CellStyle).Text(invoice.Invoicee);
                    table.Cell().Element(CellStyle).Text(invoice.Description);
                    table.Cell().Element(CellStyle).Text(invoice.DateTime);
                    table.Cell().Element(CellStyle).Text(invoice.Status);
                }

                static IContainer CellStyle(IContainer container)
                {
                    return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                }

            });
        }
    }
}
