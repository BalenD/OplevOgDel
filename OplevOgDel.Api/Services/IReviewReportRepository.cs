using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Services.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Services
{
    public interface IReviewReportRepository : IRepositoryBase<ReviewReport>
    {
        Task<IEnumerable<ReviewReport>> GetReportsForReview(Guid reviewId);
    }
}
