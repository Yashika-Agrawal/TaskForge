
using TaskForge.Domain.Entities;
namespace TaskForge.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<string>?> GetRolesForUserAsync(Guid userId);
        Task<bool> AssignRoleToUserAsync(Guid userId, Guid roleId);

    }
}