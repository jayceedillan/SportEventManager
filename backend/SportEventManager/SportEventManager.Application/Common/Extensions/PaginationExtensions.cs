using Microsoft.EntityFrameworkCore;
using SportEventManager.Application.Common.Models;

namespace SportEventManager.Application.Common.Extensions
{
    public static class PaginationExtensions
    {
        public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(
            this IQueryable<T> source,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var count = await source.CountAsync(cancellationToken);
            var items = await source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedResult<T>(items, pageNumber, pageSize, count);
        }

        public static PaginatedResult<T> ToPaginatedList<T>(
            this IEnumerable<T> source,
            int pageNumber,
            int pageSize)
        {
            var count = source.Count();
            var items = source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PaginatedResult<T>(items, pageNumber, pageSize, count);
        }
    }
}
