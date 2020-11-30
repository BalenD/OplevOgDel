using Microsoft.EntityFrameworkCore;
using OplevOgDel.Api.Data;
using OplevOgDel.Api.Data.Models;
using System;
using OplevOgDel.Api.Helpers;
using OplevOgDel.Api.Models.Dto.RequestDto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Services.RepositoryBase
{
    /// <summary>
    /// Implementation for the repository handling "users" table calls
    /// </summary>
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(OplevOgDelDbContext context) : base(context)
        {

        }

        /// <summary>
        /// Gets all users in the database in a paged order
        /// </summary>
        /// <param name="req">Filtering and searching parameters</param>
        public async Task<IEnumerable<User>> GetAllAsync(UserRequestParametersDto req)
        {
            var query = this._context.Users.AsQueryable().AsNoTracking();
            if (!string.IsNullOrEmpty(req.FilterByRole))
            {
                query = query.Where(x => x.Role.ToLower() == req.FilterByRole.ToLower());
            }
            if (!string.IsNullOrEmpty(req.SearchByEmail))
            {
                query = query.Where(x => x.Email.ToLower().Contains(req.SearchByEmail.ToLower()));
            }
            if (!string.IsNullOrEmpty(req.SearchByUsername))
            {
                query = query.Where(x => x.Username.ToLower().Contains(req.SearchByUsername.ToLower()));
            }
            return await PaginatedList<User>.CreateAsync(query, req.Page, req.PageSize);
        }
        
        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.Where(x => x.Username == username).Include(x => x.Profile).FirstOrDefaultAsync();
        }
    }
}
