using Microsoft.EntityFrameworkCore;
using SportEventManager.Domain.Common;
using SportEventManager.Domain.Interfaces.IRepositories;
using System.Linq.Expressions;

namespace SportEventManager.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>()
               .Where(x => !x.IsDeleted && x.Id == id)
               .FirstOrDefaultAsync();
        }

        public IQueryable<T> GetAllQueryable()
        {
            return _context.Set<T>().Where(d => !d.IsDeleted).AsQueryable();
        }

        public async Task<IReadOnlyList<T>> GetFilteredAsync(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            int pageNumber,
            int pageSize)
        {
            var query = _context.Set<T>()
                .Where(d => !d.IsDeleted)
                .Where(filter);

            query = orderBy(query); // Apply sorting

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            var existingEntity = await _context.Set<T>().FindAsync(entity.Id);

            if (existingEntity == null)
                throw new Exception($"Entity with ID {entity.Id} not found.");

            _context.Set<T>().Remove(existingEntity);
            await _context.SaveChangesAsync();
        }
    }
}
