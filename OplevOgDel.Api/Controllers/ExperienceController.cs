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
using OplevOgDel.Api.Models.Dto.RequestDto;
using OplevOgDel.Api.Helpers;

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
        public async Task<IActionResult> GetAllExperiences([FromQuery] RequestParametersDto req)
        {
            // get all experiences from the database
            //var allExperiences = await _context.GetAllAsync();
            var allExperiences = await _context.GetAllAsync(req);
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
            var foundExp = await _context.GetAnExperienceAsync(id);
            
            if (foundExp == null)
            {
                var err = new ErrorObject()
                {
                    Method = "GET",
                    At = $"/Experiences/{id}",
                    StatusCode = 404,
                    Error = "Could not find experience"

                };
                return NotFound(err);
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
        /// <response code="400">If the category does not exist</response>
        /// <response code="500">If a problem occurs during creation</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOneExperience([FromBody] NewExperienceDto createdExpr)
        {

            var category = await _context.GetCategoryByNameAsync(createdExpr.Category);

            if (category == null)
            {
                var err = new ErrorObject()
                {
                    Method = "POST",
                    At = "/Experiences",
                    StatusCode = 400,
                    Error = "Category is invalid"
                };
                return BadRequest(err);
            }

            var exprToAdd = _mapper.Map<Experience>(createdExpr);
            exprToAdd.Id = Guid.NewGuid();

            exprToAdd.Category = category;

            // TODO: add profile or it will crash

            _context.Create(exprToAdd);

            if (!await _context.Saveasync())
            {
                var errMsg = "Error createing an experience";
                _logger.Error(errMsg);
                var err = new ErrorObject()
                {
                    Method = "POST",
                    At = "/Experiences",
                    StatusCode = 500,
                    Error = errMsg

                };
                return StatusCode(500, err);
            }
            // TODO: consider mapping before sending back or send back the DB object
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
                var err = new ErrorObject()
                {
                    Method = "PUT",
                    At = $"/Experiences/{id}",
                    StatusCode = 404,
                    Error = "Could not find experience to edit"

                };
                return NotFound(err);
            }

            _mapper.Map(updatedExpr, exprFromDb);
            exprFromDb.ModifiedOn = DateTime.Now;
          
            if (updatedExpr.Category != null && updatedExpr.Category != string.Empty)
            {
                var categoryUpdated = await _context.GetCategoryByNameAsync(updatedExpr.Category);
                if (categoryUpdated == null)
                {
                    var err = new ErrorObject()
                    {
                        Method = "PUT",
                        At = $"/Experiences/{id}",
                        StatusCode = 400,
                        Error = "Category is invalid"

                    };
                    return BadRequest(err);
                }
                exprFromDb.CategoryId = categoryUpdated.Id;
            }


            _context.Update(exprFromDb);

            if (!await _context.Saveasync())
            {
                var errMsg = "Error updating an experience";
               _logger.Error(errMsg);
                var err = new ErrorObject()
                {
                    Method = "PUT",
                    At = $"/Experiences/{id}",
                    StatusCode = 500,
                    Error = errMsg

                };
                return StatusCode(500, err);
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
        /// <response code="500">If a problem occurs during deletion</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ViewOneExperienceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteOneExperience([FromRoute] Guid id)
        {
            var exprToDelete = await _context.GetFirstByExpressionAsync(x => x.Id == id);
            
            if (exprToDelete == null)
            {
                var err = new ErrorObject()
                {
                    Method = "DELETE",
                    At = $"/Experiences/{id}",
                    StatusCode = 404,
                    Error = "Could not find experience to delete"

                };
                return NotFound(err);
            }

            _context.Delete(exprToDelete);

            if (!await _context.Saveasync())
            {
                var errMsg = "Error deleting an experience";
                _logger.Error(errMsg);
                var err = new ErrorObject()
                {
                    Method = "DELETE",
                    At = $"/Experiences/{id}",
                    StatusCode = 500,
                    Error = errMsg

                };
                return StatusCode(500, err);
            }
            return Ok(_mapper.Map<ViewOneExperienceDto>(exprToDelete));
        }
    }
}
