using Microsoft.AspNetCore.Mvc;
using JobFinder.Domain;
using JobFinder.Infrastructure;
using JobFinder.Domain.Dtos;

namespace JobFinder.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _repository.GetAllAsync();

            var userDtos = users.Select(u => new UserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                CreatedDate = u.CreatedDate
            });

            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return NotFound();

            var userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                CreatedDate = user.CreatedDate
            };

            return Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            user.Id = Guid.NewGuid();
            user.CreatedDate = DateTime.UtcNow;
            await _repository.AddAsync(user);

            var userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                CreatedDate = user.CreatedDate
            };

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, userDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] User updatedUser)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return NotFound();

            user.UserName = updatedUser.UserName;
            user.Email = updatedUser.Email;

            await _repository.UpdateAsync(user);
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
