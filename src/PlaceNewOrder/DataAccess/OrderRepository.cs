using System;
using System.Collections.Generic;
using System.Linq;

namespace PlaceNewOrder.DataAccess
{
    public class OrderRepository
    {
        private static Dictionary<Guid, Order> _orders = new Dictionary<Guid, Order>();

        internal Order CreateNewOrder(Customer c)
        {
            var o = new Order();
            o.ClientId = c.Id;
            o.Id = Guid.NewGuid();
            _orders.Add(o.Id, o);
            return o;
        }

        internal void Save(Order o)
        {
            _orders[o.Id] = o;
        }
    }
}