using System;
using System.Collections.Generic;
using System.Linq;

namespace PlaceNewOrder.DataAccess
{
    public class CustomerRepository
    {
        private static Dictionary<Guid, Customer> _customers = new Dictionary<Guid, Customer>();

        public Customer GetCustomerById(Guid customerId)
        {
            if (_customers.TryGetValue(customerId, out Customer customer))
            {
                return customer;
            }

            return null;
        }

        public Customer GetCustomerByName(string nom) => _customers.Values.FirstOrDefault(customer=>customer.Nom == nom);

        public Customer SaveNewCustomer(Customer newCustomer)
        {
            newCustomer.Id = Guid.NewGuid();
            _customers.Add(newCustomer.Id, newCustomer);
            return GetCustomerById(newCustomer.Id);
        }
    }
}