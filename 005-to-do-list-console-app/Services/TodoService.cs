using System.Text.Json;
using _005_to_do_list_console_app.Interfaces;
using _005_to_do_list_console_app.Models;

namespace _005_to_do_list_console_app.Services
{
    public class TodoService : ITodoService
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _jsonOptions;
        private List<TodoItem> _todoItems;

        public TodoService(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            _todoItems = new List<TodoItem>();

            // Load existing todo items from the file asynchronously
            _ = LoadDataAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            await LoadDataAsync(); // Ensure data is loaded before returning
            return _todoItems.AsReadOnly();
        }

        public async Task<TodoItem?> GetByIdAsync(Guid id)
        {
            await LoadDataAsync();
            return _todoItems.FirstOrDefault(t => t.Id == id);
        }

        public async Task<TodoItem> CreateAsync(string title, string description = "")
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be empty.", nameof(title));
            }

            await LoadDataAsync();

            var newItem = TodoItem.Create(title, description);
            _todoItems.Add(newItem);

            await SaveDataAsync();
            return newItem;
        }

        public async Task UpdateAsync(Guid id, string? title = null, string? description = null)
        {
            await LoadDataAsync();

            var item = _todoItems.FirstOrDefault(t => t.Id == id);

            if (item == null)
            {
                throw new KeyNotFoundException($"Todo item with ID {id} not found.");
            }

            if (title != null)
            {
                item.UpdateTitle(title);
            }

            if (description != null)
            {
                item.UpdateDescription(description);
            }

            await SaveDataAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await LoadDataAsync();

            var item = _todoItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                throw new KeyNotFoundException($"Todo item with ID {id} not found.");
            }

            _todoItems.Remove(item);
            await SaveDataAsync();
        }

        public async Task MarkAsCompletedAsync(Guid id)
        {
            await LoadDataAsync();

            var item = _todoItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                throw new KeyNotFoundException($"Todo item with ID {id} not found.");
            }

            item.MarkAsCompleted();
            await SaveDataAsync();
        }

        public async Task MarkAsIncompleteAsync(Guid id)
        {
            await LoadDataAsync();

            var item = _todoItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                throw new KeyNotFoundException($"Todo item with ID {id} not found.");
            }

            item.MarkAsIncomplete();
            await SaveDataAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetCompletedAsync()
        {
            await LoadDataAsync();
            return _todoItems
                .Where(t => t.IsCompleted)
                .ToList();
        }

        public async Task<IEnumerable<TodoItem>> GetPendingAsync()
        {
            await LoadDataAsync();
            return _todoItems
                .Where(t => !t.IsCompleted)
                .ToList();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    _todoItems = new List<TodoItem>();
                    return;
                }

                var json = await File.ReadAllTextAsync(_filePath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    _todoItems = new List<TodoItem>();
                    return;
                }

                _todoItems = JsonSerializer
                            .Deserialize<List<TodoItem>>(json, _jsonOptions)
                            ?? new List<TodoItem>();
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Failed to load todo items from {_filePath}. Invalid JSON format.", ex);
            }
            catch (IOException ex)
            {
                throw new InvalidOperationException($"Failed to load todo items from {_filePath}. File access error.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An unexpected error occurred while loading todo items from {_filePath}.", ex);
            }
        }

        private async Task SaveDataAsync()
        {
            try
            {
                var json = JsonSerializer.Serialize(_todoItems, _jsonOptions);
                await File.WriteAllTextAsync(_filePath, json);
            }
            catch (IOException ex)
            {
                throw new InvalidOperationException($"Failed to save todo items to {_filePath}. File access error.", ex);
            }
        }
    }
}