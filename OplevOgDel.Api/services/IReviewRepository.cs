using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Models.Dto.RequestDto;
using OplevOgDel.Api.Services.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Services
{
    /// <summary>
    /// Interface for the repository handling "review" table calls
    /// </summary>
    public interface IReviewRepository : IRepositoryBase<Review>
    {
        /// <summary>
        /// Gets all reviews of an experience in the database, in a paged order,
        /// by the experience id
        /// </summary>
        /// <param name="req">Filtering  and searching parameters</param>
        /// <param name="experienceId">Id of the experience to get reviews for</param>
        Task<IEnumerable<Review>> GetAllReviewsAsync(ReviewRequestParametersDto req, Guid experienceId);
        /// <summary>
        /// Get one review by id
        /// </summary>
        /// <param name="id">Id of the review to get</param>
        Task<Review> GetAReviewAsync(Guid id);
    }
}
