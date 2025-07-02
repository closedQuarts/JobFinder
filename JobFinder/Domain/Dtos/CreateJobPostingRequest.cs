using System;

namespace JobFinder.Domain.Dtos
{
    public class CreateJobPostingRequest
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Town { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string WorkingPreference { get; set; } = null!;
        public Guid CompanyId { get; set; }
    }
}
