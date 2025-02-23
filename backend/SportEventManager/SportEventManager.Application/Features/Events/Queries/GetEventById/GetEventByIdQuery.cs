using MediatR;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Application.Features.Events.Queries.GetEventById
{
    public class GetEventByIdQuery : IRequest<Event>
    {
        public int Id { get; set; }

        public GetEventByIdQuery(int id)
        {
            Id = id;
        }
    }
}
