using OplevOgDel.Api.Data;
using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.services.RepositoryBase;

namespace OplevOgDel.Api.services
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(OplevOgDelDbContext context) : base(context)
        {

        }
    }
}
