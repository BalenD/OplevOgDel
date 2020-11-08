using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OplevOgDel.Api.services.RepositoryBase
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetFirstByExpressionAsync(Expression<Func<T, bool>> expr);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> Saveasync();
    }
}
