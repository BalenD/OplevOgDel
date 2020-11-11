using Microsoft.AspNetCore.Mvc;

namespace OplevOgDel.Api.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {

        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}
