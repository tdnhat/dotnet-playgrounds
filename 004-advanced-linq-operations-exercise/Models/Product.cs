using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _004_advanced_linq_operations_exercise.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }

        // Navigation property
        public Category Category { get; set; } = new Category();
    }
}
