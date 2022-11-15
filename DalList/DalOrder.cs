

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
        DataSource.orders[DataSource._indexOfOrder++] = o1;
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
        for (int i = 0; i < DataSource._indexOfOrder; i++)
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
    public Order[] GetAllObject()
    {
        Order[] o = new Order[DataSource._indexOfOrder ];
        for (int i = 0; i < DataSource._indexOfOrder; i++)
        {
            o[i] = DataSource.orders[i];
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
        int index = Array.FindIndex(DataSource.orders, o => o.ID == id);
        if (index == -1)
            throw new Exception("ERROR: id is not exist in the array ");
        if (DataSource._indexOfOrder == index)
            DataSource.orders[index] = default(Order);
        else
            DataSource.orders[index] = DataSource.orders[DataSource._indexOfOrder - 1];
        DataSource._indexOfOrder--;
    }
    /// <summary>
    /// Function for updating an object if the ID number exists
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public void UpDateObject(Order o)
    {
        for (int i = 0; i < DataSource._indexOfOrder; i++)
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

