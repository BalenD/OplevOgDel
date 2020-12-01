using KissLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OplevOgDel.Api.Helpers;
using OplevOgDel.Api.Models.Configuration;
using System;
using System.IO;

namespace OplevOgDel.Api.Controllers
{
    /// <summary>
    /// The controller that handles API call to retrieve an image
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly FileUploadOptions _fileOptions;
        private readonly ILogger _logger;

        public ImagesController(IOptions<FileUploadOptions> fileOptions, ILogger logger)
        {
            _fileOptions = fileOptions.Value;
            _logger = logger;
        }

        /// <summary>
        /// Gets the actual image on the disk
        /// </summary>
        /// <param name="name">Name of the picture on disk to get</param>
        /// <response code="200">Returns the picture</response>
        /// <response code="500">Problem occured during retrieval</response>
        [HttpGet("{name}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status500InternalServerError)]
        public IActionResult GetOneImage([FromRoute] string name)
        {
            try
            {
                // find the image file on disk and return it
                var image = System.IO.File.OpenRead(Path.Combine(_fileOptions.Path, name));
                return File(image, "image/jpeg");
            }
            catch (Exception)
            {
                var errMsg = "Error getting picture";
                var err = new ErrorObject()
                {
                    Method = "GET",
                    At = $"/api/images/{name}",
                    StatusCode = 500,
                    Error = errMsg
                };
                _logger.Error(errMsg);
                return StatusCode(500, err);

            }
        }
    }
}
