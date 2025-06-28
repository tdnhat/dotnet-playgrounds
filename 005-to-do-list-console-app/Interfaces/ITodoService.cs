using _005_to_do_list_console_app.Models;

namespace _005_to_do_list_console_app.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem?> GetByIdAsync(Guid id);
        Task<TodoItem> CreateAsync(string title, string description = "");
        Task UpdateAsync(Guid id, string? title = null, string? description = null);
        Task DeleteAsync(Guid id);
        Task MarkAsCompletedAsync(Guid id);
        Task MarkAsIncompleteAsync(Guid id);
        Task<IEnumerable<TodoItem>> GetCompletedAsync();
        Task<IEnumerable<TodoItem>> GetPendingAsync();
    }
}