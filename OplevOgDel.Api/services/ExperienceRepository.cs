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
    public class ExperienceRepository : RepositoryBase<Experience>, IExperienceRepository
    {
        public ExperienceRepository(OplevOgDelDbContext context) : base (context)
        {

        }

        public async override Task<IEnumerable<Experience>> GetAllAsync()
        {
            return await this._context.Experiences.Include(x => x.Category).Include(x => x.Pictures).ToListAsync();
        }

        public async Task<IEnumerable<Experience>> GetAllAsync(RequestParametersDto req)
        {

            var query = this._context.Experiences.Include(x => x.Category).Include(x => x.Pictures).AsQueryable().AsNoTracking();
            if (!string.IsNullOrEmpty(req.SortByCity))
            {
                query = query.Where(x => x.City.ToLower() == req.SortByCity.ToLower());
            }
            if (!string.IsNullOrEmpty(req.SortByCategory))
            {
                query = query.Where(x => x.Category.Name.ToLower() == req.SortByCategory.ToLower());
            }
            if (!string.IsNullOrEmpty(req.SearchString))
            {
                query = query.Where(x => x.Name.ToLower().Contains(req.SearchString.ToLower()));
            }
            return await PaginatedList<Experience>.CreateAsync(query, req.Page, req.PageSize);
        }

        public async Task<Experience> GetAnExperience(Guid id)
        {
            return await this._context.Experiences.Where(x => x.Id == id).Include(x => x.Category).Include(x => x.Pictures).FirstOrDefaultAsync();
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            return await this._context.Categories.Where(x => x.Name == name).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Experience>> GetExperiencesWithReports()
        {
            return await _context.Experiences.Where(e => e.ExperienceReports.Any() || e.Reviews.Any(r => r.ReviewReports.Any())).ToListAsync();
        }

        public async Task<Experience> GetAnExperienceAndReports(Guid id)
        {
            return await _context.Experiences.Where(e => e.Id == id)
                                                .Include(e => e.Creator)
                                                .Include(e => e.Category)
                                                .Include(e => e.ExperienceReports)
                                                    .ThenInclude(er => er.Creator)
                                                .Include(e => e.Reviews)
                                                    .ThenInclude(r => r.Creator)
                                                .Include(e => e.Reviews)
                                                    .ThenInclude(r => r.ReviewReports)
                                                        .ThenInclude(rr => rr.Creator)
                                                .FirstOrDefaultAsync();
        }
    }
}
