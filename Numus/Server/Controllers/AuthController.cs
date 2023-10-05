using Microsoft.AspNetCore.Mvc;
using Numus.Shared.Dtoes.User;
using Numus.Shared.Dtoes;
using Numus.Shared.Interfaces;

namespace Numus.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly ICompanyService _companyService;

        public AuthController(IAuthService authService, IUserService userService, ICompanyService companyService)
        {
            _authService = authService;
            _userService = userService;
            _companyService = companyService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var user = await _userService.GetUserByNameAsync(request.Name);

            if (user != null && _authService.CheckPassword(request.Password, user.PasswordHash))
            {
                return Ok(_authService.GenerateJwtToken(user));
            }
            return BadRequest("Incorrect username or password.");
        }
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<string>> Register(RegisterDto request)
        {
            try
            {
                await _companyService.CreateCompanyAsync(request.Company);
                var companyId = await _companyService.GetCompanyIdByExternalIdAsync(request.Company.ExternalId);

                request.User.CompanyId = companyId;

                await _userService.CreateUserAsync(request.User);
                var user = await _userService.GetUserByNameAsync(request.User.Name);
                return Ok(_authService.GenerateJwtToken(user));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
