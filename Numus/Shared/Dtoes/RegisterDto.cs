using Numus.Shared.Dtoes.Company;
using Numus.Shared.Dtoes.User;

namespace Numus.Shared.Dtoes
{
    public class RegisterDto
    {
        public UserRegisterDto User { get; set; }
        public CompanyDto Company { get; set; }
    }
}
