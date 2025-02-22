using SportEventManager.Domain.Interfaces;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private ISportCategoryRepository _sportCategoryRepository;
        //private ISportRepository _sportRepository;
        // ... other repositories

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ISportCategoryRepository SportCategories =>
            _sportCategoryRepository ??= new SportCategoryRepository(_context);

        //public ISportRepository Sports =>
        //    _sportRepository ??= new SportRepository(_context);

        // ... other repository properties

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
