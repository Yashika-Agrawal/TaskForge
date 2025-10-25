using Microsoft.EntityFrameworkCore;
using TaskForge.Application.Interfaces.Repositories;
using TaskForge.Domain.Entities;
using TaskForge.Infrastructure.Persistence;

namespace TaskForge.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskForgeDbContext _dbContext;

        public UserRepository(TaskForgeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<string>?> GetRolesForUserAsync(Guid userId)
        {
            if(userId == Guid.Empty)
            { 
                return null;
            }
            var roleNames = await _dbContext.UserRoles
                .Where(u => u.UserId == userId)
                .Include(ur => ur.Role)
                .Select(r => r.Role.Name)
                .ToListAsync();

            return roleNames;
        }
    }
}
