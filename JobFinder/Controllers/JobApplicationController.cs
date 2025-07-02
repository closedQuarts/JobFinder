using JobFinder.Domain;
using JobFinder.Domain.Dtos;
using JobFinder.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers
{
    [ApiController]
    [Route("api/v1/jobapplications")]
    public class JobApplicationController : ControllerBase
    {
        private readonly IJobApplicationRepository _repository;

        public JobApplicationController(IJobApplicationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var apps = await _repository.GetAllAsync();

            var dtos = apps.Select(a => new JobApplicationDto
            {
                Id = a.Id,
                UserId = a.UserId,
                JobPostingId = a.JobPostingId,
                Message = a.Message,
                ApplicationDate = a.ApplicationDate
            });

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var app = await _repository.GetByIdAsync(id);
            if (app == null) return NotFound();

            var dto = new JobApplicationDto
            {
                Id = app.Id,
                UserId = app.UserId,
                JobPostingId = app.JobPostingId,
                Message = app.Message,
                ApplicationDate = app.ApplicationDate
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobApplicationRequest request)
        {
            var app = new JobApplication
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                JobPostingId = request.JobPostingId,
                Message = request.Message,
                ApplicationDate = DateTime.UtcNow
            };

            await _repository.AddAsync(app);

            var dto = new JobApplicationDto
            {
                Id = app.Id,
                UserId = app.UserId,
                JobPostingId = app.JobPostingId,
                Message = app.Message,
                ApplicationDate = app.ApplicationDate
            };

            return CreatedAtAction(nameof(GetById), new { id = app.Id }, dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
