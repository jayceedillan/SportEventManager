using Microsoft.EntityFrameworkCore;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Persistence.Repositories
{
    public class SportCategoryRepository : GenericRepository<SportCategory>, ISportCategoryRepository
    {
        public SportCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SportCategory>> GetCategoriesWithChildren()
        {
            return await _context.SportCategories.ToListAsync();
        }

        public async Task<SportCategory> GetCategoryWithParent(int id)
        {
            return await _context.SportCategories.FirstOrDefaultAsync(x => x.Id == id);
        }
    }

}
