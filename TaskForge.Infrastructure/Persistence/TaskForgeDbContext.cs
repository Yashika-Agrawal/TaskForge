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
                .HasKey(ur => ur.Id);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<UserRole>()
                .HasIndex(ur => new { ur.UserId, ur.RoleId })
                .IsUnique();
        }

    }
}
