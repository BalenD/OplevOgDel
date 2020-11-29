using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Models.Dto.RequestDto;
using OplevOgDel.Api.Services.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Services
{
    /// <summary>
    /// Interface for the repository handling "profile" table calls
    /// </summary>
    public interface IProfileRepository : IRepositoryBase<Profile>
    {
        /// <summary>
        /// Get a profile by id
        /// </summary>
        /// <param name="id">Id of the profile to get</param>
        Task<Profile> GetAProfileAsync(Guid id);
        /// <summary>
        /// Gets all profiles in the database, in a paged order
        /// </summary>
        /// <param name="req">Filtering and searching parameters</param>
        Task<IEnumerable<Profile>> GetAllAsync(ProfileRequestParametersDto req);
    }
}
