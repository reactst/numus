using Numus.Shared.Constants;
using Numus.Shared.Dtoes.Company;

namespace Numus.Shared.Dtoes.User
{
    public class UserDto
    {
        public virtual Guid ExternalId { get; set; }
        public virtual string? Name { get; set; }
        public virtual string? Email { get; set; }
        public virtual string? Password { get; set; }
        public virtual string? PasswordHash { get; set; }
        public byte[]? Image { get; set; }
        public Roles Role { get; set; }
        public int? CompanyId { get; set; }
    }
}
