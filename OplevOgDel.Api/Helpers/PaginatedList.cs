using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OplevOgDel.Api.Helpers
{
    /// <summary>
    /// Class that creates a paged list
    /// </summary>
    /// <typeparam name="T">Type of paged list</typeparam>
    public class PaginatedList<T> : List<T>
    {
        /// <summary>
        /// Which page you are on
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// Total amount of pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Whether the page has a previous page
        /// </summary>
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        /// <summary>
        /// Whether the page has a next page
        /// </summary>
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            // calculated the total pages
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        public static async Task<PaginatedList<T>> CreateAsync(
           IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip(
                (pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
