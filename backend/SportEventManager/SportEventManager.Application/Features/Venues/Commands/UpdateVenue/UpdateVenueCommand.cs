using MediatR;
using SportEventManager.Application.DTOs;

namespace SportEventManager.Application.Features.Venues.Commands.UpdateEvent
{
    public class UpdateVenueCommand : IRequest<VenueDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Capacity { get; set; }
    }
}
