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
        public async Task<bool> AssignRoleToUserAsync(Guid userId, Guid roleId)
        {
            if (userId == Guid.Empty || roleId == Guid.Empty)
            {
                return false;
            }
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user==null)
            {
                return false;
            }
            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
            if(role==null)
            {
                return false;
            }
            // Check if already assigned
            bool exists = await _dbContext.UserRoles
                .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

            if (exists)
                return false;

            var userRole = new UserRole
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                RoleId = roleId,
                AssignedAt = DateTime.UtcNow
            };

            await _dbContext.UserRoles.AddAsync(userRole);
           
            await _dbContext.SaveChangesAsync();
            return true;


        }
    }
}
