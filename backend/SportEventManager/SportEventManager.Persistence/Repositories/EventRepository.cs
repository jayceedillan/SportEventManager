using Microsoft.EntityFrameworkCore;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Persistence.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Event>> GetSport()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event?> GetSport(int id)
        {
            return await _context.Events.FirstOrDefaultAsync(x => x.Id == id);
        }
    }

}
