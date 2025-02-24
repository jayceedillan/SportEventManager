using AutoMapper;
using SportEventManager.Application.Common.Models;

namespace SportEventManager.Application.Mappings
{
    public class PaginatedResultConverter<TSource, TDestination> : ITypeConverter<PaginatedResult<TSource>, PaginatedResult<TDestination>>
    {
        private readonly IMapper _mapper;

        public PaginatedResultConverter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public PaginatedResult<TDestination> Convert(PaginatedResult<TSource> source, PaginatedResult<TDestination> destination, ResolutionContext context)
        {
            var mappedItems = _mapper.Map<List<TDestination>>(source.Items);
            return new PaginatedResult<TDestination>(mappedItems, source.TotalCount, source.PageNumber, source.PageSize);
        }
    }
}
