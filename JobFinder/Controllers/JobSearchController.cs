using Microsoft.AspNetCore.Mvc;
using JobFinder.Domain;
using JobFinder.Infrastructure;
using JobFinder.Domain.Dtos;

namespace JobFinder.Controllers
{
    [ApiController]
    [Route("api/v1/jobsearches")]
    public class JobSearchController : ControllerBase
    {
        private readonly IJobSearchRepository _repository;
        private readonly IUserRepository _userRepository;

        public JobSearchController(
            IJobSearchRepository repository,
            IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobSearchRequest dto)
        {
            // User var mı kontrol et
            var user = await _userRepository.GetByIdAsync(dto.UserId);
            if (user == null)
                return BadRequest("User not found.");

            var search = new JobSearch
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                User = user,
                SearchText = dto.SearchText,
                SearchDate = DateTime.UtcNow
            };

            await _repository.AddAsync(search);

            var resultDto = new JobSearchDto
            {
                Id = search.Id,
                UserId = search.UserId,
                UserName = user.UserName,
                SearchText = search.SearchText,
                SearchDate = search.SearchDate
            };

            return CreatedAtAction(nameof(GetById), new { id = search.Id }, resultDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var searches = await _repository.GetAllAsync();

            var dtos = searches.Select(s => new JobSearchDto
            {
                Id = s.Id,
                UserId = s.UserId,
                UserName = s.User?.UserName ?? "Unknown",
                SearchText = s.SearchText,
                SearchDate = s.SearchDate
            });

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var search = await _repository.GetByIdAsync(id);
            if (search == null) return NotFound();

            var dto = new JobSearchDto
            {
                Id = search.Id,
                UserId = search.UserId,
                UserName = search.User?.UserName ?? "Unknown",
                SearchText = search.SearchText,
                SearchDate = search.SearchDate
            };

            return Ok(dto);
        }

        [HttpGet("cached")]
        public async Task<IActionResult> GetCachedJobSearches([FromServices] RedisCacheService cacheService)
        {
            var cached = await cacheService.GetAsync<List<JobSearchDto>>("jobsearches");

            if (cached != null)
            {
                return Ok(cached);
            }

            var jobSearches = await _repository.GetAllAsync();

            var dtos = jobSearches.Select(s => new JobSearchDto
            {
                Id = s.Id,
                UserId = s.UserId,
                UserName = s.User?.UserName ?? "Unknown",
                SearchText = s.SearchText,
                SearchDate = s.SearchDate
            }).ToList();

            await cacheService.SetAsync("jobsearches", dtos, TimeSpan.FromMinutes(10));

            return Ok(dtos);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateJobSearchRequest dto)
        {
            var search = await _repository.GetByIdAsync(id);
            if (search == null) return NotFound();

            var user = await _userRepository.GetByIdAsync(dto.UserId);
            if (user == null)
                return BadRequest("User not found.");

            search.SearchText = dto.SearchText;
            search.UserId = dto.UserId;
            search.User = user;
            search.SearchDate = DateTime.UtcNow;

            await _repository.UpdateAsync(search);
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
