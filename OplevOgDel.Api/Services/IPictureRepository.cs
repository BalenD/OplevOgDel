using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Services.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Services
{
    /// <summary>
    /// Interface for the repository handling "pictures" table calls
    /// </summary>
    public interface IPictureRepository : IRepositoryBase<Picture>
    {
        /// <summary>
        /// Get all pictures by experience id
        /// </summary>
        /// <param name="id">Id of the experience</param>
        Task<IEnumerable<Picture>> GetAllByExperienceAsync(Guid id);
    }
}
