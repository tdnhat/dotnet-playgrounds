using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace _004_advanced_linq_operations_exercise.Queries
{
    public static class PerformanceDemonstrations
    {
        public static void Demonstrate()
        {
            Console.WriteLine("Performance Demonstrations");
            Console.WriteLine("===========================");

            DeferredVsImmediateExecution();
        }

        private static void DeferredVsImmediateExecution()
        {
            Console.WriteLine("Deferred vs Immediate Execution:");

            // Example of deferred execution
            var largeDataset = Enumerable.Range(1, 1000000);

            // Deferred execution
            Console.WriteLine("\nDeferred Execution:");
            var sw = Stopwatch.StartNew();
            var deferredQuery = largeDataset.Where(x => x % 2 == 0).Select(x => x * 2);
            sw.Stop();
            Console.WriteLine($"Query created (not executed): {sw.ElapsedMilliseconds} ms");

            Console.WriteLine("Executing deferred query...");
            sw.Restart();
            var count = deferredQuery.Count();
            sw.Stop();
            Console.WriteLine($"Count of even numbers (deferred execution): {count}, Time taken: {sw.ElapsedMilliseconds} ms");

            // Immediate execution
            Console.WriteLine("\nImmediate Execution:");
            sw.Restart();
            var immediateQuery = largeDataset.Where(x => x % 2 == 0).Select(x => x * 2).ToList();
            sw.Stop();
            Console.WriteLine($"Query created and executed immediately: {sw.ElapsedMilliseconds} ms");
            sw.Restart();
            count = immediateQuery.Count;
            sw.Stop();
            Console.WriteLine($"Count of even numbers (immediate execution): {count}, Time taken: {sw.ElapsedMilliseconds} ms");
        }
    }
}