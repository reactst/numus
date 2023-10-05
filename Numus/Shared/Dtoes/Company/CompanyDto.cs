using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numus.Shared.Dtoes.Company
{
    public class CompanyDto
    {
        public Guid ExternalId { get; set; }
        public string LegalName { get; set; }
        public string LegalAddress { get; set; }
        public string VatId { get; set; }
        public double Balance { get; set; }
    }
}
