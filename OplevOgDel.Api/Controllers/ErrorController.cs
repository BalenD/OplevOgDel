using Microsoft.AspNetCore.Mvc;

namespace OplevOgDel.Api.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {

        [Route("/error")]
        [HttpGet]
        public IActionResult Error() => Problem();
    }
}
