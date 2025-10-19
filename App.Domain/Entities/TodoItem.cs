namespace ToDo.Domain.Entities
{
    public class TodoItem
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public required string Title { get; set; }
        public string? Description { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
    }
}
