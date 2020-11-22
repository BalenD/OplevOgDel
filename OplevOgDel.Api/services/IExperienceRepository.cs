using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Models.Dto.RequestDto;
using OplevOgDel.Api.Services.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Services
{
    public interface IExperienceRepository : IRepositoryBase<Experience>
    {
        Task<Category> GetCategoryByNameAsync(string name);
        Task<Experience> GetAnExperienceAsync(Guid id);
        Task<IEnumerable<Experience>> GetExperiencesWithReportsAsync();
        Task<IEnumerable<Experience>> GetAllAsync(RequestParametersDto req);
        Task<Experience> GetAnExperienceAndReportsAsync(Guid id);
    }
}
