using JobFinder.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobFinder.Infrastructure
{
    public interface INotificationRepository
    {
        Task AddAsync(Notification notification);
        Task<List<Notification>> GetByUserIdAsync(Guid userId);
    }
}
