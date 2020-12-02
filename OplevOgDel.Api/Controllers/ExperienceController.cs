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
using Microsoft.AspNetCore.Authorization;
using OplevOgDel.Api.Models.Dto;
using System.Linq;

namespace OplevOgDel.Api.Controllers
{
    /// <summary>
    /// The controller that handles all API calls to /api/experiences
    /// </summary>
    [Route("api/experiences")]
    [Produces("application/json")]
    [Authorize(Roles = Roles.AdminAndUser)]
    [ApiController]
    public class ExperienceController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IExperienceRepository _experienceRepository;
        private readonly IMapper _mapper;

        public ExperienceController(ILogger logger, IExperienceRepository repository, IMapper mapper)
        {
            _logger = logger;
            _experienceRepository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all the experiences
        /// </summary>
        /// <param name="req">Query to perform sorting, filtering, searching and pagination</param>
        /// <response code="200">Returns all the experiences</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<ViewExperienceDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllExperiences([FromQuery] ExperienceRequestParametersDto req)
        {
            // get all experiences from the database
            var allExperiences = await _experienceRepository.GetAllAsync(req);
            // map it to the DTO and return
            var listToReturn = _mapper.Map<IEnumerable<ViewExperienceDto>>(allExperiences);
            return Ok(listToReturn);
        }

        /// <summary>
        /// Get an experience by id
        /// </summary>
        /// <param name="id">Id of experience to get</param>
        /// <response code="200">Successfully returned the found experience</response>
        /// <response code="404">No experience was found with that id</response>     
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ViewOneExperienceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorObject),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOneExperience([FromRoute] Guid id)
        {
            // get the experience from the database 
            var foundExp = await _experienceRepository.GetAnExperienceAsync(id);
            
            // if no experience is found then return an error object
            if (foundExp == null)
            {
                var err = new ErrorObject()
                {
                    Method = "GET",
                    At = $"/api/Experiences/{id}",
                    StatusCode = 404,
                    Error = "Could not find experience"

                };
                return NotFound(err);
            }

            // map to the DTO and return found experience
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
        /// <response code="201">Successfully created the experience</response>
        /// <response code="400">Category is invalid</response>
        /// <response code="500">Problem occured during creation</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOneExperience([FromBody] NewExperienceDto createdExpr)
        {

            // check to see if the category is valid by checking against the database
            // if it's invalid, return an error object
            var category = await _experienceRepository.GetCategoryByNameAsync(createdExpr.Category);

            if (category == null)
            {
                var err = new ErrorObject()
                {
                    Method = "POST",
                    At = "/api/experiences",
                    StatusCode = 400,
                    Error = "Category is invalid"
                };
                return BadRequest(err);
            }

            // map the incoming DTO to our actual database model
            var exprToAdd = _mapper.Map<Experience>(createdExpr);

            // add the necessary relations for creation
            //exprToAdd.Id = Guid.NewGuid();
            exprToAdd.Category = category;
            //var profileId = User.Claims.FirstOrDefault(x => x.Type == "profileId").Value;
            //exprToAdd.ProfileId = Guid.Parse(profileId);
            exprToAdd.CreatedOn = DateTime.Now;

            // add the created experience to the database
            _experienceRepository.Create(exprToAdd);

            if (!await _experienceRepository.SaveAsync())
            {
                var errMsg = "Error creating an experience";
                _logger.Error(errMsg);
                var err = new ErrorObject()
                {
                    Method = "POST",
                    At = "/api/experiences",
                    StatusCode = 500,
                    Error = errMsg

                };
                return StatusCode(500, err);
            }
 
            return CreatedAtAction(nameof(GetOneExperience), new { id =  exprToAdd.Id }, _mapper.Map<ViewOneExperienceDto>(exprToAdd));
        }

        /// <summary>
        /// Update an experience by id
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
        /// <response code="401">Unauthorized to perform this action</response>
        /// <response code="404">Can't find the experience to update</response>
        /// <response code="500">problem occuured during update</response>
        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOneExperience([FromRoute] Guid id, [FromBody] EditExperienceDto updatedExpr)
        {
            
            // retrieve user to update, 
            var exprFromDb = await _experienceRepository.GetFirstByExpressionAsync(x => x.Id == id);

            // if user does not exist return error
            if (exprFromDb == null)
            {
                var err = new ErrorObject()
                {
                    Method = "PUT",
                    At = $"/api/experiences/{id}",
                    StatusCode = 404,
                    Error = "Could not find experience to edit"

                };
                return NotFound(err);
            }

            // if you are a user, you can only update your own experience
            if (User.IsInRole(Roles.User))
            {
                var profileId = User.Claims.FirstOrDefault(x => x.Type == "profileId").Value;
                if (Guid.Parse(profileId) != exprFromDb.ProfileId)
                {
                    var err = new ErrorObject()
                    {
                        Method = "PUT",
                        At = $"/api/experiences/{id}",
                        StatusCode = 401,
                        Error = "Unauthorized to perform this action"
                    };
                    return Unauthorized(err);
                }
            }

            // map the changes to the entity from the database
            _mapper.Map(updatedExpr, exprFromDb);
            exprFromDb.ModifiedOn = DateTime.Now;
          

            // add the changes to the database entity
            _experienceRepository.Update(exprFromDb);

            if (!await _experienceRepository.SaveAsync())
            {
                var errMsg = "Error updating an experience";
               _logger.Error(errMsg);
                var err = new ErrorObject()
                {
                    Method = "PUT",
                    At = $"/api/experiences/{id}",
                    StatusCode = 500,
                    Error = errMsg

                };
                return StatusCode(500, err);
            }
            return NoContent();
        }
        /// <summary>
        /// Delete an experience by id
        /// </summary>
        /// <param name="id">Id of experience to delete</param>
        /// <response code="204">Successfully deleted experience</response>
        /// <response code="404">No experience is found</response>
        /// <response code="500">Problem occured during deletion</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteOneExperience([FromRoute] Guid id)
        {

            // retrieve user to delete, if user does not exist return error
            var exprToDelete = await _experienceRepository.GetFirstByExpressionAsync(x => x.Id == id);
            
            if (exprToDelete == null)
            {
                var err = new ErrorObject()
                {
                    Method = "DELETE",
                    At = $"/api/experiences/{id}",
                    StatusCode = 404,
                    Error = "Could not find experience to delete"

                };
                return NotFound(err);
            }

            // delete the experience from the database
            _experienceRepository.Delete(exprToDelete);

            if (!await _experienceRepository.SaveAsync())
            {
                var errMsg = "Error deleting an experience";
                _logger.Error(errMsg);
                var err = new ErrorObject()
                {
                    Method = "DELETE",
                    At = $"/api/experiences/{id}",
                    StatusCode = 500,
                    Error = errMsg

                };
                return StatusCode(500, err);
            }
            return NoContent();
        }
    }
}
