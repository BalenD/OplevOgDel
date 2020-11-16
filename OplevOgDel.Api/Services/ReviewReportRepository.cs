using OplevOgDel.Api.Data;
using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Services.RepositoryBase;

namespace OplevOgDel.Api.Services
{
    public class ReviewReportRepository : RepositoryBase<ReviewReport>, IReviewReportRepository
    {
        public ReviewReportRepository(OplevOgDelDbContext context) : base (context)
        {

        }
    }
}
