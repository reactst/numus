using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Numus.Server.Entities;
using Numus.Shared.Dtoes.User;
using Numus.Shared.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace Numus.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{externalId}")]
        public async Task<ActionResult<UserDto>> GetUserByExternalId(Guid externalId)
        {
            var user = await _userService.GetUserByExternalIdAsync(externalId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(user));
        }
        [HttpGet("{externalId}/id")]
        public async Task<ActionResult<UserDto>> GetUserId(Guid externalId)
        {
            var userId = await _userService.GetUserIdAsync(externalId);

            if (userId == 0)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(userId));
        }

        [HttpGet]
        [Route("name/{name}")]
        public async Task<ActionResult<UserDto>> GetUserByName(string name)
        {
            var user = await _userService.GetUserByNameAsync(name);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(user));
        }

        [HttpGet]
        public async Task<ActionResult<UserDto[]>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            return Ok(JsonSerializer.Serialize(users));
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<List<UserDto>>> GetUsersAsQueryable()
        {
            var tokenString = Request.Headers.Authorization.ToString();
            var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString.Replace("Bearer ",""));
            var currentUserName = token.Claims.Where(claim => claim.Type == ClaimTypes.Name).First().Value;
            var currentUser = await _userService.GetUserByNameAsync(currentUserName);
            var users = _userService.GetUsersAsQueryable();
            users.RemoveAll(user => user.CompanyId != currentUser.CompanyId || user.Name == currentUser.Name);
            return Ok(JsonSerializer.Serialize(users));
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(UserDto userDTO)
        {
            try
            {
                await _userService.CreateUserAsync(userDTO);
            }
            catch (ArgumentException ex)
            {

                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDto userDTO)
        {
            try
            {
                await _userService.UpdateUserAsync(userDTO);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{externalId}")]
        public async Task<IActionResult> DeleteUser(Guid externalId)
        {
            try
            {
                await _userService.DeleteUserAsync(externalId);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
