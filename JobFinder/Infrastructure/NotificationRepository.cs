using JobFinder.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Infrastructure
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly JobFinderDbContext _context;

        public NotificationRepository(JobFinderDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Notification>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }
    }
}
