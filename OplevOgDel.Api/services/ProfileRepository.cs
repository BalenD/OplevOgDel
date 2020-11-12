using Microsoft.EntityFrameworkCore;
using OplevOgDel.Api.Data;
using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Services.RepositoryBase;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace OplevOgDel.Api.Services
{
    public class ProfileRepository : RepositoryBase<Profile>, IProfileRepository
    {
        public ProfileRepository(OplevOgDelDbContext context) : base(context)
        {

        }

        public async Task<Profile> GetAProfile(Guid id)
        {
            return await _context.Profiles.Where(p => p.Id == id)
                                            .Include(p => p.ListOfExps)
                                            .ThenInclude(l => l.ListOfExpsExperiences).FirstOrDefaultAsync();
        }
    }
}
