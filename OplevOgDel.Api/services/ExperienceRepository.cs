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
    public class ExperienceRepository : RepositoryBase<Experience>, IExperienceRepository
    {
        public ExperienceRepository(OplevOgDelDbContext context) : base (context)
        {

        }

        public async override Task<IEnumerable<Experience>> GetAllAsync()
        {
            return await this._context.Experiences.Include(x => x.Category).ToListAsync();
        }

        public async Task<Experience> GetAnExperience(Guid id)
        {
            return await this._context.Experiences.Where(x => x.Id == id).Include(x => x.Category).FirstOrDefaultAsync();
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            return await this._context.Categories.Where(x => x.Name == name).FirstOrDefaultAsync();
        }
    }
}
