using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KissLog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OplevOgDel.Api.Services;

namespace OplevOgDel.Api.Controllers
{
    [Route("api/profiles")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        public ProfileController(ILogger logger, IProfileRepository profileRepository, IMapper mapper)
        {
            _logger = logger;
            _profileRepository = profileRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneProfile([FromRoute] Guid id)
        {
            var foundProfile = await _profileRepository.GetAProfileAsync(id);

            if (foundProfile == null)
            {
                return NotFound();
            }

            //return Ok(_mapper.Map<ViewOneExperienceDto>(foundProfile));
            return Ok(foundProfile);
        }
    }
}
