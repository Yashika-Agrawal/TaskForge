using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskForge.Domain.Entities
{
    public class Role
    {
        public Guid Id { get; set; }

        // e.g. "User", "Admin"
        public string Name { get; set; } = string.Empty;

        // Navigation property back to UserRoles
        //ICollection<T> is an interface (like a “contract” that says: “this thing behaves like a collection”).
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}

