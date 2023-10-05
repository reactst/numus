using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Numus.Server.Data;
using Numus.Server.Entities;
using Numus.Shared.Interfaces;
using Numus.Shared.Dtoes.Company;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;

namespace Numus.Server.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly NumusServerContext _dbContext;
        private readonly IMapper _mapper;

        public CompanyService(NumusServerContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<CompanyDto>> GetAllCompaniesAsync()
        {
            var companies = await _dbContext.Companies.ToListAsync();
            return _mapper.Map<List<CompanyDto>>(companies);
        }

        public async Task<CompanyDto> GetCompanyByExternalIdAsync(Guid externalId)
        {
            var company = await _dbContext.Companies.FirstOrDefaultAsync(c => c.ExternalId == externalId);
            return _mapper.Map<CompanyDto>(company);
        }

        public async Task CreateCompanyAsync(CompanyDto companyDto)
        {
            var companyExisting = await GetCompanyByVatIdAsync(companyDto.VatId);

            if (companyExisting != null)
            {
                throw new ArgumentException("Company with specified VAT Id already exists.");
            }

            var company = Company.Create(companyDto);
            _dbContext.Companies.Add(company);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CompanyDto> UpdateCompanyAsync(Guid externalId, CompanyDto companyDto)
        {
            var company = await _dbContext.Companies.FirstOrDefaultAsync(c => c.ExternalId == externalId);

            if (company == null)
            {
                return null;
            }

            company.Update(companyDto);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CompanyDto>(company);
        }

        public async Task<bool> DeleteCompanyAsync(Guid externalId)
        {
            var company = await _dbContext.Companies.FirstOrDefaultAsync(c => c.ExternalId == externalId);

            if (company == null)
            {
                return false;
            }

            _dbContext.Companies.Remove(company);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<CompanyDto> GetCompanyByVatIdAsync(string vatId)
        {
            var company = await _dbContext.Companies.FirstOrDefaultAsync(c => c.VatId == vatId);
            return _mapper.Map<CompanyDto>(company);
        }

        public async Task<int?> GetCompanyIdByExternalIdAsync(Guid externalId)
        {
            var company = await _dbContext.Companies.FirstOrDefaultAsync(c => c.ExternalId == externalId);
            if(company == null)
            {
                return null;
            }
            return company.Id;
        }

        public async Task<CompanyDto> GetCompanyByIdAsync(int id)
        {
            var company = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == id);
            return _mapper.Map<CompanyDto>(company);
        }
    }
}
