using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace _005_to_do_list_console_app.Models
{
    public class TodoItem
    {
        public Guid Id { get; private set; }
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Title { get; private set; } = string.Empty;
        [StringLength(1000)]
        public string Description { get; private set; } = string.Empty;
        public bool IsCompleted { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Private constructor to enforce factory method usage
        // This prevents direct instantiation and ensures all properties are set correctly.
        private TodoItem() { }

        // Constructor for deserialization
        // This constructor is used by JSON deserialization libraries like System.Text.Json.
        [JsonConstructor]
        public TodoItem(Guid id, string title, string description, bool isCompleted, DateTime createdAt, DateTime? completedAt = null, DateTime? updatedAt = null)
        {
            Id = id;
            Title = title;
            Description = description;
            IsCompleted = isCompleted;
            CreatedAt = createdAt;
            CompletedAt = completedAt;
            UpdatedAt = updatedAt;
        }

        // Factory methods
        public static TodoItem Create(string title, string description = "")
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be empty.");
            }

            if (title.Length > 200)
            {
                throw new ArgumentException("Title cannot exceed 200 characters.");
            }

            if (description.Length > 1000)
            {
                throw new ArgumentException("Description cannot exceed 1000 characters.");
            }

            return new TodoItem
            {
                Id = Guid.NewGuid(),
                Title = title.Trim(),
                Description = description.Trim() ?? string.Empty,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void MarkAsCompleted()
        {
            if (IsCompleted)
            {
                throw new InvalidOperationException("Todo item is already completed.");
            }

            IsCompleted = true;
            CompletedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsIncomplete()
        {
            if (!IsCompleted)
            {
                throw new InvalidOperationException("Todo item is not completed.");
            }

            IsCompleted = false;
            CompletedAt = null;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be empty.");
            }

            if (title.Length > 200)
            {
                throw new ArgumentException("Title cannot exceed 200 characters.");
            }

            Title = title.Trim();
            UpdatedAt = DateTime.UtcNow;
        }
        public void UpdateDescription(string description)
        {
            if (description.Length > 1000)
            {
                throw new ArgumentException("Description cannot exceed 1000 characters.");
            }

            Description = description.Trim() ?? string.Empty;
            UpdatedAt = DateTime.UtcNow;
        }

        public override bool Equals(object? obj)
        {
            return obj is TodoItem item && Id.Equals(item.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            var status = IsCompleted ? "✓" : "○";
            return $"{status} {Title} (Created: {CreatedAt:yyyy-MM-dd})";
        }
    }
}