using System.Numerics;
using _004_advanced_linq_operations_exercise.Data;
using _004_advanced_linq_operations_exercise.Models;
using _004_advanced_linq_operations_exercise.Queries;

namespace _004_advanced_linq_operations_exercise
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advanced LINQ Operations Exercise");
            Console.WriteLine("================================");

            // Generate sample data
            var categories = SampleDataGenerator.GenerateCategories();
            var customers = SampleDataGenerator.GenerateCustomers();
            var products = SampleDataGenerator.GenerateProducts(categories);
            var orders = SampleDataGenerator.GenerateOrders(customers, products);

            Console.WriteLine("Sample data generated successfully.");
            Console.WriteLine($"Total Categories: {categories.Count}, First category: {categories.FirstOrDefault()?.Name ?? "None"}");
            Console.WriteLine($"Total Customers: {customers.Count}, First customer: {customers.FirstOrDefault()?.Name ?? "None"}");
            Console.WriteLine($"Total Products: {products.Count}, First product: {products.FirstOrDefault()?.Name ?? "None"}");
            Console.WriteLine($"Total Orders: {orders.Count}, First order date: {orders.FirstOrDefault()?.OrderDate.ToString("d") ?? "None"}");

            // Implement complex queries with multiple joins and grouping operations
            Console.WriteLine("\nRunning complex queries...");
            Console.WriteLine("====================================================");

            // ComplexQueries.RunInnerJoinQuery(customers.ToArray(), orders.ToArray(), products.ToArray());

            // ComplexQueries.RunGroupJoinQuery(customers.ToArray(), orders.ToArray());

            ComplexQueries.RunGroupByRegionQuery(customers.ToArray(), orders.ToArray());

            // Performance Analysis
            // PerformanceDemonstrations.Demonstrate();
        }
    }
}
