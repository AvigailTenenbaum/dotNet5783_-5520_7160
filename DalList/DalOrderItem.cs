﻿using DalApi;
using DO;
using System.Runtime.CompilerServices;

namespace Dal;

internal class DalOrderItem : IorderItem
{
    [MethodImpl(MethodImplOptions.Synchronized)]
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
    [MethodImpl(MethodImplOptions.Synchronized)]

    /// <summary>
    /// A function that receives an ID number of an object and returns the object if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem? GetObject(int id)
    {
        OrderItem? orderItem = GetObjectByFilter(orderItem => orderItem?.ID == id);
        return orderItem;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    /// <summary>
    /// A function that returns an array of all objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem?> GetAllObject(Func<OrderItem?, bool>? func = null)
    {
        if (func == null)
        {
            return DataSource.items.Select(orderItem => orderItem);
        }
        return DataSource.items.Where(orderItem => func(orderItem));

    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    /// <summary>
    /// A function that receives an ID number of an object and deletes it if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void DeleteObject(int id)
    {
        OrderItem? o1 = DataSource.items.Find(o => o?.ID == id);
        if (o1 == null)
            throw new NotExist();
        DataSource.items.Remove(o1.Value);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    /// <summary>
    /// Function for updating an object if the ID number exists
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public void UpDateObject(OrderItem o)
    {
        int i = DataSource.items.FindIndex(item => item?.ID == o.ID);
        if (i == -1)
            throw new NotExist();
        DataSource.items[i] = o;

    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    /// <summary>
    /// get all the orderItems with this orderId
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public IEnumerable<OrderItem?> GetAllOrderItems(int orderId)
    {
        return GetAllObject(item => item?.OrderID == orderId);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    /// <summary>
    /// get orderItem by two orderId
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem? GetOrderItem(int orderId, int productId)
    {
        return GetObjectByFilter(item => item?.OrderID == orderId && item?.ProductID == productId);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    /// <summary>
    /// Accepts a condition and returns the first object that meets this condition
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="CanNotFound"></exception>
    public OrderItem? GetObjectByFilter(Func<OrderItem?, bool>? func)
    {
        if (DataSource.items.FirstOrDefault(item => func!(item)) == null)
            throw new NotExist();
        return DataSource.items.FirstOrDefault(item => func!(item));

    }

}
