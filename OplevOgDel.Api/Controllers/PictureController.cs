using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using KissLog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Models.Configuration;
using OplevOgDel.Api.Models.Dto.PictureDto;
using OplevOgDel.Api.Services;

namespace OplevOgDel.Api.Controllers
{
    [Route("api/experiences/{experienceId}/pictures/")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        private readonly FileUploadOptions _fileOptions;
        private readonly IPictureRepository _context;
        private readonly IExperienceRepository _exprContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public PictureController(IOptions<FileUploadOptions> fileOptions, IPictureRepository context, IExperienceRepository exprContext, ILogger logger, IMapper mapper)
        {
            _fileOptions = fileOptions.Value;
            _context = context;
            _exprContext = exprContext;
            _logger = logger;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets a JSON list of all the images with the name of the picture on local disk
        /// </summary>
        /// <param name="experienceId">The id of the experience attached to the pictures</param>
        /// <returns>A  JSON list of picture names</returns>
        /// <response code="200">Returns all the pictures</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<ViewPictureDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPictures([FromRoute] Guid experienceId)
        {
            var allPictures = await _context.GetAllByExperienceAsync(experienceId);
            var listToReturn = _mapper.Map<IEnumerable<ViewPictureDto>>(allPictures);
            return Ok(listToReturn);
        }

        /// <summary>
        /// Gets the actual picture on the disk
        /// </summary>
        /// <param name="name">name of the picture on disk to get</param>
        /// <returns>The picture</returns>
        /// <response code="200">Returns the picture</response>
        /// <response code="500">If a problem occurs during retrieval</response>
        [HttpGet("{name}")]
        [ProducesResponseType(typeof(FileStream), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetOnePicture([FromRoute] string name)
        {
            try
            {
                var image = System.IO.File.OpenRead(Path.Combine(_fileOptions.Path, name));
                return File(image, "image/jpeg");
            }
            catch (Exception)
            {
                _logger.Error("Exception thrown when trying to retrieve a picture");
                return Problem("Failed to retrieve picture");
                
            }
        }

        /// <summary>
        /// Add a picture to an experience 
        /// </summary>
        /// <param name="experienceId">Id of the experience to add it to</param>
        /// <param name="files">One or more pictures to add</param>
        /// <returns></returns>
        /// <response code="201">Successfully added the picture</response>
        /// <response code="400">If the file is less than 1 or more than 3</response>
        /// <response code="404">if the experience does not exist</response>
        /// <response code="500">If a problem occurs during creation</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePictures([FromRoute] Guid experienceId, [FromForm] List<IFormFile> files)
        {
            if (files.Count < 1)
            {
                return BadRequest("No filed sent");
            }

            if (files.Count > 3)
            {
                return BadRequest("Too many files sent");
            }

            if (await _exprContext.GetFirstByExpressionAsync(x => x.Id == experienceId) == null)
            {
                return NotFound();
            }

            foreach (var file in files)
            {
                var path = Path.Combine(_fileOptions.Path, $"{Guid.NewGuid()}_{DateTime.UtcNow.Ticks}.jpg");

                // TODO make them run concurrently
                _context.Create(new Picture
                {
                    Path = path,
                    CreatedOn = DateTime.UtcNow,
                    ExperienceId = experienceId,
                    // TODO: add profile here as well
                });

                if (!await _context.Saveasync())
                {
                    _logger.Error("Failed to create picture");
                    return Problem();
                }

                try
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                catch (Exception)
                {
                    _logger.Error("Exception thrown when trying to save a picture");
                    return Problem("Failed to create a picture");
                }
            }
            return Ok();
        }

        /// <summary>
        /// Deletes an image
        /// </summary>
        /// <param name="id">ID of the image to delete</param>
        /// <returns>the deleted images DB object</returns>'
        /// <response code="200">Successfully returned the deleted picture object</response>
        /// <response code="404">If no picture object is found</response>
        /// <response code="500">If a problem occurs during deletion</response>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ViewPictureDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteOnePicture([FromRoute] Guid id)
        {
            var pictureToDelete = await _context.GetFirstByExpressionAsync(x => x.Id == id);
            
            if (pictureToDelete == null)
            {
                return NotFound();
            }

            var path = Path.Combine(_fileOptions.Path, pictureToDelete.Path);

            if (System.IO.File.Exists(path))
            {
                try
                {
                    System.IO.File.Delete(path);
                }
                catch (Exception)
                {
                    _logger.Error("Exception thrown when trying to delete a picture");
                    return Problem("Failed deleting image");
                }
                
            }

            _context.Delete(pictureToDelete);

            if (!await _context.Saveasync())
            {
                _logger.Error("Failed to delete picture");
                return Problem();
            }
            return Ok(pictureToDelete);
        }
    }
}
