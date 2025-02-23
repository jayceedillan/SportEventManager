using MediatR;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Application.Features.Sports.Queries.GetSportById
{
    public class GetSportByIdQuery : IRequest<Sport>
    {
        public int Id { get; set; }

        public GetSportByIdQuery(int id)
        {
            Id = id;
        }
    }
}
