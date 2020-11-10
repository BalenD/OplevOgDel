using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Services.RepositoryBase;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Services
{
    public interface IExperienceRepository : IRepositoryBase<Experience>
    {
        Task<ExpCategory> GetCategoryByName(string name);
    }
}
