using System;

namespace PlaceNewOrder.DataAccess
{
    public interface ICustomerRepository
    {
        Customer GetCustomerById(Guid customerId);
        Customer GetCustomerByName(string nom);
        Customer SaveNewCustomer(Customer newCustomer);
    }
}