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
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(OplevOgDelDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync(Guid experienceId)
        {
            return await this._context.Reviews.Include(x => x.Creator).Where(x => x.ExperienceId == experienceId).AsNoTracking().ToListAsync();
        }

        public async Task<Review> GetAReviewAsync(Guid id)
        {
            return await this._context.Reviews.Where(x => x.Id == id).Include(x => x.Creator).FirstOrDefaultAsync();
        }
    }
}
