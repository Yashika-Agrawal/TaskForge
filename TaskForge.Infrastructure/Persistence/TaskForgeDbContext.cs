using Microsoft.EntityFrameworkCore; // DbContext and DbSet
using TaskForge.Domain.Entities;

namespace TaskForge.Infrastructure.Persistence
{
    // In Entity Framework Core (EF Core), the DbContext is the bridge
    // between your C# objects (entities like TaskItem) and the database tables.
    // Mapping entities to database tables → e.g., your TaskItem entity will map to a TaskItems table.

    public class TaskForgeDbContext : DbContext
    {
        public TaskForgeDbContext(DbContextOptions<TaskForgeDbContext> options) : base(options)
        {

        }

        // DbSets for your tables
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        // Configure relationships and keys here
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many-to-many: User ↔ Role via UserRole
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId }); // composite primary key

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // Seeding Roles
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Admin"
                },
                new Role
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "User"
                }
            );
            // Seeding Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    UserName = "Alice",
                    Email = "alice@example.com"
                },
                new User
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    UserName = "Bob",
                    Email = "bob@example.com"
                }
            );
            // Seeding UserRoles
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole
                {
                    UserId = Guid.Parse("33333333-3333-3333-3333-333333333333"), // Alice
                    RoleId = Guid.Parse("11111111-1111-1111-1111-111111111111")  // Admin
                },
                new UserRole
                {
                    UserId = Guid.Parse("33333333-3333-3333-3333-333333333333"), // Alice
                    RoleId = Guid.Parse("22222222-2222-2222-2222-222222222222")  // User
                },
                new UserRole
                {
                    UserId = Guid.Parse("44444444-4444-4444-4444-444444444444"), // Bob
                    RoleId = Guid.Parse("22222222-2222-2222-2222-222222222222")  // User
                }
            );


        }

    }
}
