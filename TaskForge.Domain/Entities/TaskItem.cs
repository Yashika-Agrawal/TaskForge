// using directives
using System; // console, datetime, 
using System.Collections.Generic; //list, dictionary
using System.Linq; //query list: where, first
using TaskForge.Domain.Enums;

namespace TaskForge.Domain.Entities //where this file exists in our project
{
    public class TaskItem // internal: only accessed in this project ie domain
    {
        public Guid Id { get; set; } //it is added for encapsulation to not have direct access to field and to have logic later
                                     //used by ef core framework
        public string Title { get; set; } = string.Empty; //for avoiding nulls, it takes "" empty string
        public string? Description { get; set; }
        public DateTime DueDate { get; set; } //deadline to complete task
        public Status Status { get; set; } = Status.Todo; //enum to have todo, progress, completed
        public DateTime CreatedAt { get; set; }

    }
}
