using Microsoft.EntityFrameworkCore;
using OplevOgDel.Api.Data;
using OplevOgDel.Api.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Services.RepositoryBase
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(OplevOgDelDbContext context) : base(context)
        {

        }

        public async override Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.Where(x => x.Role == "User").ToListAsync();
        }
    }
}
