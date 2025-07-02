using System;

namespace JobFinder.Domain
{
    public class JobSearch
    {
        public Guid Id { get; set; }
        public User User { get; set; } = null!;
        public Guid UserId { get; set; }
        public string SearchText { get; set; } = null!;
        public DateTime SearchDate { get; set; }
    }
}
