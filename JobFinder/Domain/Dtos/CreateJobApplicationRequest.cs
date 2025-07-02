using System;

namespace JobFinder.Domain.Dtos
{
    public class CreateJobApplicationRequest
    {
        public Guid UserId { get; set; }
        public Guid JobPostingId { get; set; }
        public string? Message { get; set; }
    }
}
