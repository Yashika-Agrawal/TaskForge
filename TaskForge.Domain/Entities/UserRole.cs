using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskForge.Domain.Entities
{
    // Join entity for User <-> Role many-to-many relationship
    public class UserRole
    {
        //UserId is a foreign key → points to a specific row in the Users table.
        public Guid UserId { get; set; }
        //User is the navigation property →  load the actual User object.
        public User User { get; set; }

        //RoleId is another foreign key → points to a row in the Roles table.
        public Guid RoleId { get; set; }
        //Role is the navigation property back to that role.
        public Role Role { get; set; }

        // Optional metadata for future (industry-ready)
        public DateTime AssignedAt { get; set; }
    }
}

