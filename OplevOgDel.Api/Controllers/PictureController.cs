using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KissLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Helpers;
using OplevOgDel.Api.Models.Configuration;
using OplevOgDel.Api.Models.Dto;
using OplevOgDel.Api.Models.Dto.PictureDto;
using OplevOgDel.Api.Services;

namespace OplevOgDel.Api.Controllers
{
    /// <summary>
    /// The controller that handles all aPI calls to /api/experiences/:experienceId/pictures
    /// </summary>
    [Route("api/experiences/{experienceId}/pictures/")]
    [Produces("application/json")]
    [Authorize(Roles = Roles.AdminAndUser)]
    [ApiController]
    public class PictureController : ControllerBase
    {
        private readonly FileUploadOptions _fileOptions;
        private readonly IPictureRepository _pictureRepository;
        private readonly IExperienceRepository _experienceRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public PictureController(IOptions<FileUploadOptions> fileOptions, IPictureRepository pictureRepository, IExperienceRepository experienceRepository, ILogger logger, IMapper mapper)
        {
            _fileOptions = fileOptions.Value;
            _pictureRepository = pictureRepository;
            _experienceRepository = experienceRepository;
            _logger = logger;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets a JSON list of all the pictures with the name of the picture on local disk
        /// </summary>
        /// <param name="experienceId">Id of the experience to get the pictures of</param>
        /// <response code="200">Returns all the picture objects</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ViewPictureDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPictures([FromRoute] Guid experienceId)
        {
            var allPictures = await _pictureRepository.GetAllByExperienceAsync(experienceId);
            var listToReturn = _mapper.Map<IEnumerable<ViewPictureDto>>(allPictures);
            return Ok(listToReturn);
        }

        /// <summary>
        /// Gets the actual picture on the disk
        /// </summary>
        /// <param name="experienceId">Id of the experience which the picture belongs to</param>
        /// <param name="name">Name of the picture on disk to get</param>
        /// <response code="200">Returns the picture</response>
        /// <response code="500">Problem occured during retrieval</response>
        [HttpGet("{name}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status500InternalServerError)]
        public IActionResult GetOnePicture([FromRoute] Guid experienceId, [FromRoute] string name)
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
                    At = $"/api/experiences/{experienceId}/pictures/{name}",
                    StatusCode = 500,
                    Error = errMsg
                };
                _logger.Error(errMsg);
                return StatusCode(500, err);
                
            }
        }

        /// <summary>
        /// Add a picture to an experience 
        /// </summary>
        /// <param name="experienceId">Id of the experience to add it to</param>
        /// <param name="files">One or more pictures to add</param>
        /// <response code="204">Successfully added the picture(s)</response>
        /// <response code="400">There is less than 1 or more than 3 files</response>
        /// <response code="404">The experience does not exist</response>
        /// <response code="500">Problem occured during creation</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorObject),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePictures([FromRoute] Guid experienceId, [FromForm] List<IFormFile> files)
        {
            var err = new ErrorObject()
            {
                Method = "POST",
                At = $"/api/experiences/{experienceId}/pictures",
                StatusCode = 400
            };
            
            if (files.Count < 1)
            {
                err.Error = "No file sent";
                return BadRequest(err);
            }

            if (files.Count > 3)
            {
                err.Error = "Too many files sent";
                return BadRequest(err);
            }

            if (await _experienceRepository.GetFirstByExpressionAsync(x => x.Id == experienceId) == null)
            {
                err.StatusCode = 404;
                err.Error = "No experience found";
                return NotFound(err);
            }

            // get the amount of numbers to make sure we don't cross 3 pictures to an experience
            var pictures = await _pictureRepository.GetAllByExperienceAsync(experienceId);
            if (pictures.Count() == 3)
            {
                err.Error = "There is already 3 pictures, please remove one before adding another";
                return BadRequest(err);
            }

            foreach (var file in files)
            {
                var path = Path.Combine(_fileOptions.Path, $"{Guid.NewGuid()}_{DateTime.UtcNow.Ticks}.jpg");
                
                // save the picture object in database
                _pictureRepository.Create(new Picture
                {
                    Path = path,
                    CreatedOn = DateTime.UtcNow,
                    ExperienceId = experienceId,
                    
                });

                if (!await _pictureRepository.SaveAsync())
                {
                    err.Error = "Error on saving picture object";
                    err.StatusCode = 500;
                    _logger.Error(err.Error);
                    return StatusCode(500, err);
                }

                // save the  picture on disk
                try
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                catch (Exception)
                {
                    err.Error = "Error on saving picture on disk";
                    err.StatusCode = 500;
                    _logger.Error(err.Error);
                    return StatusCode(500, err);
                }
            }
            return NoContent();
        }

        /// <summary>
        /// Delete a picture
        /// </summary>
        /// <param name="experienceId">Id of the experience which the picture belongs to</param>
        /// <param name="id">Id of the picture to delete</param>
        /// <response code="204">Successfully deleted picture</response>
        /// <response code="404">No picture object is found</response>
        /// <response code="500">Problem occured during deletion</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteOnePicture([FromRoute] Guid experienceId,[FromRoute] Guid id)
        {

            var pictureToDelete = await _pictureRepository.GetFirstByExpressionAsync(x => x.Id == id);
            
            if (pictureToDelete == null)
            {
                var err = new ErrorObject()
                {
                    Method = "DELETE",
                    At = $"/api/experiences/{experienceId}/pictures/{id}",
                    StatusCode = 404,
                    Error = "Could not find picture to delete"
                };
                return NotFound(err);
            }

            var path = Path.Combine(_fileOptions.Path, pictureToDelete.Path);

            // if the file exists, then delete it from disk
            if (System.IO.File.Exists(path))
            {
                try
                {
                    System.IO.File.Delete(path);
                }
                catch (Exception)
                {
                    var errMsg = "Error on deleton";
                    var err = new ErrorObject()
                    {
                        Method = "DELETE",
                        At = $"/api/experiences/{experienceId}/pictures/{id}",
                        StatusCode = 500,
                        Error = errMsg
                    };
                    _logger.Error(errMsg);
                    return StatusCode(500, err);
                }
                
            }

            // delete the picture object from DB too
            _pictureRepository.Delete(pictureToDelete);

            if (!await _pictureRepository.SaveAsync())
            {
                var errMsg = "Error on deleton";
                var err = new ErrorObject()
                {
                    Method = "DELETE",
                    At = $"/api/experiences/{experienceId}/pictures/{id}",
                    StatusCode = 500,
                    Error = errMsg
                };
                _logger.Error(errMsg);
                return StatusCode(500, err);
            }
            return NoContent();
        }
    }
}
