namespace ToDo.Application.DTOs
{
    public class TodoItemDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public bool IsCompleted { get; set; }
    }
}
