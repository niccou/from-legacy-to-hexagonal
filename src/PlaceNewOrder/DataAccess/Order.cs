using System;

namespace PlaceNewOrder.DataAccess
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string Prod { get; internal set; }
        public int Qty { get; internal set; }
    }
}