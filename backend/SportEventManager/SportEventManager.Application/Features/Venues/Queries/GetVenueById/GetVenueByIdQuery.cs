using MediatR;
using SportEventManager.Application.DTOs;

namespace SportEventManager.Application.Features.Venues.Queries.GetVenueByIdQuery
{
    public class GetVenueByIdQuery : IRequest<VenueDto>
    {
        public int Id { get; set; }

        public GetVenueByIdQuery(int id)
        {
            Id = id;
        }
    }
}
