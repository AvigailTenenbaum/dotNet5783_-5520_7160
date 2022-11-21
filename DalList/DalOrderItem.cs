

using DO;

namespace Dal;

public class DalOrderItem
{
    /// <summary>
    /// A function for adding an object
    /// </summary>
    /// <param name="o1"></param>
    /// <returns></returns>
    public int AddObject(OrderItem o1)
    {
        o1.ID = DataSource.getLastOrderItemsID();
        DataSource.items[DataSource.items.Count] = o1;
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
        for (int i = 0; i < DataSource._indexOfOrderItem; i++)
        {
            if (DataSource.items[i].ID == id)
                return DataSource.items[i];
        }
         throw new Exception("ERROR: id is not exist in the array ");
    }
    /// <summary>
    /// A function that returns an array of all objects
    /// </summary>
    /// <returns></returns>
    public OrderItem[] GetAllObject()
    {
        OrderItem[] p = new OrderItem[DataSource._indexOfOrderItem ];
        for (int i = 0; i < DataSource._indexOfOrderItem; i++)
        {
            p[i] = DataSource.items[i];
        }
        return p;
    }
    /// <summary>
    /// A function that receives an ID number of an object and deletes it if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void DeleteObject(int id)
    {
        int index = Array.FindIndex(DataSource.items, o => o.ID == id);
        if (index == -1)
            throw new Exception("ERROR: id is not exist in the array ");
        if (DataSource._indexOfOrderItem == index)
            DataSource.items[index] = default(OrderItem);
        else
            DataSource.items[index] = DataSource.items[DataSource._indexOfOrderItem - 1];
        DataSource._indexOfOrderItem--;
    }
    /// <summary>
    /// Function for updating an object if the ID number exists
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public void UpDateObject(OrderItem o)
    {
        for (int i = 0; i < DataSource._indexOfOrderItem; i++)
        {
            if (DataSource.items[i].ID == o.ID)
            {
                DataSource.items[i] = o;
                return;
            }
        }
        throw new Exception("ERROR: id is not exist in the array ");
    }
}
