using Numus.Shared.Dtoes.Company;

namespace Numus.Shared.Interfaces
{
    public interface ICompanyService
    {
        Task CreateCompanyAsync(CompanyDto companyDto);
        Task<bool> DeleteCompanyAsync(Guid externalId);
        Task<List<CompanyDto>> GetAllCompaniesAsync();
        Task<CompanyDto> GetCompanyByExternalIdAsync(Guid externalId);
        Task<int?> GetCompanyIdByExternalIdAsync(Guid externalId);
        Task<CompanyDto> UpdateCompanyAsync(Guid externalId, CompanyDto companyDto);
        Task<CompanyDto> GetCompanyByVatIdAsync(string vatId);
        Task<CompanyDto> GetCompanyByIdAsync(int id);
    }
}