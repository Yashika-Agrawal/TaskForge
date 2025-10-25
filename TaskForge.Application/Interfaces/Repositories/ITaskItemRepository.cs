using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskForge.Domain.Entities;

namespace TaskForge.Application.Interfaces.Repositories
{
    public interface ITaskItemRepository //abstraction
    {
        Task<TaskItem> CreateAsync(TaskItem taskItem);

        Task<TaskItem?> GetByIdAsync(Guid id);

    }
}
