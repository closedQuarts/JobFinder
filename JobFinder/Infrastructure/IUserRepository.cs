using JobFinder.Domain;


namespace JobFinder.Infrastructure
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();

        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
    }
}
