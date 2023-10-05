using Numus.Shared.Dtoes.User;

namespace Numus.Shared.Interfaces
{
    public interface IAuthService
    {
        bool CheckPassword(string password, string hashedPassword);
        string GenerateJwtToken(UserDto userDTO);
    }
}