using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Services.RepositoryBase;
using System;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Services
{
    public interface IProfileRepository : IRepositoryBase<Profile>
    {
        Task<Profile> GetAProfileAsync(Guid id);
    }
}
