using System;
namespace PlaceNewOrder.DataAccess
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}