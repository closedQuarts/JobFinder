using JobFinder.Domain;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Infrastructure
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly JobFinderDbContext _context;

        public JobApplicationRepository(JobFinderDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobApplication>> GetAllAsync()
        {
            return await _context.JobApplications
                .Include(j => j.User)
                .Include(j => j.JobPosting)
                .ToListAsync();
        }

        public async Task<JobApplication?> GetByIdAsync(Guid id)
        {
            return await _context.JobApplications
                .Include(j => j.User)
                .Include(j => j.JobPosting)
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task AddAsync(JobApplication jobApplication)
        {
            _context.JobApplications.Add(jobApplication);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(JobApplication jobApplication)
        {
            _context.JobApplications.Update(jobApplication);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.JobApplications.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                _context.JobApplications.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
