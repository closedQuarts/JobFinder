using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using JobFinder.Infrastructure;

namespace JobFinder.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<JobFinderDbContext>
    {
        public JobFinderDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<JobFinderDbContext>();

            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=JobFinderDb;Trusted_Connection=True;");

            return new JobFinderDbContext(optionsBuilder.Options);
        }
    }
}
