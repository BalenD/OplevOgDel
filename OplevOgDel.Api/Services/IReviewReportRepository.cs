using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Services.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Services
{
    /// <summary>
    /// Interface for teh repository handling "reviewreports" table calls
    /// </summary>
    public interface IReviewReportRepository : IRepositoryBase<ReviewReport>
    {
        /// <summary>
        /// Get the reports of a review by id
        /// </summary>
        /// <param name="reviewId">Id of the review to get reports for</param>
        Task<IEnumerable<ReviewReport>> GetReportsForReview(Guid reviewId);
    }
}
