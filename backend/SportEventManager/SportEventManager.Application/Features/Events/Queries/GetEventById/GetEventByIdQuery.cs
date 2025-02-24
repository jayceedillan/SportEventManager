using MediatR;
using SportEventManager.Application.DTOs;

namespace SportEventManager.Application.Features.Events.Queries.GetEventById
{
    public class GetEventByIdQuery : IRequest<EventDto>
    {
        public int Id { get; set; }

        public GetEventByIdQuery(int id)
        {
            Id = id;
        }
    }
}
