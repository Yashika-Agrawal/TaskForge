
using TaskForge.Domain.Entities;
namespace TaskForge.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<string>?> GetRolesForUserAsync(User user);

    }
}