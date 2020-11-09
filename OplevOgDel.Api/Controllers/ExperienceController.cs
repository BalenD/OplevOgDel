using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Services;

namespace OplevOgDel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceController : ControllerBase
    {
        private readonly ILogger<ExperienceController> _logger;
        private readonly IExperienceRepository _context;

        public ExperienceController(ILogger<ExperienceController> logger, IExperienceRepository context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allExperiences = await _context.GetAllAsync();
            return Ok(allExperiences);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] Guid id)
        {
            var foundExp = await _context.GetFirstByExpressionAsync(x => x.Id == id);
            
            if (foundExp == null)
            {
                return NotFound();
            }

            return Ok(foundExp);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOne([FromBody] Experience expr)
        {
            expr.Id = Guid.NewGuid();
            _context.Create(expr);

            if (!await _context.Saveasync())
            {
                _logger.LogError("Failed to create experience");
            }
            return Ok(expr);
        }
    }
}
