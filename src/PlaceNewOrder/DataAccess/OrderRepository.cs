using System;
using System.Collections.Generic;
using System.Linq;

namespace PlaceNewOrder.DataAccess
{
    public class OrderRepository : IOrderRepository
    {
        private static Dictionary<Guid, Order> _orders = new Dictionary<Guid, Order>();

        public Order CreateNewOrder(Customer c)
        {
            var o = new Order();
            o.ClientId = c.Id;
            o.Id = Guid.NewGuid();
            _orders.Add(o.Id, o);
            return o;
        }

        public void Save(Order o)
        {
            _orders[o.Id] = o;
        }
    }
}