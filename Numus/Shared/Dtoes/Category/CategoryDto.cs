using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numus.Shared.Dtoes.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public Guid ExternalId { get; set; }
        public string Name { get; set; }
        public int? CompanyId { get; set; }
    }
}
