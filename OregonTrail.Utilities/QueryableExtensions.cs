using OregonTrail.UI.Shared.DTOs;
using System.Linq;
using System.Threading;

namespace OregonTrail.Utilities
{
    /// <summary>
    /// Extensions methods for queryable.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Paginates the queryable object based on the results.
        /// </summary>
        /// <typeparam name="T">A generic queryable data type.</typeparam>
        /// <param name="queryable">The queryable.</param>
        /// <param name="paginationDTO">The pagination data transfer object that defines pagination rules.</param>
        /// <returns></returns>
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationRequstDTO paginationDTO)
        {
            var recordsToSkip = (paginationDTO.Page - 1) * paginationDTO.RecordsPerPage;

            return queryable.Skip(recordsToSkip)
                    .Take(paginationDTO.RecordsPerPage);
        }
    }
}
