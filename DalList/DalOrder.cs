﻿

using DO;
using DalApi;

namespace Dal;
internal class DalOrder : Iorder
{
    /// <summary>
    /// A function for adding an object
    /// </summary>
    /// <param name="o1"></param>
    /// <returns></returns>
    public int AddObject(Order o1)
    {
        o1.ID = DataSource.getLastOrderID();
        DataSource.orders.Add(o1);
        return o1.ID;
    }
    /// <summary>
    /// A function that receives an ID number of an object and returns the object if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Order? GetObject(int id)
    {
        return GetObjectByFilter(delegate (Order? order) { return order.Value.ID == id; });
        throw new NotExist();
    }
    /// <summary>
    /// A function that returns an array of all objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order?> GetAllObject(Func<Order?, bool>? func = null)
    {
        if (func == null)
        {
            return DataSource.orders.Select(order => order);
        }
        return DataSource.orders.Where(order => func(order));
    }
    /// <summary>
    /// A function that receives an ID number of an object and deletes it if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void DeleteObject(int id)
    {
        Order o1 = DataSource.orders.Find(o => o.ID == id);
        if (o1.Equals(default(Order)))
            throw new NotExist();
        DataSource.orders.Remove(o1);
    }
    /// <summary>
    /// Function for updating an object if the ID number exists
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public void UpDateObject(Order? o)
    {
        for (int i = 0; i < DataSource.orders.Count(); i++)
        {
            if (DataSource.orders[i].ID == o.ID)
            {
                DataSource.orders[i] = o;
                return;
            }
        }
        throw new NotExist();
    }
    /// <summary>
    /// Accepts a condition and returns the first object that meets this condition
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="CanNotFound"></exception>
    public Order? GetObjectByFilter(Func<Order?, bool>? func)
    {
        foreach(var order in DataSource.orders)
        {
            if(func(order))
            {
                return order;
            }
        }
        throw new NotExist();
    }
}

