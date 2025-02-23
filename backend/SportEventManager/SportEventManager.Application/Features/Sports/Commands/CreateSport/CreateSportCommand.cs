using MediatR;
using SportEventManager.Application.Features.SportCategories.DTOs;

namespace SportEventManager.Application.Features.Sports.Commands.CreateSport
{
    public class CreateSportCommand : IRequest<SportDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Rules { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int? CategoryId { get; set; }
        public bool IsActive { get; set; }
    }
}
