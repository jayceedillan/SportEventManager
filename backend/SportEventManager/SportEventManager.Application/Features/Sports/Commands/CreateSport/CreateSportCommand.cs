﻿using MediatR;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Application.Features.Sports.Commands.CreateSport
{
    public class CreateSportCommand : IRequest<Sport>
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
