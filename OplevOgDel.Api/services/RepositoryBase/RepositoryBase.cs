using Microsoft.EntityFrameworkCore;
using OplevOgDel.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Services.RepositoryBase
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly OplevOgDelDbContext _context;


        public RepositoryBase(OplevOgDelDbContext context)
        {
            _context = context;
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<T> GetFirstByExpressionAsync(Expression<Func<T, bool>> expr)
        {
            return await _context.Set<T>().Where(expr).FirstOrDefaultAsync();
        }

        public async Task<bool> Saveasync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
