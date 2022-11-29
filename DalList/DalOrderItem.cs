

using DO;
using DalApi;
using System.Collections.Generic;

namespace Dal;

internal class DalOrderItem :IorderItem
{
    /// <summary>
    /// A function for adding an object
    /// </summary>
    /// <param name="o1"></param>
    /// <returns></returns>
    public int AddObject(OrderItem o1)
    {
        o1.ID = DataSource.getLastOrderItemsID();
        DataSource.items.Add(o1);
        return o1.ID;
    }
    /// <summary>
    /// A function that receives an ID number of an object and returns the object if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem GetObject(int id)
    {
        for (int i = 0; i < DataSource.orders.Count; i++)
        {
            if (DataSource.items[i].ID == id)
                return DataSource.items[i];
        }
         throw new NotExist();
    }
    /// <summary>
    /// A function that returns an array of all objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem> GetAllObject()
    {
        return DataSource.items.Select(orderItem => orderItem);
    }
    /// <summary>
    /// A function that receives an ID number of an object and deletes it if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void DeleteObject(int id)
    {
        OrderItem o1 = DataSource.items.Find(o => o.ID == id);
        if (o1.Equals(default(OrderItem)))
            throw new NotExist();
        DataSource.items.Remove(o1);
    }
    /// <summary>
    /// Function for updating an object if the ID number exists
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public void UpDateObject(OrderItem o)
    {
        for (int i = 0; i < DataSource.items.Count(); i++)
        {
            if (DataSource.items[i].ID == o.ID)
            {
                DataSource.items[i] = o;
                return;
            }
        }
        throw new NotExist();
    }
    /// <summary>
    /// get all the orderItems with this orderId
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
   public IEnumerable<OrderItem> GetAllOrderItems(int orderId)
    {
       return DataSource.items.FindAll(item => item.OrderID == orderId);
    }
    /// <summary>
    /// get orderItem by two orderId
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
   public OrderItem GetOrderItem(int orderId, int productId)
    {
        for(int i=0;i< DataSource.items.Count;i++)
        {
            if (DataSource.items[i].ProductID == productId && DataSource.items[i].OrderID==orderId) {
                return DataSource.items[i];
            }
        }
            throw new NotExist();
    }

   
}
