using Microsoft.EntityFrameworkCore;
using TaskForge.Application.Interfaces.Repositories;
using TaskForge.Domain.Entities;
using TaskForge.Infrastructure.Persistence;

namespace TaskForge.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskForgeDbContext _dbContext;
        public async Task<List<string>?> GetRolesForUserAsync(User user)
        {
            if(user == null || user.Id == Guid.Empty)
            { 
                return null;
            }
            var roleNames = await _dbContext.UserRoles
                .Where(u => u.UserId == user.Id)
                .Include(ur => ur.Role)
                .Select(r => r.Role.Name)
                .ToListAsync();

            return roleNames;
        }
    }
}
