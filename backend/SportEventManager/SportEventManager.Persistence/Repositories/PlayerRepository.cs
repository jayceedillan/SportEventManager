using Microsoft.EntityFrameworkCore;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Persistence.Repositories
{
    public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(ApplicationDbContext context) : base(context)
        {
        } 
        public async Task<Player?> GetPlayerById(int id)
        {
            return await _context.Players
                    .Where(p => p.Id == id)
                    .Include(p => p.Sport)
                    .Include(p => p.Event)
                    .Include(p => p.Team)
                    .FirstOrDefaultAsync();
        }
    }

}
