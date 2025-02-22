using Microsoft.EntityFrameworkCore;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Persistence.Repositories
{
    public class SportRepository : GenericRepository<Sport>, ISportRepository
    {
        public SportRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Sport>> GetSport()
        {
            return await _context.Sports.ToListAsync();
        }

        public async Task<Sport> GetSport(int id)
        {
            return await _context.Sports.FirstOrDefaultAsync(x => x.Id == id);
        }
    }

}
