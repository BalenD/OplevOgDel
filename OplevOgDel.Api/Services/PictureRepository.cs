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
    public class PictureRepository : RepositoryBase<Picture>, IPictureRepository
    {
        public PictureRepository(OplevOgDelDbContext context) : base (context)
        {

        }

        public async Task<IEnumerable<Picture>> GetAllByExperienceAsync(Guid id)
        {
            return await this._context.Pictures.Where(x => x.ExperienceId == id).ToListAsync();
        }

    }
}
