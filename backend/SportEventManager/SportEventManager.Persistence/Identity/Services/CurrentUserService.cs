using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SportEventManager.Application.Common;
using SportEventManager.Domain.Entities;
using System.Security.Claims;

namespace SportEventManager.Persistence.Identity.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Lazy<UserManager<User>> _userManager;

        public CurrentUserService(
            IHttpContextAccessor httpContextAccessor,
            Lazy<UserManager<User>> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        public string UserName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name) ?? string.Empty;

        public string Email => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;

        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        public IList<string> Roles
        {
            get
            {
                if (!IsAuthenticated || string.IsNullOrEmpty(UserId))
                {
                    return new List<string>();
                }

                var user = _userManager.Value.FindByIdAsync(UserId).Result;
                return user != null ? _userManager.Value.GetRolesAsync(user).Result : new List<string>();
            }
        }
    }
}
