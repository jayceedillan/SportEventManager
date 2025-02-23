using SportEventManager.Application.Common.Models;
using SportEventManager.Application.Common.Queries;
using SportEventManager.Application.DTOs;


namespace SportEventManager.Application.Features.Venues.Queries.GetAllVenue
{
    public record GetAllVenueQuery(PaginationFilterDto filter)
       : PaginatedQuery<VenueDto>(filter);
}
