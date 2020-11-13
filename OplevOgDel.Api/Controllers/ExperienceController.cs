using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Models.Dto.ExperienceDto;
using OplevOgDel.Api.Services;
using KissLog;
using Microsoft.AspNetCore.Http;

namespace OplevOgDel.Api.Controllers
{
    [Route("api/experiences")]
    [Produces("application/json")]
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

        /// <summary>
        /// Gets all the experiences
        /// </summary>
        /// <returns>Returns all the experiences</returns>
        /// <response code="200">Returns all the experiences</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ViewExperienceDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllExperiences()
        {
            // get all experiences from the database
            var allExperiences = await _context.GetAllAsync();
            // map it to the DTO and return
            var listToReturn = _mapper.Map<IEnumerable<ViewExperienceDto>>(allExperiences);
            return Ok(listToReturn);
        }

        /// <summary>
        /// Gets an experience by ID
        /// </summary>
        /// <returns>Returns found experience</returns>
        /// <param name="id">Id of experience to get</param>
        /// <response code="200">Successfully returned the found experience</response>
        /// <response code="404">If no experience is found</response>     
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ViewOneExperienceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOneExperience([FromRoute] Guid id)
        {
            var foundExp = await _context.GetAnExperience(id);
            
            if (foundExp == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ViewOneExperienceDto>(foundExp));
        }

        /// <summary>
        /// Create an experience
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/experiences
        ///     {
        ///        "name": "Location1",
        ///        "Description": "A beautiful place",
        ///        "City": "København",
        ///        "Address": "Gadenavn 5",
        ///        "Category": "Musik"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created an experience</response>
        /// <response code="500">If a problem occurs during creation</response>     
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Update an experience
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/experiences/:id
        ///     {
        ///        "Description": "a very nice place",
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Id of experience to update</param>
        /// <param name="updatedExpr">new experience object to update</param>
        /// <response code="204">Successfully updated an experience</response>
        /// <response code="404">Can't find the experience to update</response>
        /// <response code="400">If you type in a non-existent category</response>
        /// <response code="500">If a problem occurs during update</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        /// <summary>
        /// Deletes an experience by ID
        /// </summary>
        /// <returns>Returns the deleted experiencee</returns>
        /// <param name="id">Id of experience to delete</param>
        /// <response code="200">Successfully returned the deleted experience</response>
        /// <response code="404">If no experience is found</response>
        /// <response code="500">If a problem occurs during update</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ViewOneExperienceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                _logger.Error("Failed to delete experience");
                return Problem();
            }
            return Ok(_mapper.Map<ViewOneExperienceDto>(exprToDelete));
        }
    }
}
