using AutoMapper;
using MediatR;
using SportEventManager.Application.DTOs;
using SportEventManager.Application.Exceptions;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Players.Queries.GetPlayerByIdQuery
{
    public class GetPlayerByIdQueryHandler : IRequestHandler<GetPlayerByIdQuery, PlayerDto>
    {
        private readonly IPlayerRepository _repository;
        private readonly IMapper _mapper;

        public GetPlayerByIdQueryHandler(IPlayerRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PlayerDto> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
        {
           
            var player = await _repository.GetPlayerById(request.Id)
               ?? throw new NotFoundException(nameof(Player), request.Id);

            return _mapper.Map<PlayerDto>(player);
        }
    }
}
