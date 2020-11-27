using AutoMapper;
using KissLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Helpers;
using OplevOgDel.Api.Models.Configuration;
using OplevOgDel.Api.Models.Dto;
using OplevOgDel.Api.Models.Dto.UserDto;
using OplevOgDel.Api.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace OplevOgDel.Api.Controllers
{
    /// <summary>
    /// The controller that handles all API calls to /api/account
    /// </summary>
    [Route("api/account")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly SecretOptions _secretsOptions;

        public AccountController(IUserRepository context, IMapper mapper, ILogger logger, IOptions<SecretOptions> secretsOptions)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _secretsOptions = secretsOptions.Value;
        }

        /// <summary>
        /// Get a token by logging in
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <response code="200">Successfully authenticated user</response>
        /// <response code="404">Wrong user or password</response>   
        [HttpPost("login")]
        [ProducesResponseType(typeof(Token), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] UserLoginDto user)
        {
            // check if the user(name) exists, if they don't return an error
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

            // verify that the password matches, if they don't return an error
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

            // create the JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            // get out signature to sign the token
            var key = Encoding.ASCII.GetBytes(_secretsOptions.Signature);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                // add claims (strings of information along with token)
                Subject = new ClaimsIdentity(new Claim[]
                {
                    
                    new Claim("userId", foundUser.Id.ToString()),
                    new Claim("profileId", foundUser.ProfileId.ToString()),
                    new Claim(ClaimTypes.Role, foundUser.Role),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                // how long the token can be used for
                Expires = DateTime.UtcNow.AddHours(4),
                // sign the token with our key
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                // add who issued the token to which audience
                Issuer = _secretsOptions.Issuer,
                Audience = _secretsOptions.Audience,

            };

            // create the token, turn it into a string and return it
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenStr = tokenHandler.WriteToken(token);

            return Ok(new Token { Jwt = tokenStr });
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto newUser)
        {
            // hash the password using bcrypt
            newUser.Password = BC.HashPassword(newUser.Password);

            // map the user to the proper model entity
            var userToCreate = _mapper.Map<User>(newUser);
            // add necessary ID and role
            userToCreate.Id = Guid.NewGuid();
            userToCreate.Role = Roles.User;

            // add the newly created user entity into the database
            _context.Create(userToCreate);

            if (!await _context.SaveAsync())
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
                return StatusCode(500, err);
            }

            return NoContent();
        }
    }
}
