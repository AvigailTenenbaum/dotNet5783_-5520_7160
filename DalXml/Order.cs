﻿using DalApi;
using DO;
using System.Runtime.CompilerServices;
namespace Dal;
internal class Order : Iorder
{
    string dir = "..\\xml\\";
    static string orderPath = @"Order.xml";
    [MethodImpl(MethodImplOptions.Synchronized)]
    //static XElement? 
    /// <summary>
    /// A function for adding an object
    /// </summary>
    /// <param name="o1"></param>
    /// <returns></returns>
    public int AddObject(DO.Order o1)
    {
        List<DO.Order?> orders = Tools<DO.Order?>.LoadListFromXml(dir + orderPath);
        if (o1.ID == null || o1.ID == 0)//check because the update action
        {
            o1.ID = Tools<int>.GetLastOrderID();
        }
        orders.Add(o1);
        Tools<DO.Order?>.SaveListToXml(orders, dir + orderPath);
        return o1.ID;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    /// <summary>
    /// A function that receives an ID number of an object and returns the object if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public DO.Order? GetObject(int id)
    {
        List<DO.Order?> orders = Tools<DO.Order?>.LoadListFromXml(dir + orderPath);
        return orders.FirstOrDefault(order => order?.ID == id) ?? throw new NotExist();
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    /// <summary>
    /// A function that returns an array of all objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<DO.Order?> GetAllObject(Func<DO.Order?, bool>? func = null)
    {
        List<DO.Order?> orders = Tools<DO.Order?>.LoadListFromXml(dir + orderPath);
        if (func == null)
        {
            return orders.Select(order => order).OrderBy(order => order?.ID);
        }
        return orders.Where(order => func(order)).OrderBy(order => order?.ID);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    /// <summary>
    /// A function that receives an ID number of an object and deletes it if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void DeleteObject(int id)
    {
        List<DO.Order?> orders = Tools<DO.Order?>.LoadListFromXml(dir + orderPath);
        if (orders.RemoveAll(item => item?.ID == id) == 0)
        {
            throw new NotExist();
        }
        Tools<DO.Order?>.SaveListToXml(orders, dir + orderPath);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    /// <summary>
    /// Function for updating an object if the ID number exists
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public void UpDateObject(DO.Order o)
    {
        DeleteObject(o.ID);
        AddObject(o);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    /// <summary>
    /// Accepts a condition and returns the first object that meets this condition
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="CanNotFound"></exception>
    public DO.Order? GetObjectByFilter(Func<DO.Order?, bool>? func)
    {
        List<DO.Order?> orders = Tools<DO.Order?>.LoadListFromXml(dir + orderPath);
        return orders.FirstOrDefault(order => func!(order)) ?? throw new NotExist();
    }
}
