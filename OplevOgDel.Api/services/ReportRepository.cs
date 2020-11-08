using OplevOgDel.Api.Data;
using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.services.RepositoryBase;

namespace OplevOgDel.Api.services
{
    public class ReportRepository : RepositoryBase<Report>, IReportRepository
    {
        public ReportRepository(OplevOgDelDbContext context) : base (context)
        {

        }
    }
}
