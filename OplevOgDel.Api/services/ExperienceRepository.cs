using OplevOgDel.Api.Data;
using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.services.RepositoryBase;

namespace OplevOgDel.Api.services
{
    public class ExperienceRepository : RepositoryBase<Experience>, IExperienceRepository
    {
        public ExperienceRepository(OplevOgDelDbContext context) : base (context)
        {

        }
    }
}
