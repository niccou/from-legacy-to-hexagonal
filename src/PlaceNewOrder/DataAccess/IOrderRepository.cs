namespace PlaceNewOrder.DataAccess
{
    public interface IOrderRepository
    {
        Order CreateNewOrder(Customer customer);

        void Save(Order order);
    }
}