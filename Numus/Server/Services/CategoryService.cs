using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Numus.Server.Data;
using Numus.Server.Entities;
using Numus.Shared.Dtoes.Category;
using Numus.Shared.Dtoes.Invoice;
using Numus.Shared.Interfaces;

namespace Numus.Server.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly NumusServerContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryService(NumusServerContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CategoryDto> GetById(int id)
        {
            var category = await _dbContext.Set<Category>().FindAsync(id);
            return _mapper.Map<CategoryDto>(category);
        }
        public async Task<CategoryDto> GetCategoryByExternalId(Guid externalId)
        {
            var category = await _dbContext.Set<Category>().FirstOrDefaultAsync(category => category.ExternalId == externalId);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<int> GetCategoryIdByExternalId(Guid externalId)
        {
            var category = await _dbContext.Set<Category>().FirstOrDefaultAsync(category => category.ExternalId == externalId);
            return category.Id;
        }

        public async Task<List<CategoryDto>> GetAll()
        {
            var categories = await _dbContext.Set<Category>().ToListAsync();
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> Create(CategoryDto categoryDto)
        {
            var category = Category.Create(categoryDto);
            _dbContext.Set<Category>().Add(category);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> Update(CategoryDto categoryDto)
        {
            var existingCategory = await _dbContext.Set<Category>().FirstOrDefaultAsync(category => category.ExternalId == categoryDto.ExternalId);
            if (existingCategory == null)
                return null;

            existingCategory.Update(categoryDto);

            await _dbContext.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(existingCategory);
        }

        public async Task Delete(Guid id)
        {
            var category = await _dbContext.Set<Category>().FirstOrDefaultAsync(category => category.ExternalId == id);
            if (category != null)
            {
                _dbContext.Set<Category>().Remove(category);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
