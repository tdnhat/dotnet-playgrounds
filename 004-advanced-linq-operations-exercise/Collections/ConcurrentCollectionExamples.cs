using System.Collections.Concurrent;

namespace _004_advanced_linq_operations_exercise.Collections
{
    public static class ConcurrentCollectionExamples
    {
        public static void Demonstrate()
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("CONCURRENT COLLECTIONS");
            Console.WriteLine(new string('=', 50));

            WebRequestCounterDemo();
        }

        private static void WebRequestCounterDemo()
        {
            Console.WriteLine("\nWeb Request Counter");
            Console.WriteLine("------------------");

            // Track how many times each API endpoint gets called
            var requestCounts = new ConcurrentDictionary<string, int>();
            
            // Available API endpoints
            var endpoints = new[] { "/api/users", "/api/orders", "/api/products", "/api/reports" };

            // Simulate multiple web requests hitting our API
            var tasks = new Task[10];
            
            for (int i = 0; i < 10; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    var random = new Random();
                    
                    // Each task simulates 50 web requests
                    for (int request = 0; request < 50; request++)
                    {
                        // Pick a random endpoint
                        var endpoint = endpoints[random.Next(endpoints.Length)];
                        
                        // Increment the counter for this endpoint (thread-safe)
                        requestCounts.AddOrUpdate(endpoint, 1, (key, currentCount) => currentCount + 1);
                        
                        // Simulate request processing time
                        Task.Delay(1).Wait();
                    }
                });
            }

            Task.WaitAll(tasks);

            Console.WriteLine("\nEndpoint Usage:");
            foreach (var endpoint in requestCounts.OrderByDescending(x => x.Value))
            {
                Console.WriteLine($"   {endpoint.Key}: {endpoint.Value} requests");
            }
            
            var totalRequests = requestCounts.Values.Sum();
            Console.WriteLine($"\nTotal requests processed: {totalRequests}");
        }
    }
} 