using ToDo.Domain.Abstractions;
using ToDo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ToDo.Infrastructure.Persistence.Data.DbContexts;

namespace ToDo.Infrastructure.Persistence.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly AppDbContext _dbContext;

        public TodoItemRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TodoItem?> GetTodoItemByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.TodoItems.FindAsync(new object[] { id }, cancellationToken);
        }
        public async Task<IEnumerable<TodoItem?>> GetTodoItemsByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.TodoItems
                .Where(t => t.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task AddTodoItemAsync(TodoItem todoItem, CancellationToken cancellationToken = default)
        {
            await _dbContext.TodoItems.AddAsync(todoItem, cancellationToken);
        }

        public Task DeleteTodoItemAsync(TodoItem todoItem, CancellationToken cancellationToken = default)
        {
            _dbContext.TodoItems.Remove(todoItem);
            return Task.CompletedTask;
        }
        public Task UpdateTodoItemAsync(TodoItem todoItem, CancellationToken cancellationToken = default)
        {
            _dbContext.TodoItems.Update(todoItem);
            return Task.CompletedTask;
        }
    }
}
