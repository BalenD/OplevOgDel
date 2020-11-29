using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Services.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Services
{
    /// <summary>
    /// Interface for the repository handling "experiencereport" table calls
    /// </summary>
    public interface IExperienceReportRepository : IRepositoryBase<ExperienceReport>
    {
        Task<IEnumerable<ExperienceReport>> GetReportsForExperience(Guid experienceId);
    }
}
