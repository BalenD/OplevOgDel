using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Services.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Services
{
    public interface IPictureRepository : IRepositoryBase<Picture>
    {
        Task<IEnumerable<Picture>> GetAllByExperienceAsync(Guid id);
    }
}
