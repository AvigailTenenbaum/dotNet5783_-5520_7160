using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IorderItem: ICrud<OrderItem>
    {
        List<OrderItem> GetAllOrderItems(int id);
        OrderItem GetOrderItem(int orderId, int productId);
    }
}
