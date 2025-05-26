using System;

namespace BugTracker.Models
{
    public enum PriorityLevel
    {
        Low,
        Medium,
        High
    }

    public class Bug
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public required string CreatedBy { get; set; }
        public bool IsResolved { get; set; }
        public DateTime? ResolvedDate { get; set; }
        
        // Priority Level feature implementation
        public PriorityLevel Priority { get; set; }
        
        // Default constructor for testing purposes
        public Bug()
        {
            Title = string.Empty;
            Description = string.Empty;
            CreatedBy = string.Empty;
        }
    }
}
