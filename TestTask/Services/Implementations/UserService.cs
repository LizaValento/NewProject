using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<User> GetUser()
        {
            return await _applicationDbContext.Users
            .Include(u => u.Orders)
            .OrderByDescending(u => u.Orders.Count)
            .FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetUsers()
        {
            return await _applicationDbContext.Set<User>()
            .Where(x => x.Status == UserStatus.Inactive)
            .ToListAsync();
        }
    }
}
