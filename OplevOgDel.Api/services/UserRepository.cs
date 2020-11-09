using OplevOgDel.Api.Data;
using OplevOgDel.Api.Data.Models;

namespace OplevOgDel.Api.Services.RepositoryBase
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(OplevOgDelDbContext context) : base(context)
        {

        }
    }
}
