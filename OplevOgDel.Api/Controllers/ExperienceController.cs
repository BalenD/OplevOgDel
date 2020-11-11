using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Models;
using OplevOgDel.Api.Services;
using KissLog;
using OplevOgDel.Api.Helpers;

namespace OplevOgDel.Api.Controllers
{
    [Route("api/experiences")]
    [ApiConventionType(typeof(OplevOgDelConvention))]
    [ApiController]
    public class ExperienceController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IExperienceRepository _context;
        private readonly IMapper _mapper;

        public ExperienceController(ILogger logger, IExperienceRepository context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExperiences()
        {
            // get all experiences from the database
            var allExperiences = await _context.GetAllAsync();
            // map it to the DTO and return
            var listToReturn = _mapper.Map<IEnumerable<ViewExperienceDto>>(allExperiences);
            return Ok(listToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneExperience([FromRoute] Guid id)
        {
            var foundExp = await _context.GetAnExperience(id);
            
            if (foundExp == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ViewOneExperienceDto>(foundExp));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOneExperience([FromBody] NewExperienceDto createdExpr)
        {

            var exprToAdd = _mapper.Map<Experience>(createdExpr);
            exprToAdd.Id = Guid.NewGuid();

            var category = await _context.GetCategoryByName(createdExpr.Category);
            
            exprToAdd.Category = category;
            
            // TODO: add creator to experience or it will crash

            _context.Create(exprToAdd);

            if (!await _context.Saveasync())
            {
                _logger.Error("Failed to create experience");
                return Problem();
            }
            return CreatedAtAction(nameof(GetOneExperience), new { id =  exprToAdd.Id }, createdExpr);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOneExperience([FromRoute] Guid id, [FromBody] EditExperienceDto updatedExpr)
        {
            
            var exprFromDb = await _context.GetFirstByExpressionAsync(x => x.Id == id);
            if (exprFromDb == null)
            {
                return NotFound();
            }

            _mapper.Map(updatedExpr, exprFromDb);
            exprFromDb.ModifiedOn = DateTime.UtcNow;
            
                if (updatedExpr.Category != null && updatedExpr.Category != string.Empty)
            {
                var categoryUpdated = await _context.GetCategoryByName(updatedExpr.Category);
                if (categoryUpdated == null)
                {
                    return BadRequest();
                }
                exprFromDb.ExpCategoryId = categoryUpdated.Id;
            }

            _context.Update(exprFromDb);

            if (!await _context.Saveasync())
            {
               _logger.Error("Failed to update experience");
                return Problem();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOneExperience([FromRoute] Guid id)
        {
            var exprToDelete = await _context.GetFirstByExpressionAsync(x => x.Id == id);
            
            if (exprToDelete == null)
            {
                return NotFound();
            }

            _context.Delete(exprToDelete);

            if (!await _context.Saveasync())
            {
                //_logger.LogError("Failed to delete experience");
                return Problem();
            }
            return Ok(exprToDelete);
        }
    }
}
