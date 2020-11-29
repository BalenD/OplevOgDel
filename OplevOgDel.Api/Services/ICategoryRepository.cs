using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Services.RepositoryBase;

namespace OplevOgDel.Api.Services
{
    /// <summary>
    /// Interface for the repository handling "categories" table calls
    /// </summary>
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
    }
}
