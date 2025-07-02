using JobFinder.Domain;

namespace JobFinder.Infrastructure
{
    public interface IJobPostingRepository
    {
        Task<IEnumerable<JobPosting>> GetAllAsync();
        Task<JobPosting?> GetByIdAsync(Guid id);
        Task AddAsync(JobPosting jobPosting);
        Task UpdateAsync(JobPosting jobPosting);
        Task DeleteAsync(Guid id);
    }
}
