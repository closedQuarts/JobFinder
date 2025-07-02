namespace JobFinder.Domain
{
    public class JobPosting
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Town { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string WorkingPreference { get; set; } = null!;
        public DateTime LastUpdateDate { get; set; }
        public int ApplicationCount { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;
    }
}
