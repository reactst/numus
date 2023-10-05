using Numus.Shared.Dtoes.User;

namespace Numus.Shared.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(UserDto userDTO);
        Task DeleteUserAsync(Guid externalId);
        Task<UserDto[]> GetAllUsersAsync();
        Task<UserDto> GetUserByExternalIdAsync(Guid externalId);
        Task<int> GetUserIdAsync(Guid externalId);
        Task<UserDto> GetUserByNameAsync(string name);
        List<UserDto> GetUsersAsQueryable();
        Task UpdateUserAsync(UserDto userDTO);
    }
}