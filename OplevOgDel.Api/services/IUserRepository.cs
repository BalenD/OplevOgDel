using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Services.RepositoryBase;
using System;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Services
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> GetUserByUsername(string username);
    }
}
