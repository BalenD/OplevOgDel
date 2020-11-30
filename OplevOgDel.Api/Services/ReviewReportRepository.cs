using Microsoft.EntityFrameworkCore;
using OplevOgDel.Api.Data;
using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Services.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Services
{
    /// <summary>
    /// Implementation for the repository handling "reviewreport" table calls
    /// </summary>
    public class ReviewReportRepository : RepositoryBase<ReviewReport>, IReviewReportRepository
    {
        public ReviewReportRepository(OplevOgDelDbContext context) : base (context)
        {

        }

        /// <summary>
        /// Get the reports of a review by id
        /// </summary>
        /// <param name="reviewId">Id of the review to get reports for</param>
        public async Task<IEnumerable<ReviewReport>> GetReportsForReview(Guid reviewId)
        {
            return await _context.ReviewReports.Where(x => x.ReviewId == reviewId).ToListAsync();
        }
    }
}
