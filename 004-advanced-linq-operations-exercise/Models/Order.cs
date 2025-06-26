using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _004_advanced_linq_operations_exercise.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Amount { get; set; }

        // Navigation property
        public Customer Customer { get; set; } = new Customer();
        public Product Product { get; set; } = new Product();
    }
}
