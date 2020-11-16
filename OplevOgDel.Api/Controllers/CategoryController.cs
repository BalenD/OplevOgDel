using System.Threading.Tasks;
using AutoMapper;
using KissLog;
using Microsoft.AspNetCore.Mvc;
using OplevOgDel.Api.Services;

namespace OplevOgDel.Api.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ILogger logger, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var allCategories = await _categoryRepository.GetAllAsync();

            if (allCategories == null)
            {
                return NotFound();
            }
            return Ok(allCategories);
        }
    }
}
