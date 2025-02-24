using MediatR;
using SportEventManager.Application.DTOs;

namespace SportEventManager.Application.Features.Sports.Queries.GetSportById
{
    public class GetSportByIdQuery : IRequest<SportDto>
    {
        public int Id { get; set; }

        public GetSportByIdQuery(int id)
        {
            Id = id;
        }
    }
}
