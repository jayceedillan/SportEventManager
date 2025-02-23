using MediatR;
using SportEventManager.Application.Exceptions;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Sports.Queries.GetSportById
{
    public class GetEventByIdQueryHandler : IRequestHandler<GetSportByIdQuery, Sport>
    {
        private readonly IGenericRepository<Sport> _repository;

        public GetEventByIdQueryHandler(IGenericRepository<Sport> repository)
        {
            _repository = repository;
        }

        public async Task<Sport> Handle(GetSportByIdQuery request, CancellationToken cancellationToken)
        {
            var sport = await _repository.GetByIdAsync(request.Id);

            if (sport == null)
                throw new NotFoundException(nameof(Sport), request.Id);

            return sport;
        }
    }
}
