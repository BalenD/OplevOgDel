using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KissLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OplevOgDel.Api.Helpers;
using OplevOgDel.Api.Models.Configuration;
using OplevOgDel.Api.Models.Dto.UserDto;
using OplevOgDel.Api.Services;
using System.Security.Claims;
using BC = BCrypt.Net.BCrypt;
using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Models.Dto;
using System.Collections.Generic;

namespace OplevOgDel.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly SecretOptions _secretsOptions;

        public UserController(IUserRepository context, IMapper mapper, ILogger logger, IOptions<SecretOptions> secretsOptions)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _secretsOptions = secretsOptions.Value;
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            //var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userId").Value;
            var foundUsers = await _context.GetAllAsync();
            var listToReturn = _mapper.Map<IEnumerable<ViewUserDto>>(foundUsers);
            return Ok(listToReturn);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneUser([FromRoute] Guid id)
        {
            var foundUser = await _context.GetFirstByExpressionAsync(x => x.Id == id);
            if (foundUser == null)
            {
                var err = new ErrorObject()
                {
                    Method = "GET",
                    At = $"/users/{id}",
                    StatusCode = 404,
                    Error = "Could not find user"
                };
                return NotFound(err);
            }

            return Ok(_mapper.Map<ViewUserDto>(foundUser));
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateOneUser([FromBody] User user)
        {
            user.Id = Guid.NewGuid();
            _context.Create(user);

            if (!await _context.Saveasync())
            {
                var errMsg = "Error creating user";
                _logger.Error(errMsg);
                var err = new ErrorObject()
                {
                    Method = "POST",
                    At = "/users",
                    StatusCode = 500,
                    Error = errMsg
                };
                return StatusCode(500, err);
            }
            return NoContent();
        }

        // TODO actually finish this method
        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto user, [FromRoute] Guid id)
        {
            var foundUser = await _context.GetFirstByExpressionAsync(x => x.Id == id);
            if (foundUser == null)
            {
                var err = new ErrorObject()
                {
                    Method = "PUT",
                    At = $"/users/{id}",
                    StatusCode = 404,
                    Error = "Could not find user to update"
                };
                return NotFound(err);
            }
            _mapper.Map(user, foundUser);
            foundUser.ModifiedOn = DateTime.Now;

            if (!await _context.Saveasync())
            {
                var errMsg = "Error creating user";
                _logger.Error(errMsg);
                var err = new ErrorObject()
                {
                    Method = "POST",
                    At = "/users",
                    StatusCode = 500,
                    Error = errMsg
                };
                return StatusCode(500, err);
            }
            return NoContent();
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOneUser([FromRoute]Guid id)
        {
            var foundUser = await _context.GetFirstByExpressionAsync(x => x.Id == id);
            if (foundUser == null)
            {
                var err = new ErrorObject()
                {
                    Method = "DELETE",
                    At = $"/users/{id}",
                    StatusCode = 404,
                    Error = "Could not find user to delete"
                };
                return NotFound(err);
            }

            _context.Delete(foundUser);

            if (!await _context.Saveasync())
            {
                var errMsg = "Error deleting user";
                _logger.Error(errMsg);
                var err = new ErrorObject()
                {
                    Method = "DELETE",
                    At = $"/users/{id}",
                    StatusCode = 500,
                    Error = errMsg
                };
                return StatusCode(500, err);
            }
            return Ok(_mapper.Map<ViewUserDto>(foundUser));

        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto user)
        {
            var foundUser = await _context.GetFirstByExpressionAsync(x => x.Username == user.Username);

            if (foundUser == null)
            {
                var err = new ErrorObject()
                {
                    Method = "POST",
                    At = "/users",
                    StatusCode = 404,
                    Error = "User not found"
                };
                return NotFound(err);
            }

            var authenticated = BC.Verify(user.Password, foundUser.Password);

            if (!authenticated)
            {
                var err = new ErrorObject()
                {
                    Method = "POST",
                    At = "/users/login",
                    StatusCode = 404,
                    Error = "Wrong Password"
                };
                return NotFound(err);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretsOptions.Signature);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userId", foundUser.Id.ToString()),
                    new Claim(ClaimTypes.Role, foundUser.Role),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _secretsOptions.Issuer,
                Audience = _secretsOptions.Audience,
                
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenStr = tokenHandler.WriteToken(token);

            return Ok(new { token = tokenStr });
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto newUser)
        {
            newUser.Password = BC.HashPassword(newUser.Password);
            var userToCreate = _mapper.Map<User>(newUser);
            userToCreate.Id = Guid.NewGuid();
            userToCreate.Role = "User";

            _context.Create(userToCreate);

            if (!await _context.Saveasync())
            {
                var errMsg = "Error registering user";
                _logger.Error(errMsg);
                var err = new ErrorObject()
                {
                    Method = "POST",
                    At = "/users/register",
                    StatusCode = 500,
                    Error = errMsg
                };
                return StatusCode(500,err);
            }

            return NoContent();
        }
    }
}
