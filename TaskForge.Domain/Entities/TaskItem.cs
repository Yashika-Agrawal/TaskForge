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

        //Task is the child which needs to know who created it and owns it
        //foreign keys goes in child entity : child is taskitem and parent is User
        public Guid AssignedToUserId { get; set; }  // FK, used in db mapping, used to access full user object
        public Guid CreatedByUserId { get; set; }   // FK

        public DateTime DueDate { get; set; } //deadline to complete task
        public Status Status { get; set; } = Status.Todo; //enum to have todo, progress, completed
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; //current time when task is created at
        public DateTime? LastModifiedAt { get; set; } // Nullable because task may not be updated yet
        public string? AttachmentUrl { get; set; } //optional to attach attachment

    }
}
