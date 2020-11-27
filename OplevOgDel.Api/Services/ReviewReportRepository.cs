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
    public class ReviewReportRepository : RepositoryBase<ReviewReport>, IReviewReportRepository
    {
        public ReviewReportRepository(OplevOgDelDbContext context) : base (context)
        {

        }

        public async Task<IEnumerable<ReviewReport>> GetReportsForReview(Guid reviewId)
        {
            return await _context.ReviewReports.Where(x => x.ReviewId == reviewId).ToListAsync();
        }
    }
}
