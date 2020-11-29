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
    /// Implementation for the repository handling "experiences" table calls
    /// </summary>
    public class ExperienceRepository : RepositoryBase<Experience>, IExperienceRepository
    {
        public ExperienceRepository(OplevOgDelDbContext context) : base (context)
        {

        }
        /// <summary>
        /// Gets all experiences in the database in a paged order
        /// </summary>
        /// <param name="req">Filtering and searching parameters</param>
        public async Task<IEnumerable<Experience>> GetAllAsync(ExperienceRequestParametersDto req)
        {
            // retrieve all the experiences including category and picture as a queryable list
            var query = this._context.Experiences.Include(x => x.Category).Include(x => x.Pictures).AsQueryable().AsNoTracking();
            // if filtering by city was requested
            if (!string.IsNullOrEmpty(req.FilterByCity))
            {
                // add filtering by city to the query
                query = query.Where(x => x.City.ToLower() == req.FilterByCity.ToLower());
            }
            // if filtering by category was requested
            if (!string.IsNullOrEmpty(req.FilterByCategory))
            {
                // add filtering by category
                query = query.Where(x => x.Category.Name.ToLower() == req.FilterByCategory.ToLower());
            }
            // if there is a search by name
            if (!string.IsNullOrEmpty(req.SearchString))
            {
                // add the search by name to the query
                query = query.Where(x => x.Name.ToLower().Contains(req.SearchString.ToLower()));
            }
            // return a paged list
            return await PaginatedList<Experience>.CreateAsync(query, req.Page, req.PageSize);
        }

        /// <summary>
        /// Get one experience by id
        /// </summary>
        /// <param name="id">Id to get the experience by</param>
        public async Task<Experience> GetAnExperienceAsync(Guid id)
        {
            // Get one experience by id, including category and pictures
            return await this._context.Experiences.Where(x => x.Id == id).Include(x => x.Category).Include(x => x.Pictures).AsNoTracking().FirstOrDefaultAsync();
        }
         
        /// <summary>
        ///  Get a category by the name
        /// </summary>
        /// <param name="name">Name of the category to get</param>
        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            // finds the first category with the matching name
            return await this._context.Categories.Where(x => x.Name == name).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets every experience that has a report or that has a review with a report
        /// </summary>
        public async Task<IEnumerable<Experience>> GetExperiencesWithReportsAsync()
        {
            // return a list of all the experiences that have a report
            // or which has a review with a report
            return await _context.Experiences.Where(e => e.ExperienceReports.Any() || e.Reviews.Any(r => r.ReviewReports.Any())).AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Get an experience and its reports
        /// </summary>
        /// <param name="id">Id of the experience with reports to get</param>
        public async Task<Experience> GetAnExperienceAndReportsAsync(Guid id)
        {
            // find an experience with the matching id
            // and include all its relations
            return await _context.Experiences.Where(e => e.Id == id)
                                                .Include(e => e.Creator)
                                                .Include(e => e.Category)
                                                .Include(e => e.ExperienceReports)
                                                    .ThenInclude(er => er.Creator)
                                                .Include(e => e.Reviews.Where(r => r.ReviewReports.Any()))
                                                    .ThenInclude(r => r.Creator)
                                                .Include(e => e.Reviews.Where(r => r.ReviewReports.Any()))
                                                    .ThenInclude(r => r.ReviewReports)
                                                        .ThenInclude(rr => rr.Creator)
                                                .AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
