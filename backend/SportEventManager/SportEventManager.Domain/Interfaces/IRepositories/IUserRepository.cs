using SportEventManager.Domain.Entities;

namespace SportEventManager.Domain.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<User?> CreateUserAsync(User user, string password);
        Task<User?> GetUserByIdAsync(string id);
        Task<List<User>> GetUsersAsync(string search);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(string userId);
        Task<List<string>> GetUserRolesAsync(User user);
        Task<bool> AddToRolesAsync(User user, IEnumerable<string> roles);
        IQueryable<User> GetAllUsersQueryable();
        Task<Dictionary<string, List<string>>> GetRolesForUsersAsync(List<string> userIds);
    }
}
