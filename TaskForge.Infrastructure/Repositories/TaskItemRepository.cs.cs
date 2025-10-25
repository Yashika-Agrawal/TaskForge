using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskForge.Application.Interfaces.Repositories;
using TaskForge.Domain.Entities;

namespace TaskForge.Infrastructure.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        //need something to talk to the database,
        //That’s what DbContext (Entity Framework Core's way of handling database interaction) does.
        private readonly List<TaskItem> taskItems = [];
        
        public  Task<TaskItem> CreateAsync (TaskItem item)
        {
            item.Id = Guid.NewGuid();
            taskItems.Add(item);
            return Task.FromResult(item); 

            //return item; //if we have async in definition it automatically wraps object into Task<T> type

        }
        public  Task<TaskItem?> GetByIdAsync(Guid id)
        {
            var item=taskItems.Find(x => x.Id == id);
            return Task.FromResult(item); 
        }
    }
}
