namespace SportEventManager.Application.Common.Models.Constants
{
    public static class PaginationConstants
    {
        public const int DefaultPageNumber = 1;
        public const int DefaultPageSize = 10;
        public const int MaxPageSize = 100;

        public static class SortDirection
        {
            public const string Ascending = "asc";
            public const string Descending = "desc";
        }
    }
}
