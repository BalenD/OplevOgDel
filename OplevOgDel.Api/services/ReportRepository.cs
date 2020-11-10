using OplevOgDel.Api.Data;
using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Services.RepositoryBase;

namespace OplevOgDel.Api.Services
{
    public class ReportRepository : RepositoryBase<Report>, IReportRepository
    {
        public ReportRepository(OplevOgDelDbContext context) : base (context)
        {

        }
    }
}
