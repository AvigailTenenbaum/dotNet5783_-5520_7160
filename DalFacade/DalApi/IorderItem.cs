using DO;

namespace DalApi
{
    public interface IorderItem : ICrud<OrderItem>
    {
        IEnumerable<OrderItem?> GetAllOrderItems(int id);
        OrderItem? GetOrderItem(int orderId, int productId);
    }
}
