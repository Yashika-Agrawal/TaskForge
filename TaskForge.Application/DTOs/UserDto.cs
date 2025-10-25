using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskForge.Domain.Enums;

namespace TaskForge.Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; } 
        public string  UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Role UserRole { get; set; }

    }
}
