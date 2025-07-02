using System;

namespace JobFinder.Domain.Dtos
{
    public class JobSearchDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string SearchText { get; set; } = null!;
        public DateTime SearchDate { get; set; }
    }

}
