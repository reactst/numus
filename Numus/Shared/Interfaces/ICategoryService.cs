using Numus.Shared.Dtoes.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numus.Shared.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto> Create(CategoryDto categoryDto);
        Task Delete(Guid id);
        Task<List<CategoryDto>> GetAll();
        Task<CategoryDto> GetById(int id);
        Task<CategoryDto> GetCategoryByExternalId(Guid externalId);
        Task<int> GetCategoryIdByExternalId(Guid externalId);
        Task<CategoryDto> Update(CategoryDto categoryDto);
    }
}
