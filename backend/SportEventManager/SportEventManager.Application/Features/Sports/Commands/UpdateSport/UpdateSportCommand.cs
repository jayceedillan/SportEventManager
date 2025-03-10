﻿using MediatR;
using SportEventManager.Application.DTOs;

namespace SportEventManager.Application.Features.Sports.Commands.UpdateSport
{
    public class UpdateSportCommand : IRequest<SportDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Rules { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int? CategoryId { get; set; }
        public bool IsActive { get; set; }
    }
}
