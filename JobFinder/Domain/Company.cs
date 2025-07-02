using System;

namespace JobFinder.Domain
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}
