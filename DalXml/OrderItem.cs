using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
internal class OrderItem:IorderItem
{
    string dir = "..\\xml\\";
    static string orderItemPath = @"OrderItem.xml";

   // public int ID { get; internal set; }

    /// <summary>
    /// A function for adding an object
    /// </summary>
    /// <param name="o1"></param>
    /// <returns></returns>
    public int AddObject(DO.OrderItem o1)
    {
        List<DO.OrderItem?> orders = Tools<DO.OrderItem?>.LoadListFromXml(dir+orderItemPath);
        if (o1.ID== null||o1.ID==0)//check because the update action
        {
            o1.ID = Tools<int>.GetLastOrderItemID();
        }
        orders.Add(o1);
        Tools<DO.OrderItem?>.SaveListToXml(orders, dir+orderItemPath);
        return o1.ID;
    }
    /// <summary>
    /// A function that receives an ID number of an object and returns the object if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public DO.OrderItem? GetObject(int id)
    {
        List<DO.OrderItem?> orders = Tools<DO.OrderItem?>.LoadListFromXml(dir+orderItemPath);
        return orders.FirstOrDefault(order => order?.ID == id) ?? throw new NotExist();
    }
    /// <summary>
    /// A function that returns an array of all objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<DO.OrderItem?> GetAllObject(Func<DO.OrderItem?, bool>? func = null)
    {
        List<DO.OrderItem?> orders = Tools<DO.OrderItem?>.LoadListFromXml(dir+orderItemPath);
        if (func == null)
        {
            return orders.Select(order => order).OrderBy(order => order?.ID);
        }
        return orders.Where(order => func(order)).OrderBy(order => order?.ID);

    }
    /// <summary>
    /// A function that receives an ID number of an object and deletes it if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void DeleteObject(int id)
    {
        List<DO.OrderItem?> orders = Tools<DO.OrderItem?>.LoadListFromXml(dir+orderItemPath);
        if (orders.RemoveAll(item => item?.ID == id) == 0)
        {
            throw new NotExist();
        }
        Tools<DO.OrderItem?>.SaveListToXml(orders, dir+orderItemPath);
    }
    /// <summary>
    /// Function for updating an object if the ID number exists
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public void UpDateObject(DO.OrderItem o)
    {
        DeleteObject(o.ID);
        AddObject(o);
    }
    /// <summary>
    /// get all the orderItems with this orderId
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public IEnumerable<DO.OrderItem?> GetAllOrderItems(int orderId)
    {
        return GetAllObject(item => item?.OrderID == orderId);
    }
    /// <summary>
    /// get orderItem by two orderId
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public DO.OrderItem? GetOrderItem(int orderId, int productId)
    {
        return GetObjectByFilter(item => item?.OrderID == orderId && item?.ProductID == productId);
    }
    /// <summary>
    /// Accepts a condition and returns the first object that meets this condition
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="CanNotFound"></exception>
    public DO.OrderItem? GetObjectByFilter(Func<DO.OrderItem?, bool>? func)
    {
        List<DO.OrderItem?> orders = Tools<DO.OrderItem?>.LoadListFromXml(dir+orderItemPath);
        return orders.FirstOrDefault(order => func!(order)) ?? throw new NotExist();

    }

}
