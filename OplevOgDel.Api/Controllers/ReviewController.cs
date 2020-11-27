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
using Microsoft.AspNetCore.Authorization;
using OplevOgDel.Api.Models.Dto;
using OplevOgDel.Api.Helpers;
using System.Linq;

namespace OplevOgDel.Api.Controllers
{
    /// <summary>
    /// The controllere that handles all API calls to /api/experiences/:id/reviews
    /// </summary>
    [Route("api/experiences/{experienceId}/reviews")]
    [Produces("application/json")]
    [Authorize(Roles = Roles.User)]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewController(ILogger logger, IReviewRepository repository, IMapper mapper)
        {
            _logger = logger;
            _reviewRepository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all the reviews that belong to an experience
        /// </summary>
        /// <response code="200">Returns all reviews that belong to an experience</response>
        [HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllReviews([FromRoute] Guid experienceId)
        {
            // get all the reviews that belong to an experience
            var allReviews = await _reviewRepository.GetAllReviewsAsync(experienceId);
            // map it to the view DTO and return
            var listToReturn = _mapper.Map<IEnumerable<ViewReviewDto>>(allReviews);
            return Ok(listToReturn);
        }

        /// <summary>
        /// Get a review by id
        /// </summary>
        /// <param name="id">Id of review to get</param>
        /// /// <param name="experienceId">Id of the experience which the review belongs to</param>
        /// <response code="200">Successfully returned the found review</response>
        /// <response code="404">No review was found</response>     
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ViewOneReviewDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOneReview([FromRoute] Guid experienceId,[FromRoute] Guid id)
        {
            // find the review with the id and if none is found return error
            var foundReview = await _reviewRepository.GetAReviewAsync(id);
            
            if (foundReview == null)
            {
                var err = new ErrorObject()
                {
                    Method = "GET",
                    At = $"/api/experiences/{experienceId}/reviews/{id}",
                    StatusCode = 404,
                    Error = "Could not find review"
                };
                return NotFound(err);
            }

            // map and return the fund review
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
        /// <response code="201">Successfully created the review</response>
        /// <response code="500">Problem occured during creation</response>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOneReview([FromRoute] Guid experienceId, [FromBody] NewReviewDto createdReview)
        {
            // map the incomming review dto to the review entity
            var reviewToAdd = _mapper.Map<Review>(createdReview);
            // add the necessary relations and id
            reviewToAdd.Id = Guid.NewGuid();
            reviewToAdd.ExperienceId = experienceId;
            var profileId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "profileId").Value;
            reviewToAdd.ProfileId = Guid.Parse(profileId);
            reviewToAdd.CreatedOn = DateTime.Now;

            // save in database
            _reviewRepository.Create(reviewToAdd);

            if (!await _reviewRepository.SaveAsync())
            {
                var errMSg = "Error creating a review";
                _logger.Error(errMSg);
                var err = new ErrorObject()
                {
                    Method = "POST",
                    At = $"/api/experiences/{experienceId}/reviews",
                    StatusCode = 500,
                    Error = errMSg
                };
                return StatusCode(500, err);
            }

            return CreatedAtAction(nameof(GetOneReview), new { experienceId, id = reviewToAdd.Id }, _mapper.Map<ViewReviewDto>(reviewToAdd));
        }

        /// <summary>
        /// Update a review by id
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
        /// <param name="experienceId">Id of the experience which the review belongs to</param>
        /// <param name="id">Id of experience to update</param>
        /// <param name="updatedReview">new review object to update</param>
        /// <response code="204">Successfully updated an experience</response>
        /// <response code="404">No review was founde</response>
        /// <response code="500">Problem occured during update</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOneReview([FromRoute] Guid experienceId,[FromRoute] Guid id, [FromBody] EditReviewDto updatedReview)
        {
            // find the review to update, if review does not exist return error
            var reviewFromDb = await _reviewRepository.GetAReviewAsync(id);
            if (reviewFromDb == null)
            {
                var err = new ErrorObject()
                {
                    Method = "PUT",
                    At = $"/api/experiences/{experienceId}/reviews/{id}",
                    StatusCode = 404,
                    Error = "Could not find review to update"

                };
                return NotFound(err);
            }
            // map the changes to the found review object
            _mapper.Map(updatedReview, reviewFromDb);

            reviewFromDb.ModifiedOn = DateTime.Now;

            // update the entity in the database
            _reviewRepository.Update(reviewFromDb);

            if (!await _reviewRepository.SaveAsync())
            {
                var errMsg = "Error updating review";
                _logger.Error(errMsg);
                var err = new ErrorObject()
                {
                    Method = "PUT",
                    At = $"*/api/experiences/{experienceId}/reviews/{id}",
                    StatusCode = 500,
                    Error = errMsg
                };
                return StatusCode(500, err);
            }
            return NoContent();
        }

        /// <summary>
        /// Delete a review by id
        /// </summary>
        /// <param name="experienceId">Id of the experience which the review belongs to</param>
        /// <param name="id">Id of review to delete</param>
        /// <response code="200">Successfully returned the deleted experience</response>
        /// <response code="404">No review was found</response>
        /// <response code="500">Problem occured during update</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ViewOneReviewDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorObject), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteOneReview([FromRoute] Guid experienceId,[FromRoute] Guid id)
        {

            // f ind the review to delete, if no review is found return error
            var reviewToDelete = await _reviewRepository.GetFirstByExpressionAsync(x => x.Id == id);
            
            if (reviewToDelete == null)
            {
                var err = new ErrorObject() 
                {
                    Method = "DELETE",
                    At = $"*/api/experiences/{experienceId}/reviews/{id}",
                    StatusCode = 404,
                    Error = "Could not find review to delete"
                };
                return NotFound(err);
            }

            // delete the found review
            _reviewRepository.Delete(reviewToDelete);

            if (!await _reviewRepository.SaveAsync())
            {
                var errMsg = "Error deleting review";
                _logger.Error(errMsg);
                var err = new ErrorObject()
                {
                    Method = "DELETE",
                    At = $"*/api/experiences/{experienceId}/reviews/{id}",
                    StatusCode = 500,
                    Error = errMsg
                };
                return StatusCode(500, err);
            }

            // map the deleted review and return it
            return Ok(_mapper.Map<ViewOneReviewDto>(reviewToDelete));
        }
    }
}
