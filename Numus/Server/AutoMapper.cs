using AutoMapper;
using Microsoft.Build.Framework;
using Numus.Server.Entities;
using Numus.Shared.Dtoes.Category;
using Numus.Shared.Dtoes.Company;
using Numus.Shared.Dtoes.Invoice;
using Numus.Shared.Dtoes.User;

namespace Numus.Server
{
    internal class AutoMapperProfile : Profile
    {
        private readonly IMapper _mapper;

        public AutoMapperProfile() { }
        public AutoMapperProfile(IMapper mapper)
        {
            _mapper = mapper;

            CreateMap<User, UserDto>().PreserveReferences().BeforeMap((src, dest) =>
            {
                dest.PasswordHash = src.Password;
            }).ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<Company, CompanyDto>().PreserveReferences();
            CreateMap<Invoice, InvoiceDto>().PreserveReferences();
            CreateMap<Category, CategoryDto>().PreserveReferences();
        }
    }
}
