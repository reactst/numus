using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Numus.Server.Data;
using Numus.Server.Entities;
using Numus.Shared.Interfaces;
using Numus.Shared.Dtoes.User;
using AutoMapper.QueryableExtensions;
using Numus.Client.Services;

namespace Numus.Server.Services
{
    public class UserService : IUserService
    {
        private readonly NumusServerContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(NumusServerContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUserByExternalIdAsync(Guid externalId)
        {
            var user = await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.ExternalId == externalId);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByNameAsync(string name)
        {
            var user = await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.Name == name);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto[]> GetAllUsersAsync()
        {
            var users = await _dbContext.Set<User>().ToArrayAsync();

            return _mapper.Map<UserDto[]>(users);
        }

        public async Task CreateUserAsync(UserDto userDTO)
        {
            var userExisting = await GetUserByNameAsync(userDTO.Name);

            if(userExisting != null)
            {
                throw new ArgumentException("User with specified name already exists.");
            }

            var user = User.Create(userDTO);

            await _dbContext.Set<User>().AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(UserDto userDTO)
        {
            var user = await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.ExternalId == userDTO.ExternalId);

            if (user == null)
            {
                throw new ArgumentException($"User with externalId {userDTO.ExternalId} not found");
            }

            user.Update(userDTO);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid externalId)
        {
            var user = await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.ExternalId == externalId);

            if (user == null)
            {
                throw new ArgumentException($"User with id {externalId} not found");
            }

            _dbContext.Set<User>().Remove(user);

            await _dbContext.SaveChangesAsync();
        }

        public List<UserDto> GetUsersAsQueryable()
        {
            var users = _dbContext.Set<User>().AsQueryable();

            return _mapper.ProjectTo<UserDto>(users).ToList();
        }

        public async Task<int> GetUserIdAsync(Guid externalId)
        {
            var user = await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.ExternalId == externalId);

            return user.Id;
        }
    }
}
