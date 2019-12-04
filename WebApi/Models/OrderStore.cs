using System;
using System.Linq;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class OrderStore
    {
        public List<Order> Orders { get; } = new List<Order>();

        public OrderStore()
        {
            var random = new Random();
            foreach (var item in Enumerable.Range(1, 10))
            {
                Orders.Add(new Order
                {
                    Id = item,
                    OrderNo = DateTime.Now.AddSeconds(random.Next(100, 200)).AddMilliseconds(random.Next(20, 50)).Ticks.ToString(),
                    Quantity = random.Next(1, 10),
                    Amount = Math.Round(((decimal)random.Next(100, 500) / random.Next(2, 6)), 2)
                });
            }
        }
    }
}