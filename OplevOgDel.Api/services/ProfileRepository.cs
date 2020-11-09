using OplevOgDel.Api.Data;
using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Services.RepositoryBase;

namespace OplevOgDel.Api.Services
{
    public class ProfileRepository : RepositoryBase<Profile>, IProfileRepository
    {
        public ProfileRepository(OplevOgDelDbContext context) : base(context)
        {

        }
    }
}
