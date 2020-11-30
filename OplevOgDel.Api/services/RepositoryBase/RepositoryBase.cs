using Microsoft.EntityFrameworkCore;
using OplevOgDel.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Services.RepositoryBase
{
    /// <summary>
    /// A generic repository implementation
    /// that the repositories will inhert from,
    /// so we don't have to recreate the same code everytime
    /// </summary>
    /// <typeparam name="T">The table to make the implementation of</typeparam>
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        /// <summary>
        /// database context to make calls to
        /// </summary>
        protected readonly OplevOgDelDbContext _context;

        public RepositoryBase(OplevOgDelDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Asynchronously gets every single item in a table
        /// </summary>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Creates an item in the DB
        /// </summary>
        /// <param name="entity"></param>
        public void Create(T entity)
        {
            // Adds an item to be tracked by EF core
            _context.Set<T>().Add(entity);
        }
        /// <summary>
        /// Deletes an item in the DB
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            // Changes an item to a deleted state
            _context.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Asynchronously gets one item based on an expression
        /// </summary>
        /// <param name="expr">The expression condition to get the item</param>
        /// <returns></returns>
        public async Task<T> GetFirstByExpressionAsync(Expression<Func<T, bool>> expr)
        {
            return await _context.Set<T>().Where(expr).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Saves every change´in the database
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Updates an item in the DB
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            // Changes an item to an updated state
            _context.Set<T>().Update(entity);
        }

        /// <summary>
        /// Deletes multiple items from the DB
        /// </summary>
        /// <param name="entities"></param>
        public void DeleteMany(IEnumerable<T> entities)
        {
            // Changes multiple items to a deleted state
            _context.Set<T>().RemoveRange(entities);
        }
    }
}
