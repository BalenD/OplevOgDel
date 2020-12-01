using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Models.Dto;
using OplevOgDel.Api.Services;

namespace OplevOgDel.Api.Controllers
{
    /// <summary>
    /// The controller that handles all aPI calls to /api/categories
    /// </summary>
    [Route("api/categories")]
    [Produces("application/json")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Get all the categories
        /// </summary>
        /// <response code="200">Returns all the categories</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Category>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategories()
        {
            var allCategories = await _categoryRepository.GetAllAsync();
            return Ok(allCategories);
        }
    }
}
