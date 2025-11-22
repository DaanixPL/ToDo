namespace ToDo.Domain.Entities
{
    public class TodoItem
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public required string Title { get; set; }
        public string? Description { get; set; }

        public bool IsCompleted { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? CompletedAt { get; set; }
    }
}
