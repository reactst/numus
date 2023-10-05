using Numus.Shared.Constants;
using Numus.Shared.Dtoes.Category;
using Numus.Shared.Dtoes.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numus.Shared.Dtoes.Invoice
{
    public class InvoiceDto
    {
        public Guid ExternalId { get; set; }
        public string Barcode { get; set; }
        public DateTime DateTime { get; set; }
        public string Invoicee { get; set; }
        public int? UserId { get; set; }
        public string? Description { get; set; }
        public byte[] InvoiceDocument { get; set; }
        public byte[]? ProofOfPaymentDocument { get; set; }
        public Statuses Status { get; set; }
        public int? CategoryId { get; set; }
        public int? CompanyId { get; set; }
    }
}
