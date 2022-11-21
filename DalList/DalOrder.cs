

using DO;

namespace Dal;

public class DalOrder
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
    public Order GetObject(int id)
    {
        for (int i = 0; i < DataSource.orders.Count(); i++)
        {
            if (DataSource.orders[i].ID == id)
                return DataSource.orders[i];
        }
        throw new Exception("ERROR: id is not exist in the array ");
    }
    /// <summary>
    /// A function that returns an array of all objects
    /// </summary>
    /// <returns></returns>
    public List<Order> GetAllObject()
    {
       List<Order> o = new List<Order>();
        for (int i = 0; i < DataSource.orders.Count(); i++)
        {
            o.Add(DataSource.orders[i]);
        }
        return o;
    }
    /// <summary>
    /// A function that receives an ID number of an object and deletes it if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void DeleteObject(int id)
    {
     Order o1=  DataSource.orders.Find(o=>o.ID == id);
        if(o1.Equals(  default(Order)))
            throw new Exception("ERROR: id is not exist in the array ");
        DataSource.orders.Remove(o1);
    }
    /// <summary>
    /// Function for updating an object if the ID number exists
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public void UpDateObject(Order o)
    {
        for (int i = 0; i < DataSource.orders.Count(); i++)
        {
            if (DataSource.orders[i].ID == o.ID)
            {
                DataSource.orders[i] = o;
                return;
            }
        }
        throw new Exception("ERROR: id is not exist in the array ");
    }
}

