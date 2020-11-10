using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Models;
using OplevOgDel.Api.Services;

namespace OplevOgDel.Api.Controllers
{
    [Route("api/experiences")]
    [ApiController]
    public class ExperienceController : ControllerBase
    {
        private readonly ILogger<ExperienceController> _logger;
        private readonly IExperienceRepository _context;
        private readonly IMapper _mapper;

        public ExperienceController(ILogger<ExperienceController> logger, IExperienceRepository context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            // get all experiences from the database
            var allExperiences = await _context.GetAllAsync();
            // map it to the DTO and return
            var listToReturn = _mapper.Map<IEnumerable<ViewExperienceDto>>(allExperiences);
            return Ok(listToReturn);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOne([FromRoute] Guid id)
        {
            var foundExp = await _context.GetAnExperience(id);
            
            if (foundExp == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ViewOneExperienceDto>(foundExp));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOne([FromBody] NewExperienceDto expr)
        {

            var exprToAdd = _mapper.Map<Experience>(expr);
            exprToAdd.Id = Guid.NewGuid();

            var category = await _context.GetCategoryByName(expr.Category);
            
            exprToAdd.Category = category;
            
            // TODO: add creator to experience or it will crash

            _context.Create(exprToAdd);

            if (!await _context.Saveasync())
            {
                _logger.LogError("Failed to create experience");
                return StatusCode(500);
            }
            return CreatedAtAction(nameof(GetOne), new { id =  exprToAdd.Id }, expr);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOne([FromRoute] Guid id, [FromBody] EditExperienceDto expr)
        {
            
            var exprFromDb = await _context.GetFirstByExpressionAsync(x => x.Id == id);
            if (exprFromDb == null)
            {
                return NotFound();
            }

            _mapper.Map(expr, exprFromDb);
            exprFromDb.ModifiedOn = DateTime.UtcNow;
            
                if (expr.Category != null && expr.Category != string.Empty)
            {
                var categoryUpdated = await _context.GetCategoryByName(expr.Category);
                if (categoryUpdated == null)
                {
                    return BadRequest();
                }
                exprFromDb.ExpCategoryId = categoryUpdated.Id;
            }

            _context.Update(exprFromDb);

            if (!await _context.Saveasync())
            {
                _logger.LogError("Failed to update experience");
                return StatusCode(500);
            }
            return NoContent();
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
