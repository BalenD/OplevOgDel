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
    [Route("api/reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IExperienceReportRepository _experienceReportRepository;
        private readonly IReviewReportRepository _reviewReportRepository;
        private readonly IExperienceRepository _experienceRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReportController(ILogger logger, 
            IExperienceReportRepository experienceReportRepository, 
            IReviewReportRepository reviewReportRepository, 
            IExperienceRepository experienceRepository, 
            IReviewRepository reviewRepository,
            IMapper mapper)
        {
            _logger = logger;
            _experienceReportRepository = experienceReportRepository;
            _reviewReportRepository = reviewReportRepository;
            _experienceRepository = experienceRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetExperiencesWithReports()
        {
            var allExpsWithReports = await _experienceRepository.GetExperiencesWithReports();

            if (allExpsWithReports == null)
            {
                return NotFound();
            }

            return Ok(allExpsWithReports);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneExperienceWithReports(Guid id)
        {
            var expAndReports = await _experienceRepository.GetAnExperienceAndReports(id);

            if (expAndReports == null)
            {
                return NotFound();
            }

            return Ok(expAndReports);
        }

        [HttpDelete("experience/{id}")]
        public async Task<IActionResult> DeleteReportsForExperience(Guid id)
        {
            var exp = await _experienceRepository.GetFirstByExpressionAsync(r => r.Id == id);

            if (exp == null)
            {
                return NotFound();
            }

            //await _experienceReportRepository.

            return NoContent();
        }

        [HttpDelete("review/{id}")]
        public async Task<IActionResult> DeleteReportsForReview(Guid id)
        {
            var review = await _reviewRepository.GetFirstByExpressionAsync(r => r.Id == id);

            if (review == null)
            {
                return NotFound();
            }

            //await _reviewReportRepository.

            return NoContent();
        }
    }
}
