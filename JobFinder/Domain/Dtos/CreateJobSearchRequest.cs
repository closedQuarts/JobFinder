using System;

namespace JobFinder.Domain.Dtos
{
    public class CreateJobSearchRequest
    {
        public Guid UserId { get; set; }
        public string SearchText { get; set; } = null!;
    }
}
