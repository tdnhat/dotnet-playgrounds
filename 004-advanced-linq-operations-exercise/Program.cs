using System.Numerics;
using _004_advanced_linq_operations_exercise.Data;
using _004_advanced_linq_operations_exercise.Models;
using _004_advanced_linq_operations_exercise.Queries;
using _004_advanced_linq_operations_exercise.Collections;

namespace _004_advanced_linq_operations_exercise
{
    class Program
    {
        static void Main(string[] args)
        {
            // Basic Collection Types 
            // CollectionTypeDemonstrations.Demonstrate();

            // Concurrent Collections
            // ConcurrentCollectionExamples.Demonstrate();

            // Immutable Collections
            // ImmutableCollectionExamples.Demonstrate();


            // Generate sample data
            // var categories = SampleDataGenerator.GenerateCategories();
            // var customers = SampleDataGenerator.GenerateCustomers();
            // var products = SampleDataGenerator.GenerateProducts(categories);
            // var orders = SampleDataGenerator.GenerateOrders(customers, products);

            // LINQ Operations
            // ComplexQueries.RunInnerJoinQuery(customers.ToArray(), orders.ToArray(), products.ToArray());
            // ComplexQueries.RunGroupJoinQuery(customers.ToArray(), orders.ToArray());
            // ComplexQueries.RunGroupByRegionQuery(customers.ToArray(), orders.ToArray());

            // ComplexQueries.RunGroupByRegionQuery(customers.ToArray(), orders.ToArray());
        }
    }
}
