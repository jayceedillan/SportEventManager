using SportEventManager.Domain.Entities;

namespace SportEventManager.Domain.Interfaces.IRepositories
{
    public interface ISportRepository : IGenericRepository<Sport>
    {
        Task<IEnumerable<Sport>> GetSport();
        Task<Sport?> GetSport(int id);
    }
}
