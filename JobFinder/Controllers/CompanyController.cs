using Microsoft.AspNetCore.Mvc;
using JobFinder.Domain;
using JobFinder.Infrastructure;

namespace JobFinder.Controllers
{
    [ApiController]
    [Route("api/v1/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _repository;

        public CompanyController(ICompanyRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _repository.GetAllAsync();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var company = await _repository.GetByIdAsync(id);
            if (company == null) return NotFound();
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Company company)
        {
            company.Id = Guid.NewGuid();
            company.CreatedDate = DateTime.UtcNow;
            await _repository.AddAsync(company);
            return CreatedAtAction(nameof(GetById), new { id = company.Id }, company);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Company updatedCompany)
        {
            var company = await _repository.GetByIdAsync(id);
            if (company == null) return NotFound();

            company.Name = updatedCompany.Name;
            company.Description = updatedCompany.Description;

            await _repository.UpdateAsync(company);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
