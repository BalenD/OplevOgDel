using Microsoft.EntityFrameworkCore;
using OplevOgDel.Api.Data;
using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Helpers;
using OplevOgDel.Api.Models.Dto.RequestDto;
using OplevOgDel.Api.Services.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Services
{
    /// <summary>
    /// Implementation for the repository handling "profiles" table calls
    /// </summary>
    public class ProfileRepository : RepositoryBase<Profile>, IProfileRepository
    {
        public ProfileRepository(OplevOgDelDbContext context) : base(context)
        {

        }
        /// <summary>
        /// Gets all profiles in the database, in a paged order
        /// </summary>
        /// <param name="req">Filtering and searching parameters</param>
        public async Task<IEnumerable<Profile>> GetAllAsync(ProfileRequestParametersDto req)
        {
            var query = this._context.Profiles
                .Include(p => p.ListOfExps)
                .ThenInclude(i => i.ListOfExpsExperiences)
                .ThenInclude(le =>le.Experience)
                .ThenInclude(e => e.Category)
                .AsQueryable().AsNoTracking();

            if (!string.IsNullOrEmpty(req.SearchByFirstName))
            {
                query = query.Where(x => x.FirstName.ToLower().Contains(req.SearchByFirstName.ToLower()));
            }
            if (!string.IsNullOrEmpty(req.SearchByLastName))
            {
                query = query.Where(x => x.LastName.ToLower().Contains(req.SearchByLastName.ToLower()));
            }
            return await PaginatedList<Profile>.CreateAsync(query, req.Page, req.PageSize);
        }
        /// <summary>
        /// Get a profile by id
        /// </summary>
        /// <param name="id">Id of the profile to get</param>
        public async Task<Profile> GetAProfileAsync(Guid id)
        {
            return await _context.Profiles.Where(p => p.Id == id)
                                            .Include(p => p.ListOfExps)
                                                .ThenInclude(l => l.ListOfExpsExperiences)
                                                    .ThenInclude(le => le.Experience)
                                                        .ThenInclude(e => e.Category)
                                                        .AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
