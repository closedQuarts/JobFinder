using JobFinder.Domain;

namespace JobFinder.Infrastructure
{
    public interface IJobSearchRepository
    {
        Task<IEnumerable<JobSearch>> GetAllAsync();
        Task<JobSearch?> GetByIdAsync(Guid id);
        Task AddAsync(JobSearch jobSearch);
        Task UpdateAsync(JobSearch jobSearch);
        Task DeleteAsync(Guid id);
    }
}
