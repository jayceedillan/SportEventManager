using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Persistence.Repositories
{
    public class UserRepository :  IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public UserRepository(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<User?> CreateUserAsync(User user, string password)
        { 
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded ? user : null;
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<List<User>> GetUsersAsync(string search)
        {
            
            return await _userManager.Users
                .Where(u => string.IsNullOrEmpty(search) || (u.UserName ?? "").Contains(search) || (u.Email ?? "").Contains(search))
                .ToListAsync();
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<List<string>> GetUserRolesAsync(User user)
        {
            return (await _userManager.GetRolesAsync(user)).ToList();
        }

        public async Task<bool> AddToRolesAsync(User user, IEnumerable<string> roles)
        {
            var result = await _userManager.AddToRolesAsync(user, roles);
            return result.Succeeded;
        }

        public IQueryable<User> GetAllUsersQueryable()
        {
            return _userManager.Users.AsNoTracking();
        }

        public async Task<Dictionary<string, List<string>>> GetRolesForUsersAsync(List<string> userIds)
        {
            var userRoles = await _context.UserRoles
                .Where(ur => userIds.Contains(ur.UserId))
                .Join(_context.Roles,
                    ur => ur.RoleId,
                    r => r.Id,
                    (ur, r) => new { ur.UserId, RoleName = r.Name })
                .GroupBy(ur => ur.UserId)
                .ToDictionaryAsync(g => g.Key, g => g.Select(x => x.RoleName).ToList());

            return userRoles;
        }
    }
}
