using System;
using System.Collections.Generic;
using System.Linq;
using _004_advanced_linq_operations_exercise.Models;

namespace _004_advanced_linq_operations_exercise.Data
{
    public static class SampleDataGenerator
    {
        private static readonly Random _random = new Random();
        
        // Sample data arrays for realistic generation
        private static readonly string[] FirstNames = {
            "John", "Jane", "Michael", "Sarah", "David", "Emily", "Robert", "Lisa", "James", "Jessica",
            "William", "Ashley", "Richard", "Amanda", "Joseph", "Stephanie", "Thomas", "Melissa", "Christopher", "Nicole",
            "Daniel", "Elizabeth", "Paul", "Helen", "Mark", "Sandra", "Donald", "Donna", "George", "Carol",
            "Kenneth", "Ruth", "Steven", "Sharon", "Edward", "Michelle", "Brian", "Laura", "Ronald", "Sarah",
            "Anthony", "Kimberly", "Kevin", "Deborah", "Jason", "Dorothy", "Matthew", "Lisa", "Gary", "Nancy"
        };
        
        private static readonly string[] LastNames = {
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
            "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin",
            "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson",
            "Walker", "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores",
            "Green", "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts"
        };
        
        private static readonly string[] Regions = {
            "North America", "Europe", "Asia", "South America", "Africa", "Oceania", "Middle East", "Caribbean"
        };
        
        private static readonly string[] CategoryNames = {
            "Electronics", "Clothing", "Books", "Home & Garden", "Sports & Outdoors"
        };
        
        private static readonly Dictionary<string, string[]> ProductsByCategory = new Dictionary<string, string[]>
        {
            ["Electronics"] = new string[] {
                "Laptop", "Smartphone", "Tablet", "Headphones", "Smart Watch", "Desktop Computer", "Gaming Console",
                "Camera", "Bluetooth Speaker", "Monitor", "Keyboard", "Mouse", "Router", "Hard Drive", "USB Cable"
            },
            ["Clothing"] = new string[] {
                "T-Shirt", "Jeans", "Dress", "Jacket", "Sneakers", "Boots", "Sweater", "Shorts", "Skirt", "Blouse",
                "Pants", "Hoodie", "Coat", "Sandals", "Hat", "Scarf", "Gloves", "Socks", "Underwear", "Belt"
            },
            ["Books"] = new string[] {
                "Programming Guide", "Science Fiction Novel", "History Book", "Cookbook", "Biography", "Mystery Novel",
                "Self-Help Book", "Technical Manual", "Art Book", "Travel Guide", "Dictionary", "Encyclopedia"
            },
            ["Home & Garden"] = new string[] {
                "Coffee Maker", "Blender", "Vacuum Cleaner", "Lawn Mower", "Garden Hose", "Dining Table", "Chair",
                "Lamp", "Curtains", "Pillow", "Blanket", "Plant Pot", "Tools Set", "Mirror", "Carpet"
            },
            ["Sports & Outdoors"] = new string[] {
                "Running Shoes", "Bicycle", "Tennis Racket", "Basketball", "Football", "Camping Tent", "Backpack",
                "Water Bottle", "Yoga Mat", "Dumbbells", "Fishing Rod", "Golf Clubs", "Hiking Boots", "Swimsuit"
            }
        };

        public static List<Category> GenerateCategories()
        {
            var categories = new List<Category>();
            
            for (int i = 0; i < CategoryNames.Length; i++)
            {
                categories.Add(new Category
                {
                    Id = i + 1,
                    Name = CategoryNames[i]
                });
            }
            
            return categories;
        }

        public static List<Customer> GenerateCustomers(int count = 20)
        {
            var customers = new List<Customer>();
            
            for (int i = 1; i <= count; i++)
            {
                customers.Add(new Customer
                {
                    Id = i,
                    Name = $"{FirstNames[_random.Next(FirstNames.Length)]} {LastNames[_random.Next(LastNames.Length)]}",
                    Region = Regions[_random.Next(Regions.Length)]
                });
            }
            
            return customers;
        }

        public static List<Product> GenerateProducts(List<Category> categories, int count = 50)
        {
            var products = new List<Product>();
            
            for (int i = 1; i <= count; i++)
            {
                var category = categories[_random.Next(categories.Count)];
                var productNames = ProductsByCategory[category.Name];
                var productName = productNames[_random.Next(productNames.Length)];
                
                // Add some variation to product names to avoid duplicates
                if (_random.Next(3) == 0)
                {
                    productName += $" {_random.Next(1, 6)}";
                }
                
                products.Add(new Product
                {
                    Id = i,
                    Name = productName,
                    Price = Math.Round((decimal)(_random.NextDouble() * 1000 + 10), 2), // Price between $10 and $1010
                    CategoryId = category.Id,
                    Category = category
                });
            }
            
            return products;
        }

        public static List<Order> GenerateOrders(List<Customer> customers, List<Product> products, int count = 50)
        {
            var orders = new List<Order>();
            var startDate = DateTime.Now.AddYears(-2); // Orders from 2 years ago
            var endDate = DateTime.Now;
            var dateRange = (endDate - startDate).Days;
            
            for (int i = 1; i <= count; i++)
            {
                var customer = customers[_random.Next(customers.Count)];
                var product = products[_random.Next(products.Count)];
                var orderDate = startDate.AddDays(_random.Next(dateRange));
                
                orders.Add(new Order
                {
                    Id = i,
                    CustomerId = customer.Id,
                    ProductId = product.Id,
                    OrderDate = orderDate,
                    Amount = _random.Next(1, 10), // Order quantity between 1 and 9
                    Customer = customer,
                    Product = product
                });
            }
            
            return orders.OrderBy(o => o.OrderDate).ToList();
        }

        public static SampleData GenerateAllData()
        {
            var categories = GenerateCategories();
            var customers = GenerateCustomers(5);
            var products = GenerateProducts(categories, 10);
            var orders = GenerateOrders(customers, products, 50);
            
            return new SampleData
            {
                Categories = categories,
                Customers = customers,
                Products = products,
                Orders = orders
            };
        }
    }

    public class SampleData
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
