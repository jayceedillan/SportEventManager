using SportEventManager.Domain.Entities;

namespace SportEventManager.Domain.Interfaces.IRepositories
{
    public interface ISportCategoryRepository : IGenericRepository<SportCategory>
    {
        Task<IEnumerable<SportCategory>> GetCategoriesWithChildren();
        Task<SportCategory?> GetCategoryWithParent(int id);
    }
}
