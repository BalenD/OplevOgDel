using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OplevOgDel.Api.Helpers;

namespace OplevOgDel.Api.Controllers
{
    /// <summary>
    /// The controller that handles error returns
    /// </summary>
    [Route("/error")]
    [Produces("application/json")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// Get an error message when the server has unexpectedly crashed in production
        /// </summary>
        /// 
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status500InternalServerError)]
        public IActionResult Error() 
        {
            var err = new ErrorObject()
            {
                StatusCode = 500,
                Error = "An error has occured with your request. Recheck your request or try at another time"
            };
            return StatusCode(500, err);
        }
    }
}
