using Numus.Shared.Dtoes.Company;
using Numus.Shared.Dtoes.User;
using System.ComponentModel.Design;

namespace Numus.Server.Entities
{
    public class Company
    {
        public static Company Create(CompanyDto companyDto)
        {
            if (companyDto.ExternalId == Guid.Empty)
            {
                companyDto.ExternalId = Guid.NewGuid();
            }

            return new Company() {
                ExternalId = companyDto.ExternalId,
                LegalAddress = companyDto.LegalAddress,
                LegalName = companyDto.LegalName,
                VatId = companyDto.VatId,
                Balance = companyDto.Balance,
            };
        }

        public void Update(CompanyDto company)
        {

            ExternalId = company.ExternalId;
            LegalAddress = company.LegalAddress;
            LegalName = company.LegalName;
            VatId = company.VatId;
            Balance = company.Balance;
        }

        public int Id { get; set; }
        public Guid ExternalId { get; set; }
        public string LegalName { get; set; }
        public string LegalAddress { get; set; }
        public string VatId { get; set; }
        public double Balance { get; set; }
    }
}
