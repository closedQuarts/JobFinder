using JobFinder.Domain;

namespace JobFinder.Infrastructure
{
    public interface IJobApplicationRepository
    {
        Task<IEnumerable<JobApplication>> GetAllAsync();
        Task<JobApplication?> GetByIdAsync(Guid id);
        Task AddAsync(JobApplication jobApplication);
        Task UpdateAsync(JobApplication jobApplication);
        Task DeleteAsync(Guid id);
    }
}
