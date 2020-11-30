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
    /// Implementation for the repository handling "experiencereports" table calls
    /// </summary>
    public class ExperienceReportRepository : RepositoryBase<ExperienceReport>, IExperienceReportRepository
    {
        public ExperienceReportRepository(OplevOgDelDbContext context) : base(context)
        {

        }
        /// <summary>
        /// Gets every report belonging to an experience
        /// </summary>
        /// <param name="experienceId">Id of the experience to get reports of</param>
        /// <returns></returns>
        public async Task<IEnumerable<ExperienceReport>> GetReportsForExperience(Guid experienceId)
        {
            return await _context.ExperienceReports.Where(x => x.ExperienceId == experienceId).ToListAsync();
        }
    }
}
