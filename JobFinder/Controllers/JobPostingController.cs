using Microsoft.AspNetCore.Mvc;
using JobFinder.Infrastructure;
using JobFinder.Domain;
using JobFinder.Domain.Dtos;

namespace JobFinder.Controllers
{
    [ApiController]
    [Route("api/v1/jobpostings")]
    public class JobPostingController : ControllerBase
    {
        private readonly IJobPostingRepository _repository;
        private readonly ServiceBusPublisher _publisher;

        public JobPostingController(
            IJobPostingRepository repository,
            ServiceBusPublisher publisher)
        {
            _repository = repository;
            _publisher = publisher;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search)
        {
            var jobs = await _repository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                jobs = jobs
                    .Where(j =>
                        (!string.IsNullOrEmpty(j.Title) && j.Title.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                        (!string.IsNullOrEmpty(j.Description) && j.Description.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                        (!string.IsNullOrEmpty(j.City) && j.City.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                        (!string.IsNullOrEmpty(j.Town) && j.Town.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                        (!string.IsNullOrEmpty(j.Country) && j.Country.Contains(search, StringComparison.OrdinalIgnoreCase))
                    )
                    .ToList();
            }

            var jobDtos = jobs.Select(job => new JobPostingDto
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                City = job.City,
                Town = job.Town,
                Country = job.Country,
                WorkingPreference = job.WorkingPreference,
                LastUpdateDate = job.LastUpdateDate,
                ApplicationCount = job.ApplicationCount,
                CompanyId = job.CompanyId,
                CompanyName = job.Company?.Name ?? "Unknown"
            });

            return Ok(jobDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var job = await _repository.GetByIdAsync(id);
            if (job == null) return NotFound();

            var jobDto = new JobPostingDto
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                City = job.City,
                Town = job.Town,
                Country = job.Country,
                WorkingPreference = job.WorkingPreference,
                LastUpdateDate = job.LastUpdateDate,
                ApplicationCount = job.ApplicationCount,
                CompanyId = job.CompanyId,
                CompanyName = job.Company?.Name ?? "Unknown"
            };

            return Ok(jobDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobPostingRequest dto)
        {
            var job = new JobPosting
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                City = dto.City,
                Town = dto.Town,
                Country = dto.Country,
                WorkingPreference = dto.WorkingPreference,
                LastUpdateDate = DateTime.UtcNow,
                ApplicationCount = 0,
                CompanyId = dto.CompanyId
            };

            await _repository.AddAsync(job);
            await _publisher.SendMessageAsync(job);

            var jobDto = new JobPostingDto
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                City = job.City,
                Town = job.Town,
                Country = job.Country,
                WorkingPreference = job.WorkingPreference,
                LastUpdateDate = job.LastUpdateDate,
                ApplicationCount = job.ApplicationCount,
                CompanyId = job.CompanyId,
                CompanyName = job.Company?.Name ?? "Unknown"
            };

            return CreatedAtAction(nameof(GetById), new { id = job.Id }, jobDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateJobPostingRequest dto)
        {
            var job = await _repository.GetByIdAsync(id);
            if (job == null) return NotFound();

            job.Title = dto.Title;
            job.Description = dto.Description;
            job.City = dto.City;
            job.Town = dto.Town;
            job.Country = dto.Country;
            job.WorkingPreference = dto.WorkingPreference;
            job.LastUpdateDate = DateTime.UtcNow;

            await _repository.UpdateAsync(job);
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
