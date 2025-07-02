using JobFinder.Domain;
using JobFinder.Infrastructure;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JobFinder.Services
{
    public class JobAlertService
    {
        private readonly INotificationRepository _notificationRepo;
        private readonly IJobSearchRepository _jobSearchRepo;

        public JobAlertService(
            INotificationRepository notificationRepo,
            IJobSearchRepository jobSearchRepo)
        {
            _notificationRepo = notificationRepo;
            _jobSearchRepo = jobSearchRepo;
        }

        public async Task RunJobAlertsAsync(JobPosting newJob)
        {
            var searches = await _jobSearchRepo.GetAllAsync();

            var matchingUsers = searches
                .Where(x => newJob.Title.Contains(x.SearchText, StringComparison.OrdinalIgnoreCase)
                        || newJob.City.Contains(x.SearchText, StringComparison.OrdinalIgnoreCase))
                .Select(x => x.UserId)
                .Distinct()
                .ToList();

            foreach (var userId in matchingUsers)
            {
                var notification = new Notification
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Message = $"Yeni ilan yayınlandı: {newJob.Title} - {newJob.City}",
                    CreatedAt = DateTime.UtcNow,
                    IsRead = false
                };

                await _notificationRepo.AddAsync(notification);
            }
        }
    }
}
