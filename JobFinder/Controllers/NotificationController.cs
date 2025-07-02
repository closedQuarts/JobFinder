using JobFinder.Domain;
using JobFinder.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers
{
    [ApiController]
    [Route("api/v1/notifications")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepo;

        public NotificationController(INotificationRepository notificationRepo)
        {
            _notificationRepo = notificationRepo;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserNotifications(Guid userId)
        {
            var notifications = await _notificationRepo.GetByUserIdAsync(userId);
            return Ok(notifications);
        }
    }
}
