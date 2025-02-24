using SportEventManager.Domain.Common;
using System.Linq.Expressions;

namespace SportEventManager.Domain.Interfaces.IRepositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        IQueryable<T> GetAllQueryable(); 
        Task<IReadOnlyList<T>> GetFilteredAsync(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            int pageNumber,
            int pageSize
        );
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
