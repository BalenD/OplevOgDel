using OplevOgDel.Api.Data;
using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.services.RepositoryBase;

namespace OplevOgDel.Api.services
{
    public class ProfileRepository : RepositoryBase<Profile>, IProfileRepository
    {
        public ProfileRepository(OplevOgDelDbContext context) : base(context)
        {

        }
    }
}
