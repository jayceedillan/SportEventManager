using SportEventManager.Application.Common.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportEventManager.Application.Features.SportCategories.DTOs
{
    public record SportCategoryFilterDto
    {
        public string? SearchTerm { get; init; }
        public int PageNumber { get; init; } = PaginationConstants.DefaultPageNumber;
        public int PageSize { get; init; } = PaginationConstants.DefaultPageSize;
        public string? SortBy { get; init; }
        public bool SortDescending { get; init; }

        public SportCategoryFilterDto()
        {
            PageNumber = Math.Max(PageNumber, PaginationConstants.DefaultPageNumber);
            PageSize = Math.Min(Math.Max(PageSize, 1), PaginationConstants.MaxPageSize);
        }
    }
}
