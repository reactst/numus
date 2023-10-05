using Numus.Shared.Dtoes.Category;
using Numus.Shared.Dtoes.Company;

namespace Numus.Server.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public Guid ExternalId { get; set; }
        public string Name { get; set; }
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public static Category Create(CategoryDto categoryDto)
        {
            if (categoryDto.ExternalId == Guid.Empty)
            {
                categoryDto.ExternalId = Guid.NewGuid();
            }
            return new Category()
            {
                ExternalId = categoryDto.ExternalId,
                Name = categoryDto.Name,
                CompanyId = categoryDto.CompanyId,
            };
        }
        public void Update(CategoryDto categoryDto)
        {
            ExternalId = categoryDto.ExternalId;
            Name = categoryDto.Name;
            CompanyId = categoryDto.CompanyId;
        }
    }
}
