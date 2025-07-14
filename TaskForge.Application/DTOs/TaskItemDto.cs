using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskForge.Application.DTOs
{
    public class TaskItemDto
    {
        public Guid Id { get; set; } //essential to reference a task for edit, delete etc
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
    }
}
