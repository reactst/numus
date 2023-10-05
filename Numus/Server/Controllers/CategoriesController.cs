using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Numus.Client.Pages;
using Numus.Shared.Dtoes.Category;
using Numus.Shared.Dtoes.User;
using Numus.Shared.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace Numus.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;

        public CategoriesController(ICategoryService categoryService, IUserService userService)
        {
            _categoryService = categoryService;
            _userService = userService;
        }

        [HttpGet("{externalId}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryByExternalId(Guid externalId)
        {
            var category = await _categoryService.GetCategoryByExternalId(externalId);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(category));
        }

        [HttpGet("{externalId}/id")]
        public async Task<ActionResult<CategoryDto>> GetCategoryIdByExternalId(Guid externalId)
        {
            var categoryId = await _categoryService.GetCategoryIdByExternalId(externalId);

            if (categoryId == 0)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(categoryId));
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAll()
        {
            var tokenString = Request.Headers.Authorization.ToString();
            var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString.Replace("Bearer ", ""));
            var currentUserName = token.Claims.Where(claim => claim.Type == ClaimTypes.Name).First().Value;
            var currentUser = await _userService.GetUserByNameAsync(currentUserName);
            var categories = await _categoryService.GetAll();
            categories.RemoveAll(category => category.CompanyId != currentUser.CompanyId);
            return Ok(JsonSerializer.Serialize(categories));
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create(CategoryDto categoryDto)
        {
            var createdCategory = await _categoryService.Create(categoryDto);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<CategoryDto>> Update(CategoryDto categoryDto)
        {
            var updatedCategory = await _categoryService.Update(categoryDto);
            if (updatedCategory == null)
                return NotFound();

            return updatedCategory;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _categoryService.Delete(id);
            return NoContent();
        }
    }
}
