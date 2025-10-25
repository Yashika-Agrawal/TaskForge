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
        //  many-to-many join with roles
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public string Email { get; set; } = string.Empty ;
        public string PasswordHash { get; set; } = string.Empty;

    }
}
