using System;
using System.Threading.Tasks;
using AutoMapper;
using KissLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OplevOgDel.Api.Helpers;
using OplevOgDel.Api.Models.Dto.UserDto;
using OplevOgDel.Api.Services;
using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Models.Dto;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using OplevOgDel.Api.Models.Dto.RequestDto;

namespace OplevOgDel.Api.Controllers
{
    /// <summary>
    /// The controller that handles all API calls to /api/users
    /// </summary>
    [Route("api/users")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UserController(IUserRepository repository, IMapper mapper, ILogger logger)
        {
            _userRepository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>W
        /// Get all the users
        /// </summary>
        /// <param name="req">Query to perform sorting, filtering, searching and pagination</param>
        /// <response code="200">Returns all the users</response>
        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(IEnumerable<ViewUserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserRequestParametersDto req)
        {
            // retrieve all the users from the database
            var foundUsers = await _userRepository.GetAllAsync(req);
            // map and return the users
            var listToReturn = _mapper.Map<IEnumerable<ViewUserDto>>(foundUsers);
            return Ok(listToReturn);
        }

        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="id">Id of the user to get</param>
        /// <response code="200">Successfully returned the found user from the database</response>
        /// <response code="404">No user was found</response>   
        [HttpGet("{id}")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(ViewUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOneUser([FromRoute] Guid id)
        {
            // find the user with the id and if none is found return error
            var foundUser = await _userRepository.GetFirstByExpressionAsync(x => x.Id == id);
            if (foundUser == null)
            {
                var err = new ErrorObject()
                {
                    Method = "GET",
                    At = $"/api/users/{id}",
                    StatusCode = 404,
                    Error = "Could not find user"
                };
                return NotFound(err);
            }

            // map the found user to a view dto and return
            return Ok(_mapper.Map<ViewUserDto>(foundUser));
        }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/users
        ///     {
        ///        "username": "JohnSmith",
        ///        "Password": "password",
        ///        "email": "JohnSmith@gmail.com",
        ///        "Role": "User",
        ///        "profileId": "7BE07C3C-382A-4298-9941-9359BE23E106"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Successfully created the úser</response>
        /// <response code="500">Problem occured during creation</response> 
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(ViewUserDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOneUser([FromBody] User user)
        {
            // give the user object an ID and save it in the database
            user.Id = Guid.NewGuid();

            user.CreatedOn = DateTime.Now;

            _userRepository.Create(user);

            if (!await _userRepository.SaveAsync())
            {
                var errMsg = "Error creating user";
                _logger.Error(errMsg);
                var err = new ErrorObject()
                {
                    Method = "POST",
                    At = "/api/users",
                    StatusCode = 500,
                    Error = errMsg
                };
                return StatusCode(500, err);
            }
            return CreatedAtAction(nameof(GetOneUser), new { id = user.Id }, _mapper.Map<ViewUserDto>(user));
        }

        /// <summary>
        /// Update a user by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/users/:id
        ///     {
        ///        "username": "newUserName4",
        ///     }
        ///
        /// </remarks>
        /// <param name="user">New user object to update</param>
        /// <param name="id">Id of the user to update</param>
        /// <response code="204">Successfully updated a user</response>
        /// <response code="404">Unauthorized to perform this action</response>
        /// <response code="404">No user was found</response>
        /// <response code="500">Problem occuured during update</response>
        [HttpPut("{id}")]
        [Authorize(Roles = Roles.AdminAndUser)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto user, [FromRoute] Guid id)
        {
            // if you are a user, you can only update yourself
            if (User.IsInRole(Roles.User))
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "userId").Value;
                if (Guid.Parse(userId) != id)
                {
                    var err = new ErrorObject()
                    {
                        Method = "PUT",
                        At = $"/api/users/{id}",
                        StatusCode = 401,
                        Error = "Unauthorized to perform this action"
                    };
                    return Unauthorized(err);
                }
            }

            // find the user to update and if they don't exist, return an error
            var foundUser = await _userRepository.GetFirstByExpressionAsync(x => x.Id == id);
            if (foundUser == null)
            {
                var err = new ErrorObject()
                {
                    Method = "PUT",
                    At = $"/api/users/{id}",
                    StatusCode = 404,
                    Error = "Could not find user to update"
                };
                return NotFound(err);
            }
            
            // map the changes to the found user object
            _mapper.Map(user, foundUser);
            foundUser.ModifiedOn = DateTime.Now;

            // update the entity in the database
            _userRepository.Update(foundUser);

            if (!await _userRepository.SaveAsync())
            {
                var errMsg = "Error updating user";
                _logger.Error(errMsg);
                var err = new ErrorObject()
                {
                    Method = "PUT",
                    At = "/api/users",
                    StatusCode = 500,
                    Error = errMsg
                };
                return StatusCode(500, err);
            }
            return NoContent();
        }

        /// <summary>
        /// Delete a user by id
        /// </summary>
        /// <param name="id">Id of user to delete</param>
        /// <response code="204">Successfully deleted user</response>
        /// <response code="401">Unauthorized to perform this action</response>
        /// <response code="404">No user was found</response>
        /// <response code="500">Problem occured during deletion</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.AdminAndUser)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteOneUser([FromRoute]Guid id)
        {

            // if you are a user, you can only delete yourself
            if (User.IsInRole(Roles.User))
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "userId").Value;
                if (Guid.Parse(userId) != id)
                {
                    var err = new ErrorObject()
                    {
                        Method = "DELETE",
                        At = $"/api/users/{id}",
                        StatusCode = 401,
                        Error = "Unauthorized to perform this action"
                    };
                    return Unauthorized(err);
                }
            }

            // find the user to delete and if they don't exist, return an error
            var foundUser = await _userRepository.GetFirstByExpressionAsync(x => x.Id == id);
            if (foundUser == null)
            {
                var err = new ErrorObject()
                {
                    Method = "DELETE",
                    At = $"/api/users/{id}",
                    StatusCode = 404,
                    Error = "Could not find user to delete"
                };
                return NotFound(err);
            }

            // delete the user from the database
            _userRepository.Delete(foundUser);

            if (!await _userRepository.SaveAsync())
            {
                var errMsg = "Error deleting user";
                _logger.Error(errMsg);
                var err = new ErrorObject()
                {
                    Method = "DELETE",
                    At = $"/api/users/{id}",
                    StatusCode = 500,
                    Error = errMsg
                };
                return StatusCode(500, err);
            }

            return NoContent();

        }

    }
}
