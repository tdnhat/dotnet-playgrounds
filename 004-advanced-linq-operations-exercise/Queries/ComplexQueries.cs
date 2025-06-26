using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _004_advanced_linq_operations_exercise.Models;

namespace _004_advanced_linq_operations_exercise.Queries
{
    public static class ComplexQueries
    {
        public static void RunInnerJoinQuery(Customer[] customers, Order[] orders, Product[] products)
        {
            Console.WriteLine("Inner Join Query - Customers with Orders and Products:");

            // query syntax
            var query = from customer in customers
                        join order in orders on customer.Id equals order.CustomerId
                        join product in products on order.ProductId equals product.Id
                        select new
                        {
                            CustomerName = customer.Name,
                            OrderDate = order.OrderDate,
                            ProductName = product.Name,
                            Amount = order.Amount,
                            Price = product.Price
                        };

            // method syntax
            // var query = customers.Join(orders,
            //                         customer => customer.Id,
            //                         order => order.CustomerId,
            //                         (customer, order) => new { customer, order })
            //                     .Join(products,
            //                         co => co.order.ProductId,
            //                         product => product.Id,
            //                         (co, product) => new
            //                         {
            //                             CustomerName = co.customer.Name,
            //                             OrderDate = co.order.OrderDate,
            //                             ProductName = product.Name,
            //                             Amount = co.order.Amount,
            //                             Price = product.Price
            //                         });

            // Deferred execution
            foreach (var item in query)
            {
                Console.WriteLine($"Customer: {item.CustomerName}, Order Date: {item.OrderDate}, Product: {item.ProductName}, Amount: {item.Amount}, Price: {item.Price:C}");
            }
        }

        public static void RunGroupJoinQuery(Customer[] customers, Order[] orders)
        {
            Console.WriteLine("\nGroup Join Query - Customers with their Orders:");

            // query syntax
            var query = from customer in customers
                        join order in orders on customer.Id equals order.CustomerId into customerOrders
                        select new
                        {
                            CustomerName = customer.Name,
                            Orders = customerOrders
                        };

            //  method syntax
            // var query = customers.Join(orders,
            //                         customer => customer.Id,
            //                         order => order.CustomerId,
            //                         (customer, order) => new { customer, order })
            //                     .GroupBy(co => co.customer)
            //                     .Select(g => new
            //                     {
            //                         CustomerName = g.Key.Name,
            //                         Orders = g.Select(co => co.order).ToList()
            //                     });

            // Deferred execution
            foreach (var item in query)
            {
                Console.WriteLine($"Customer: {item.CustomerName}");
                foreach (var order in item.Orders)
                {
                    Console.WriteLine($"  Order ID: {order.Id}, Order Date: {order.OrderDate}, Amount: {order.Amount}");
                }
            }
        }

        // Generate a report of customer purchases grouped by region
        public static void RunGroupByRegionQuery(Customer[] customers, Order[] orders)
        {
            Console.WriteLine("\nGroup By Region Query - Customer Purchases:");

            var query = from customer in customers
                        join order in orders on customer.Id equals order.CustomerId
                        group order by customer.Region into regionGroup
                        select new
                        {
                            Region = regionGroup.Key,
                            TotalSales = regionGroup.Sum(o => o.Amount * o.Product.Price),
                            OrderCount = regionGroup.Count()
                        };

            // var query = customers.Join(orders,
            //                             customer => customer.Id,
            //                             order => order.CustomerId,
            //                             (customer, order) => new { customer, order })
            //                         .GroupBy(oc => oc.customer.Region)
            //                         .Select(g => new
            //                         {
            //                             Region = g.Key,
            //                             TotalSales = g.Sum(o => o.order.Amount * o.order.Product.Price),
            //                             OrderCount = g.Count()
            //                         });

            foreach (var item in query)
            {
                Console.WriteLine($"Region: {item.Region}, Total Sales: {item.TotalSales:C}, Order Count: {item.OrderCount}");
            }
        }
    }
}