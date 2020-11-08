using OplevOgDel.Api.Data;
using OplevOgDel.Api.Data.Models;

namespace OplevOgDel.Api.services.RepositoryBase
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(OplevOgDelDbContext context) : base(context)
        {

        }
    }
}
