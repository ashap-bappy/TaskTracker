using TaskTracker.Enums;

namespace TaskTracker.Models
{
    public class TaskModel
    {
        public required string Id { get; set; }
        public string? Description { get; set; }
        public Status TaskStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
