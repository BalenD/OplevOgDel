using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Services.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Services
{
    public interface IReviewRepository : IRepositoryBase<Review>
    {
        Task<IEnumerable<Review>> GetAllReviews(Guid experienceId);
        Task<Review> GetAReview(Guid id);
    }
}
