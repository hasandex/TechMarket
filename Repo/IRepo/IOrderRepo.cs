namespace TechMarket.Repo.IRepo
{
    public interface IOrderRepo
    {
        Task<IEnumerable<Order>> GetOrders();
        Task<IEnumerable<Order>> GetOrders(string userId);
        Task<Order> GetOrderById(int orderId);
        int Add(Order order);
    }
}
