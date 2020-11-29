using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Models.Dto.RequestDto;
using OplevOgDel.Api.Services.RepositoryBase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Services
{
    /// <summary>
    /// Interface for the repository handling "user" table calls
    /// </summary>
    public interface IUserRepository : IRepositoryBase<User>
    {
        /// <summary>
        /// Gets all users in the database in a paged order
        /// </summary>
        /// <param name="req">Filtering and searching parameters</param>
        Task<IEnumerable<User>> GetAllAsync(UserRequestParametersDto req);
    }
}
