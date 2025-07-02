using System;

namespace JobFinder.Domain.Dtos
{
    public class JobApplicationDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid JobPostingId { get; set; }
        public string? Message { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}
