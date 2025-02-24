using SportEventManager.Domain.Entities;

namespace SportEventManager.Domain.Interfaces.IRepositories
{
    public interface IPlayerRepository : IGenericRepository<Player>
    {
        Task<Player?> GetPlayerById(int id);
    }
}
