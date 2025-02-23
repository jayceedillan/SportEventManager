using MediatR;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Application.Features.Venues.Queries.GetVenueByIdQuery
{
    public class GetVenueByIdQuery : IRequest<Venue>
    {
        public int Id { get; set; }

        public GetVenueByIdQuery(int id)
        {
            Id = id;
        }
    }
}
