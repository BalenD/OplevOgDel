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
    /// Implementation for the repository handling "picture" table calls
    /// </summary>
    public class PictureRepository : RepositoryBase<Picture>, IPictureRepository
    {
        public PictureRepository(OplevOgDelDbContext context) : base (context)
        {

        }
        /// <summary>
        /// Get all pictures by experience id
        /// </summary>
        /// <param name="id">Id of the experience</param>
        public async Task<IEnumerable<Picture>> GetAllByExperienceAsync(Guid id)
        {
            return await this._context.Pictures.Where(x => x.ExperienceId == id).ToListAsync();
        }

    }
}
