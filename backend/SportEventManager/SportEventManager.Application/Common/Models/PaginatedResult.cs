namespace SportEventManager.Application.Common.Models
{
    public class PaginatedResult<T>
    {
        public IReadOnlyList<T> Items { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }

        public PaginatedResult(IReadOnlyList<T> items, int pageNumber, int pageSize, int totalCount)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }

        public static PaginatedResult<T> Create(IReadOnlyList<T> items, int pageNumber, int pageSize, int totalCount)
            => new(items, pageNumber, pageSize, totalCount);
    }
}
