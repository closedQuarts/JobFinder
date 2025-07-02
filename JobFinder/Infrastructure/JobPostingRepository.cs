using JobFinder.Domain;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Infrastructure
{
    public class JobPostingRepository : IJobPostingRepository
    {
        private readonly JobFinderDbContext _context;

        public JobPostingRepository(JobFinderDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobPosting>> GetAllAsync()
        {
            return await _context.JobPostings
                .Include(j => j.Company)
                .ToListAsync();
        }

        public async Task<JobPosting?> GetByIdAsync(Guid id)
        {
            return await _context.JobPostings
                .Include(j => j.Company)
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task AddAsync(JobPosting jobPosting)
        {
            _context.JobPostings.Add(jobPosting);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(JobPosting jobPosting)
        {
            _context.JobPostings.Update(jobPosting);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var job = await _context.JobPostings.FirstOrDefaultAsync(j => j.Id == id);
            if (job != null)
            {
                _context.JobPostings.Remove(job);
                await _context.SaveChangesAsync();
            }
        }
    }
}
