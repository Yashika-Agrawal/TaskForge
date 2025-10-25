using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskForge.Application.DTOs
{
    public class CreateTaskItemDto
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }
    }
}
