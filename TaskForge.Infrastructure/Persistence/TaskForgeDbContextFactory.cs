using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TaskForge.Infrastructure.Persistence
{
    // This is ONLY used at design time (PMC/CLI).
    public class TaskForgeDbContextFactory : IDesignTimeDbContextFactory<TaskForgeDbContext>
    {
        public TaskForgeDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TaskForgeDbContext>();

            // 👇 Use your actual connection string here for now
            optionsBuilder.UseSqlServer(
                "Server=.;Database=TaskForgeDb;Trusted_Connection=True;TrustServerCertificate=True");

            return new TaskForgeDbContext(optionsBuilder.Options);
        }
    }
}
