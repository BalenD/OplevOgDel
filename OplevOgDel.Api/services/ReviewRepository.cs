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
    /// Implementation for the repository handling "reviews" table calls
    /// </summary>
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(OplevOgDelDbContext context) : base(context)
        {
            
        }

        /// <summary>
        /// Gets all reviews of an experience in the database, in a paged order,
        /// by the experience id
        /// </summary>
        /// <param name="req">Filtering  and searching parameters</param>
        /// <param name="experienceId">Id of the experience to get reviews for</param>
        public async Task<IEnumerable<Review>> GetAllReviewsAsync(ReviewRequestParametersDto req, Guid experienceId)
        {
            var query = this._context.Reviews.AsQueryable().AsNoTracking();
            if (!(req.FilterByDate == DateTime.MinValue))
            {
                query = query.Where(x => x.CreatedOn.Date.Equals(req.FilterByDate));
            }
            if (!string.IsNullOrEmpty(req.FilterByOwner))
            {
                query = query.Where(x => x.Creator.FirstName.ToLower() == req.FilterByOwner.ToLower())
                             .Where(x => x.Creator.LastName.ToLower() == req.FilterByOwner.ToLower());
            }
            if (!string.IsNullOrEmpty(req.SearchString))
            {
                query = query.Where(x => x.Description.ToLower().Contains(req.SearchString.ToLower()));
            }
            return await PaginatedList<Review>.CreateAsync(query, req.Page, req.PageSize);
        }

        /// <summary>
        /// Get one review by id
        /// </summary>
        /// <param name="id">Id of the review to get</param>
        public async Task<Review> GetAReviewAsync(Guid id)
        {
            return await _context.Reviews.Where(x => x.Id == id).Include(x => x.Creator).FirstOrDefaultAsync();
        }
    }
}
