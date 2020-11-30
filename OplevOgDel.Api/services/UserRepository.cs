using Microsoft.EntityFrameworkCore;
using OplevOgDel.Api.Data;
using OplevOgDel.Api.Data.Models;
using System;
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
        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.Where(x => x.Username == username).Include(x => x.Profile).FirstOrDefaultAsync();
        }
        public async override Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.Where(x => x.Role == "User").ToListAsync();
        }
    }
}
