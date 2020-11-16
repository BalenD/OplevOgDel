using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KissLog;
using AutoMapper;
using OplevOgDel.Api.Services;
using OplevOgDel.Api.Models.Dto.ReviewDto;
using OplevOgDel.Api.Data.Models;

namespace OplevOgDel.Api.Controllers
{
    [Route("api/experiences/{experienceId}/reviews")]
    [Produces("application/json")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IReviewRepository _context;
        private readonly IMapper _mapper;

        public ReviewController(ILogger logger, IReviewRepository context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all reviews that belong to an experience
        /// </summary>
        /// <returns>Returns all reviews</returns>
        /// <response code="200">Returns reviews belonging to an experience</response>
        [HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllReviews([FromRoute] Guid experienceId)
        {
            var allReviews = await _context.GetAllReviews(experienceId);
            var listToReturn = _mapper.Map<IEnumerable<ViewReviewDto>>(allReviews);
            return Ok(listToReturn);
        }

        /// <summary>
        /// Gets a review by ID
        /// </summary>
        /// <returns>Returns the found reviewe</returns>
        /// <param name="id">Id of review to get</param>
        /// <response code="200">Successfully returned the found review</response>
        /// <response code="404">If no review is found</response>     
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ViewOneReviewDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOneReview([FromRoute] Guid id)
        {
            var foundReview = await _context.GetAReview(id);
            
            if (foundReview == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ViewOneReviewDto>(foundReview));
        }

        /// <summary>
        /// Create a review
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/experiences/:experienceId/reviews
        ///     {
        ///        "Description": "A beautiful place"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created a review</response>
        /// <response code="500">If a problem occurs during creation</response>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOneReview([FromRoute] Guid experienceId, [FromBody] NewReviewDto createdReview)
        {
            var reviewToAdd = _mapper.Map<Review>(createdReview);
            reviewToAdd.Id = Guid.NewGuid();
            reviewToAdd.ExperienceId = experienceId;
            // TODO: add creatoer of review here

            _context.Create(reviewToAdd);

            if (!await _context.Saveasync())
            {
                _logger.Error("Failed to create review");
                return Problem();
            }

            return CreatedAtAction(nameof(GetOneReview), new { experienceId, id = reviewToAdd.Id });
        }

        /// <summary>
        /// Update an experience
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/experiences/:experienceId:/reviews/:id
        ///     {
        ///        "Description": "a very nice place"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Id of experience to update</param>
        /// <param name="updatedReview">new review object to update</param>
        /// <response code="204">Successfully updated an experience</response>
        /// <response code="404">Can't find the experience to update</response>
        /// <response code="500">If a problem occurs during update</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOneReview([FromRoute] Guid id, [FromBody] EditReviewDto updatedReview)
        {
            var reviewFromDb = await _context.GetAReview(id);
            if (reviewFromDb == null)
            {
                return NotFound();
            }

            _mapper.Map(updatedReview, reviewFromDb);

            _context.Update(reviewFromDb);

            if (!await _context.Saveasync())
            {
                _logger.Error("Failed to update review");
                return Problem();
            }
            return NoContent();
        }

        /// <summary>
        /// Deletes a review by ID
        /// </summary>
        /// <returns>Returns the deleted review</returns>
        /// <param name="id">Id of experience to delete</param>
        /// <response code="200">Successfully returned the deleted experience</response>
        /// <response code="404">If no experience is found</response>
        /// <response code="500">If a problem occurs during update</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ViewOneReviewDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteOneReview([FromRoute] Guid id)
        {
            var reviewToDelete = await _context.GetFirstByExpressionAsync(x => x.Id == id);
            
            if (reviewToDelete == null)
            {
                return NotFound();
            }

            _context.Delete(reviewToDelete);

            if (!await _context.Saveasync())
            {
                _logger.Error("Failed to delete review");
                return Problem();
            }
            return Ok(_mapper.Map<ViewOneReviewDto>(reviewToDelete));
        }
    }
}
