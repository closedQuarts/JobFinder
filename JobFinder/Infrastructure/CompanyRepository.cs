using JobFinder.Domain;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Infrastructure
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly JobFinderDbContext _context;

        public CompanyRepository(JobFinderDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company?> GetByIdAsync(Guid id)
        {
            return await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);
            if (company != null)
            {
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
            }
        }
    }
}
