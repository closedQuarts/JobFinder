using System;

namespace JobFinder.Domain
{
    public class JobApplication
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid JobPostingId { get; set; }
        public string? Message { get; set; }
        public DateTime ApplicationDate { get; set; }

        // Navigation properties
        public User? User { get; set; }
        public JobPosting? JobPosting { get; set; }
    }
}
