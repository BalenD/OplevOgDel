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
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public PictureController(IOptions<FileUploadOptions> fileOptions, IPictureRepository context, ILogger logger, IMapper mapper)
        {
            _fileOptions = fileOptions.Value;
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetPictures([FromRoute] Guid experienceId)
        {
            var allPictures = await _context.GetAllByExperienceAsync(experienceId);
            var listToReturn = _mapper.Map<IEnumerable<ViewPictureDto>>(allPictures);
            return Ok(listToReturn);
        }

        [HttpGet("{name}")]
        public IActionResult GetOnePicture([FromRoute] string name)
        {
            var image = System.IO.File.OpenRead(Path.Combine(_fileOptions.Path, name));
            return File(image, "image/jpeg");
        }

        [HttpPost]
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

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteOnePicture([FromRoute] Guid id)
        {
            var pictureToDelete = await _context.GetFirstByExpressionAsync(x => x.Id == id);
            
            if (pictureToDelete == null)
            {
                return NotFound();
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
