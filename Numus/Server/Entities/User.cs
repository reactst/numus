using Numus.Shared.Constants;
using Numus.Shared.Dtoes.User;

namespace Numus.Server.Entities
{
    public class User
    {
        public static User Create(UserDto userDto)
        {
            string password = userDto.PasswordHash;

            if (!string.IsNullOrEmpty(userDto.Password))
            {
                password = BCrypt.Net.BCrypt.EnhancedHashPassword(userDto.Password);
            }

            if (userDto.ExternalId == Guid.Empty)
            {
                userDto.ExternalId = Guid.NewGuid();
            }

            return new User()
            {
                ExternalId = userDto.ExternalId,
                Name = userDto.Name,
                Email = userDto.Email,
                Password = password,
                Role = userDto.Role,
                Image = userDto.Image,
                CompanyId = userDto.CompanyId,
            };
        }

        public void Update(UserDto userDto)
        {

            ExternalId = userDto.ExternalId;
            Name = userDto.Name;
            Email = userDto.Email;
            Password = userDto.PasswordHash;
            Role = userDto.Role;
            Image = userDto.Image;
            CompanyId = userDto.CompanyId;

        }

        public int Id { get; set; }
        public Guid ExternalId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
        public byte[]? Image { get; set; }
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
