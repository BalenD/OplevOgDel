using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Services.RepositoryBase
{
    /// <summary>
    /// Interface for the generic repository
    /// </summary>
    /// <typeparam name="T">The table to make the repository base of</typeparam>
    public interface IRepositoryBase<T> where T : class
    {
        /// <summary>
        /// Asynchronously gets every single item in a table
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();
        /// <summary>
        /// Asynchronously gets one item based on an expression
        /// </summary>
        /// <param name="expr">The expression condition to get the item</param>
        /// <returns></returns>
        Task<T> GetFirstByExpressionAsync(Expression<Func<T, bool>> expr);
        /// <summary>
        /// Creates an item in the DB
        /// </summary>
        /// <param name="entity"></param>
        void Create(T entity);
        /// <summary>
        /// Updates an item in the DB
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
        /// <summary>
        /// Deletes an item in the DB
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
        /// <summary>
        /// Deletes multiple items from the DB
        /// </summary>
        /// <param name="entities"></param>
        void DeleteMany(IEnumerable<T> entities);
        /// <summary>
        /// Saves every change´in the database
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveAsync();
    }
}
