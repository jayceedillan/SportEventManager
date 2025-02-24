using AutoMapper;
using SportEventManager.Application.Common.Models;

namespace SportEventManager.Application.Mappings
{
    public class ListToPaginatedResultConverter<T> : ITypeConverter<List<T>, PaginatedResult<T>>
    {
        public PaginatedResult<T> Convert(List<T> source, PaginatedResult<T> destination, ResolutionContext context)
        {
            // 🔹 Ensure required parameters are passed
            int totalCount = source.Count;
            int pageSize = totalCount > 0 ? totalCount : 10; // Default pageSize if unknown
            int pageIndex = 1; // Default page index

            return new PaginatedResult<T>(source, totalCount, pageIndex, pageSize);
        }
    }
}
