using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KissLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Helpers;
using OplevOgDel.Api.Models.Dto;
using OplevOgDel.Api.Services;

namespace OplevOgDel.Api.Controllers
{
    /// <summary>
    /// The controller that handles all API calls to /api/reports
    /// </summary>
    [Authorize(Roles = Roles.Admin)]
    [Route("api/reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IExperienceReportRepository _experienceReportRepository;
        private readonly IReviewReportRepository _reviewReportRepository;
        private readonly IExperienceRepository _experienceRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReportController(ILogger logger, 
            IExperienceReportRepository experienceReportRepository, 
            IReviewReportRepository reviewReportRepository, 
            IExperienceRepository experienceRepository, 
            IReviewRepository reviewRepository)
        {
            _logger = logger;
            _experienceReportRepository = experienceReportRepository;
            _reviewReportRepository = reviewReportRepository;
            _experienceRepository = experienceRepository;
            _reviewRepository = reviewRepository;
        }

        /// <summary>
        /// Get all the experiences with reports
        /// </summary>
        /// <response code="200">Returns all the experiences</response>
        /// <response code="404">No experience with a report was found</response>   
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Experience>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetExperiencesWithReports()
        {
            var allExpsWithReports = await _experienceRepository.GetExperiencesWithReportsAsync();

            if (allExpsWithReports == null)
            {
                var err = new ErrorObject()
                {
                    Method = "GET",
                    At = "/api/reports",
                    StatusCode = 404,
                    Error = "Could not find experiences"
                };
                return NotFound(err);
            }

            return Ok(allExpsWithReports);
        }

        /// <summary>
        /// Get an experience with reviews (that have reports) and reports
        /// </summary>
        /// <param name="id">Id of the experience to get</param>
        /// <response code="200">Successfully returned the found experience from the database</response>
        /// <response code="404">No experience with a report was found</response>   
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Experience), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOneExperienceWithReviewsAndReports(Guid id)
        {
            var expAndReports = await _experienceRepository.GetAnExperienceAndReportsAsync(id);

            if (expAndReports == null)
            {
                var err = new ErrorObject()
                {
                    Method = "GET",
                    At = $"/api/reports/{id}",
                    StatusCode = 404,
                    Error = "Could not find experiences"
                };
                return NotFound(err);
            }

            return Ok(expAndReports);
        }

        /// <summary>
        /// Delete experience reports by experience id
        /// </summary>
        /// <param name="id">Id of the experience</param>
        /// <response code="204">Successfully deleted reports</response>
        /// <response code="404">No expereince was found</response>
        /// <response code="500">Problem occured during deletion</response>
        [HttpDelete("experience/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteReportsForExperience(Guid id)
        {
            var exp = await _experienceRepository.GetFirstByExpressionAsync(r => r.Id == id);

            if (exp == null)
            {
                var err = new ErrorObject()
                {
                    Method = "DELETE",
                    At = $"/api/reports/experience/{id}",
                    StatusCode = 404,
                    Error = "Could not find experience"
                };
                return NotFound(err);
            }

            var foundReports = await _experienceReportRepository.GetReportsForExperience(id);
            _experienceReportRepository.DeleteMany(foundReports);

            if (!await _experienceReportRepository.SaveAsync())
            {
                var errMsg = "Error deleting reports";
                _logger.Error(errMsg);
                var err = new ErrorObject()
                {
                    Method = "DELETE",
                    At = $"/api/reports/experience/{id}",
                    StatusCode = 500,
                    Error = errMsg
                };
                return StatusCode(500, err);
            }

            return NoContent();
        }
        /// <summary>
        /// Delete review reports by review id
        /// </summary>
        /// <param name="id">Id of the experience</param>
        /// <response code="204">Successfully deleted reports</response>
        /// <response code="404">No review was found</response>
        /// <response code="500">Problem occured during deletion</response>
        [HttpDelete("review/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteReportsForReview(Guid id)
        {
            var review = await _reviewRepository.GetFirstByExpressionAsync(r => r.Id == id);

            if (review == null)
            {
                var err = new ErrorObject()
                {
                    Method = "DELETE",
                    At = $"/api/reports/review/{id}",
                    StatusCode = 404,
                    Error = "Could not find review"
                };
                return NotFound(err);
            }

            var foundReports = await _reviewReportRepository.GetReportsForReview(id);
            _reviewReportRepository.DeleteMany(foundReports);

            if (!await _experienceReportRepository.SaveAsync())
            {
                var errMsg = "Error deleting reports";
                _logger.Error(errMsg);
                var err = new ErrorObject()
                {
                    Method = "DELETE",
                    At = $"/api/reports/review/{id}",
                    StatusCode = 500,
                    Error = errMsg
                };
                return StatusCode(500, err);
            }

            return NoContent();
        }
    }
}
