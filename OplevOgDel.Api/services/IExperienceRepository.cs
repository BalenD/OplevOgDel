using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Models.Dto.RequestDto;
using OplevOgDel.Api.Services.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Services
{
    /// <summary>
    /// Interface for teh repository handling "experience" table calls
    /// </summary>
    public interface IExperienceRepository : IRepositoryBase<Experience>
    {
        /// <summary>
        /// Get a category by the name
        /// </summary>
        /// <param name="name">Name of the category to get</param>
        Task<Category> GetCategoryByNameAsync(string name);
        /// <summary>
        /// Get one experience by id
        /// </summary>
        /// <param name="id">Id to get the experience by</param>
        Task<Experience> GetAnExperienceAsync(Guid id);
        /// <summary>
        /// Gets every experience that has a report or that has a review with a report
        /// </summary>
        Task<IEnumerable<Experience>> GetExperiencesWithReportsAsync();
        /// <summary>
        /// Gets all experiences in the databased in a paged order
        /// </summary>
        /// <param name="req">Filtering and searching parameters</param>
        /// <returns></returns>
        Task<IEnumerable<Experience>> GetAllAsync(ExperienceRequestParametersDto req);
        /// <summary>
        /// Get an experience and its reports
        /// </summary>
        /// <param name="id">Id of the experience with reports to get</param>
        Task<Experience> GetAnExperienceAndReportsAsync(Guid id);
    }
}
