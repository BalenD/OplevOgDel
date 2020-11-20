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
        Task<Category> GetCategoryByName(string name);
        Task<Experience> GetAnExperience(Guid id);
        Task<IEnumerable<Experience>> GetExperiencesWithReports();
        Task<IEnumerable<Experience>> GetAllAsync(RequestParametersDto req);
    }
}
