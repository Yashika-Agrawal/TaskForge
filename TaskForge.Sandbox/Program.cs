using TaskForge.Domain.Entities;

// Create roles
var userRole = new Role { Id = Guid.NewGuid(), Name = "User" };
var adminRole = new Role { Id = Guid.NewGuid(), Name = "Admin" };

// Create users
var alice = new User { Id = Guid.NewGuid(), UserName = "Alice" };
var bob = new User { Id = Guid.NewGuid(), UserName = "Bob" };

// Link Alice to User + Admin
alice.UserRoles.Add(new UserRole { User = alice, Role = userRole });
alice.UserRoles.Add(new UserRole { User = alice, Role = adminRole });

// Link Bob to User only
bob.UserRoles.Add(new UserRole { User = bob, Role = userRole });

// Print results
foreach (var ur in alice.UserRoles)
{
    Console.WriteLine($"{alice.UserName} has role {ur.Role.Name}");
}

foreach (var ur in bob.UserRoles)
{
    Console.WriteLine($"{bob.UserName} has role {ur.Role.Name}");
}
