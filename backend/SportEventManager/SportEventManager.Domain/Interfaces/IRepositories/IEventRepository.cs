using SportEventManager.Domain.Entities;

namespace SportEventManager.Domain.Interfaces.IRepositories
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        Task<IEnumerable<Event>> GetSport();
        Task<Event?> GetSport(int id);
    }
}
