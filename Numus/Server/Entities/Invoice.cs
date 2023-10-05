using Numus.Client.Pages;
using Numus.Shared.Constants;
using Numus.Shared.Dtoes.Category;
using Numus.Shared.Dtoes.Invoice;
using Numus.Shared.Dtoes.User;
using System.ComponentModel;
using System.Data;
using System.Security.Policy;

namespace Numus.Server.Entities
{
    public class Invoice
    {
        public static Invoice Create(InvoiceDto invoiceDto)
        {
            if (invoiceDto.ExternalId == Guid.Empty)
            {
                invoiceDto.ExternalId = Guid.NewGuid();
            }

            return new Invoice()
            {
                ExternalId = invoiceDto.ExternalId,
                Barcode = invoiceDto.Barcode,
                DateTime = invoiceDto.DateTime,
                Invoicee = invoiceDto.Invoicee,
                Description = invoiceDto.Description,
                InvoiceDocument = invoiceDto.InvoiceDocument,
                ProofOfPaymentDocument = invoiceDto.ProofOfPaymentDocument,
                UserId = invoiceDto.UserId,
                Status = invoiceDto.Status,
                CategoryId = invoiceDto.CategoryId,
                CompanyId = invoiceDto.CompanyId,
            };
        }

        public void Update(InvoiceDto invoiceDto)
        {

            ExternalId = invoiceDto.ExternalId;
            Barcode = invoiceDto.Barcode;
            DateTime = invoiceDto.DateTime;
            Invoicee = invoiceDto.Invoicee;
            Description = invoiceDto.Description;
            InvoiceDocument = invoiceDto.InvoiceDocument;
            ProofOfPaymentDocument = invoiceDto.ProofOfPaymentDocument;
            UserId = invoiceDto.UserId;
            Status = invoiceDto.Status;
            CategoryId = invoiceDto.CategoryId;
            CompanyId = invoiceDto.CompanyId;

        }
        public int Id {  get; set; }
        public Guid ExternalId { get; set; }
        public string Barcode { get; set; }
        public DateTime DateTime { get; set; }  
        public string Invoicee { get; set; }    
        public string? Description { get; set; }
        public byte[] InvoiceDocument { get; set; }
        public byte[]? ProofOfPaymentDocument { get; set; }
        public Statuses Status { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }

    }
}
