using Microsoft.EntityFrameworkCore;
using JobFinder.Domain;

namespace JobFinder.Infrastructure
{
    public class JobFinderDbContext : DbContext
    {
        public JobFinderDbContext(DbContextOptions<JobFinderDbContext> options)
            : base(options)
        {
        }
        public DbSet<JobSearch> JobSearches { get; set; }

        public DbSet<JobApplication> JobApplications { get; set; }

        public DbSet<Notification> Notifications { get; set; }


        public DbSet<User> Users { get; set; }

        public DbSet<JobPosting> JobPostings { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
