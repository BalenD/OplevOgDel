using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KissLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OplevOgDel.Api.Helpers;
using OplevOgDel.Api.Models.Dto;
using OplevOgDel.Api.Models.Dto.ProfileDto;
using OplevOgDel.Api.Models.Dto.RequestDto;
using OplevOgDel.Api.Services;

namespace OplevOgDel.Api.Controllers
{
    /// <summary>
    /// The controller that handles all API calls to /api/profiles
    /// </summary>
    [Route("api/profiles")]
    [Produces("application/json")]
    [Authorize(Roles = Roles.AdminAndUser)]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        public ProfileController(ILogger logger, IProfileRepository profileRepository, IMapper mapper)
        {
            _logger = logger;
            _profileRepository = profileRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all the profiles
        /// </summary>
        /// <param name="req">Query to perform sorting, filtering, searching and pagination</param>
        /// <response code="200">Returns all the profiles</response>
        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(IEnumerable<Profile>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProfiles([FromQuery] ProfileRequestParametersDto req)
        {
            var foundProfiles = await _profileRepository.GetAllAsync(req);
            return Ok(foundProfiles);

        }

        /// <summary>
        /// Get a profile by id
        /// </summary>
        /// <param name="id">Id of the profile to get</param>
        /// <response code="200">Successfully returned the found profile</response>
        /// <response code="404">No profile was found with that id</response>  
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Data.Models.Profile), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOneProfile([FromRoute] Guid id)
        {
            var foundProfile = await _profileRepository.GetAProfileAsync(id);

            if (foundProfile == null)
            {
                var err = new ErrorObject()
                {
                    Method = "GET",
                    At = $"/api/profiles/{id}",
                    StatusCode = 404,
                    Error = "Could not find profile"

                };
                return NotFound(err);
            }

            return Ok(foundProfile);
        }
        /// <summary>
        /// Create a profile
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/profiles
        ///     {
        ///        "FirstName": "John",
        ///        "LastName": "Smith",
        ///        "Gender": "Male",
        ///        "Birthday": "19/05/1990",
        ///        "City": "Helsingør",
        ///        "Address": "Testing gade 5"
        ///     }
        ///     
        /// </remarks>
        /// <response code="201">Successfully created the profile</response>
        /// <response code="500">Problem occured during creation</response> 
        [HttpPost]
        [ProducesResponseType(typeof(Data.Models.Profile), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOneProfile([FromBody] NewProfileDto createdProfile)
        {
            var profileToAdd = _mapper.Map<Data.Models.Profile>(createdProfile);

            profileToAdd.Id = Guid.NewGuid();
            // TODO: add profile to user
            profileToAdd.CreatedOn = DateTime.Now;

            _profileRepository.Create(profileToAdd);

            if (!await _profileRepository.SaveAsync())
            {
                var errMsg = "Error creating a profile";
                _logger.Error(errMsg);
                var err = new ErrorObject()
                {
                    Method = "POST",
                    At = "/api/profiles",
                    StatusCode = 500,
                    Error = errMsg

                };
                return StatusCode(500, err);
            }
            return CreatedAtAction(nameof(GetOneProfile), new { id = profileToAdd.Id }, profileToAdd);
        }

        /// <summary>
        /// Update a profile by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/profiles/:id
        ///     {
        ///        "FirstName": "Johnny",
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Id of the profiel to update</param>
        /// <param name="updatedProfile">new profile object to update</param>
        /// <response code="204">Successfully updated a profile</response>
        /// <response code="401">Unauthorized to perform this action</response>
        /// <response code="404">Can't find the profile to update</response>
        /// <response code="500">problem occuured during update</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOneProfile([FromRoute] Guid id, [FromBody] EditProfileDto updatedProfile)
        {
            var profileFromDb = await _profileRepository.GetFirstByExpressionAsync(x => x.Id == id);

            if (profileFromDb == null)
            {
                var err = new ErrorObject()
                {
                    Method = "PUT",
                    At = $"/api/profiles/{id}",
                    StatusCode = 404,
                    Error = "Could not find profile to edit"

                };
                return NotFound(err);
            }

            if (User.IsInRole(Roles.User))
            {
                var profileId = User.Claims.FirstOrDefault(x => x.Type == "profileId").Value;
                if (Guid.Parse(profileId) != profileFromDb.Id)
                {
                    var err = new ErrorObject()
                    {
                        Method = "PUT",
                        At = $"/api/profiles/{id}",
                        StatusCode = 401,
                        Error = "Unauthorized to perform this action"
                    };
                    return Unauthorized(err);
                }
            }

            _mapper.Map(updatedProfile, profileFromDb);
            profileFromDb.ModifiedOn = DateTime.Now;
            _profileRepository.Update(profileFromDb);

            if (!await _profileRepository.SaveAsync())
            {
                var errMsg = "Error updating a profile";
                _logger.Error(errMsg);
                var err = new ErrorObject()
                {
                    Method = "PUT",
                    At = $"/api/profiles/{id}",
                    StatusCode = 500,
                    Error = errMsg

                };
                return StatusCode(500, err);
            }
            return NoContent();
        }

        /// <summary>
        /// Delete a profile by id
        /// </summary>
        /// <param name="id">Id of profile to delete</param>
        /// <response code="204">Successfully deleted profile</response>
        /// <response code="404">No profile is found</response>
        /// <response code="500">Problem occured during deletion</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteOneProfile([FromRoute] Guid id)
        {
            var profileToDelete = await _profileRepository.GetFirstByExpressionAsync(x => x.Id == id);

            if (profileToDelete == null)
            {
                var err = new ErrorObject()
                {
                    Method = "DELETE",
                    At = $"/api/profiles/{id}",
                    StatusCode = 404,
                    Error = "Could not find profile to delete"

                };
                return NotFound(err);
            }

            _profileRepository.Delete(profileToDelete);

            if (!await _profileRepository.SaveAsync())
            {
                var errMsg = "Error deleting a profile";
                _logger.Error(errMsg);
                var err = new ErrorObject()
                {
                    Method = "DELETE",
                    At = $"/api/profiles/{id}",
                    StatusCode = 500,
                    Error = errMsg

                };
                return StatusCode(500, err);
            }
            return NoContent();
        }
    }
}
