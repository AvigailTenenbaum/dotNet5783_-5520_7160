

using DO;

namespace Dal;

public class DalProduct
{
    /// <summary>
    /// A function for adding an object
    /// </summary>
    /// <param name="o1"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int AddObject(Product o1)
    {
        for (int i = 0; i < DataSource._indexOfProduct; i++)
        {
            if (DataSource.products[i].ID == o1.ID)
            {
                throw new Exception("ERROR: id is already exist in the array ");
            }
        }
        DataSource.products[DataSource._indexOfProduct++] = o1;
        return o1.ID;
    }
    /// <summary>
    /// A function that receives an ID number of an object and returns the object if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product GetObject(int id)
    {
        for(int i=0;i<DataSource._indexOfProduct;i++)
        {
            if(DataSource.products[i].ID == id)
                return DataSource.products[i];
        }
        throw new Exception("ERROR: id is not exist in the array ");
    }
    /// <summary>
    /// A function that returns an array of all objects
    /// </summary>
    /// <returns></returns>
    public Product[] GetAllObject()
    {
        Product[] p = new Product[DataSource._indexOfProduct];
        for (int i = 0; i < DataSource._indexOfProduct; i++)
        {
            p[i] = DataSource.products[i];
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
        int index=Array.FindIndex(DataSource.products,o => o.ID == id);
        if(index == -1)
            throw new Exception("ERROR: id is not exist in the array ");
        if(DataSource._indexOfProduct==index)
            DataSource.products[index]=default(Product);    
        else
            DataSource.products[index] = DataSource.products[DataSource._indexOfProduct-1];
        DataSource._indexOfProduct--;
    }
    /// <summary>
    /// Function for updating an object if the ID number exists
    /// </summary>
    /// <param name="p"></param>
    /// <exception cref="Exception"></exception>
    public void UpDateObject(Product p)
    {
        for (int i = 0; i < DataSource._indexOfProduct; i++)
        {
            if (DataSource.products[i].ID == p.ID)
            {
                DataSource.products[i] = p;
                return;
            }
        }
        throw new Exception("ERROR: id is not exist in the array ");
    }
}