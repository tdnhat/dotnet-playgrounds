using System.Collections.Immutable;
using _004_advanced_linq_operations_exercise.Models;

namespace _004_advanced_linq_operations_exercise.Collections
{
    public static class ImmutableCollectionExamples
    {
        public static void Demonstrate()
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("IMMUTABLE COLLECTIONS");
            Console.WriteLine(new string('=', 50));

            ImmutableListDemo();
        }

        private static void ImmutableListDemo()
        {
            Console.WriteLine("\nImmutableList");
            Console.WriteLine("-------------");

            var originalList = ImmutableList<Product>.Empty;
            Console.WriteLine($"   Original list: {originalList.Count} items");

            var list1 = originalList.Add(new Product 
            { 
                Id = 1, Name = "Laptop", Price = 999.99m, CategoryId = 1, IsActive = true 
            });
            
            var list2 = list1.Add(new Product 
            { 
                Id = 2, Name = "Mouse", Price = 25.50m, CategoryId = 1, IsActive = true 
            });

            Console.WriteLine($"   After adds: original={originalList.Count}, final={list2.Count}");

            var builder = ImmutableList.CreateBuilder<Product>();
            
            for (int i = 3; i <= 10; i++)
            {
                builder.Add(new Product 
                { 
                    Id = i, 
                    Name = $"Product_{i}", 
                    Price = (decimal)(new Random(i).NextDouble() * 100 + 10),
                    CategoryId = i % 3,
                    IsActive = true 
                });
            }
            
            var bigList = builder.ToImmutable();
            Console.WriteLine($"   Built list: {bigList.Count} products");

            var expensiveProducts = bigList.Where(p => p.Price > 50).ToImmutableList();
            Console.WriteLine($"   Filtered: {expensiveProducts.Count} expensive products");
        }
    }
} 