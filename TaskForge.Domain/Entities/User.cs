using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskForge.Domain.Enums;

namespace TaskForge.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public Role UserRole { get; set; } = Role.User;
        public string Email { get; set; } = string.Empty ;

        //Relationships : one to many ( one user multiple tasks)
        public List<TaskItem> CreatedTasks { get; set; } = []; // list of items user created (.net 8+)

        //public List<TaskItem> AssignedTasks { get; } = new(); 
        //public List<TaskItem> AssignedTasks { get; } = new List<TaskItem>();
        // Gives a fresh object in memory, without null it gives a error if we try to loop or add
        public List<TaskItem> AssignedTasks { get; } = []; // list of items user assigned to 
       


    }
}
