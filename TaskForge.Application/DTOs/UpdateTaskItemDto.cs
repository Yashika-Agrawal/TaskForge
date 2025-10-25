using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskForge.Domain.Enums;

namespace TaskForge.Application.DTOs
{
    public class UpdateTaskItemDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }      
        public DateTime DueDate { get; set; }
    }

}
