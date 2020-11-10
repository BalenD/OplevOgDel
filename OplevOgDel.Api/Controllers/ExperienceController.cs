using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Services;

namespace OplevOgDel.Api.Controllers
{
    [Route("api/experiences")]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var allExperiences = await _context.GetAllAsync();
            return Ok(allExperiences);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOne([FromBody] Experience expr)
        {
            expr.Id = Guid.NewGuid();
            _context.Create(expr);

            if (!await _context.Saveasync())
            {
                _logger.LogError("Failed to create experience");
                return StatusCode(500);
            }
            return CreatedAtAction(nameof(GetOne), new { id =  expr.Id }, expr);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOne([FromRoute] Guid id, [FromBody] Experience expr)
        {
            var exprToUpdate = await _context.GetFirstByExpressionAsync(x => x.Id == id);
            if (exprToUpdate == null)
            {
                return NotFound();
            }

            // TODO: check which fields were changed
            // change said fields and update

            expr.Id = id;
            _context.Update(expr);

            if (!await _context.Saveasync())
            {
                _logger.LogError("Failed to update experience");
                return StatusCode(500);
            }
            return Ok(expr);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteOne([FromRoute] Guid id)
        {
            var exprToDelete = await _context.GetFirstByExpressionAsync(x => x.Id == id);
            
            if (exprToDelete == null)
            {
                return NotFound();
            }

            _context.Delete(exprToDelete);

            if (!await _context.Saveasync())
            {
                _logger.LogError("Failed to delete experience");
                return StatusCode(500);
            }
            return Ok(exprToDelete);
        }
    }
}
