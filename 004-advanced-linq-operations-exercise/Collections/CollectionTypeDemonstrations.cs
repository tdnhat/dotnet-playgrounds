using System.Diagnostics;
using _004_advanced_linq_operations_exercise.Models;

namespace _004_advanced_linq_operations_exercise.Collections
{
    public static class CollectionTypeDemonstrations
    {
        public static void Demonstrate()
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("COLLECTION TYPES");
            Console.WriteLine(new string('=', 50));

            ListVsDictionaryDemo();
        }

        private static void ListVsDictionaryDemo()
        {
            Console.WriteLine("\nList vs Dictionary");
            Console.WriteLine("-----------------------------------");

            var customers = new List<Customer>();
            var customerDict = new Dictionary<int, Customer>();

            for (int i = 0; i < 1000000; i++)
            {
                var customer = new Customer 
                { 
                    Id = i, 
                    Name = $"Customer_{i}", 
                    Region = $"Region_{i % 3}" 
                };
                customers.Add(customer);
                customerDict[i] = customer;
            }

            var sw = new Stopwatch();
            var searchId = 900000;

            Console.WriteLine("\nList search:");
            sw.Start();
            var foundInList = customers.FirstOrDefault(c => c.Id == searchId);
            sw.Stop();
            Console.WriteLine($"   Found: {foundInList?.Name}");
            Console.WriteLine($"   Time: {sw.ElapsedMilliseconds}ms");

            Console.WriteLine("\nDictionary lookup:");
            sw.Restart();
            customerDict.TryGetValue(searchId, out var foundInDict);
            sw.Stop();
            Console.WriteLine($"   Found: {foundInDict?.Name}");
            Console.WriteLine($"   Time: {sw.ElapsedMilliseconds}ms");
        }
    }
} 