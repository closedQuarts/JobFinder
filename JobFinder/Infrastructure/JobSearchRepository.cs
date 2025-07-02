using JobFinder.Domain;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Infrastructure
{
    public class JobSearchRepository : IJobSearchRepository
    {
        private readonly JobFinderDbContext _context;

        public JobSearchRepository(JobFinderDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobSearch>> GetAllAsync()
        {
            return await _context.JobSearches
                .Include(j => j.User)
                .ToListAsync();
        }

        public async Task<JobSearch?> GetByIdAsync(Guid id)
        {
            return await _context.JobSearches
                .Include(j => j.User)
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task AddAsync(JobSearch jobSearch)
        {
            _context.JobSearches.Add(jobSearch);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(JobSearch jobSearch)
        {
            _context.JobSearches.Update(jobSearch);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var search = await _context.JobSearches
                .FirstOrDefaultAsync(j => j.Id == id);

            if (search != null)
            {
                _context.JobSearches.Remove(search);
                await _context.SaveChangesAsync();
            }
        }
    }
}
