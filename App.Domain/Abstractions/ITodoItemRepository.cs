using ToDo.Domain.Entities;

namespace ToDo.Domain.Abstractions
{
    public interface ITodoItemRepository
    {
        Task<TodoItem?> GetTodoItemByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TodoItem?>> GetTodoItemsByUserIdAsync(int userId, CancellationToken cancellationToken = default);

        Task AddTodoItemAsync(TodoItem todoItem, CancellationToken cancellationToken = default);
        Task DeleteTodoItemAsync(TodoItem todoItem, CancellationToken cancellationToken = default);
        Task UpdateTodoItemAsync(TodoItem todoItem, CancellationToken cancellationToken = default);
    }
}
