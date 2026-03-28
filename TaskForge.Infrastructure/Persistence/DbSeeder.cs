using TaskForge.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace TaskForge.Infrastructure.Persistence
{
    public static class DbSeeder
    {
        public static void Seed(TaskForgeDbContext context)
        {
            // Run pending migrations if any
            context.Database.Migrate();

            // ✅ 1. Seed Roles
            if (!context.Roles.Any())
            {
                var adminRole = new Role { Id = Guid.NewGuid(), Name = "Admin" };
                var userRole = new Role { Id = Guid.NewGuid(), Name = "User" };
                context.Roles.AddRange(adminRole, userRole);
                context.SaveChanges();
            }

            // ✅ 2. Seed Users
            if (!context.Users.Any())
            {
                var adminRole = context.Roles.First(r => r.Name == "Admin");
                var userRole = context.Roles.First(r => r.Name == "User");

                var alice = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "Alice",
                    Email = "alice@taskforge.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123")
                };

                var bob = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "Bob",
                    Email = "bob@taskforge.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("User@123")
                };

                alice.UserRoles.Add(new UserRole { User = alice, Role = adminRole });
                alice.UserRoles.Add(new UserRole { User = alice, Role = userRole });
                bob.UserRoles.Add(new UserRole { User = bob, Role = userRole });

                context.Users.AddRange(alice, bob);
                context.SaveChanges();
            }
        }
    }
}
