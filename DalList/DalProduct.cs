

using DO;
using DalApi;

namespace Dal;

internal class DalProduct :Iproduct
{
    /// <summary>
    /// A function for adding an object
    /// </summary>
    /// <param name="o1"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int AddObject(Product o1)
    {
        for (int i = 0; i < DataSource.orders.Count; i++)
        {
            if (DataSource.products[i].ID == o1.ID)
            {
                throw new Exception("ERROR: id is already exist in the array ");
            }
        }
        DataSource.products.Add( o1);
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
        Product p1=DataSource.products.Find(o=>o.ID == id); 
        if(p1.Equals(default(Product)))
            throw new Exception("ERROR: id is not exist in the array ");
        return p1;
        
    }
    /// <summary>
    /// A function that returns an array of all objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product> GetAllObject()
    {
        return DataSource.products.Select(product => product);
    }
    /// <summary>
    /// A function that receives an ID number of an object and deletes it if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void DeleteObject(int id)
    {
        Product p1 = DataSource.products.Find(o => o.ID == id);
        if (p1.Equals(default(Product)))
            throw new Exception("ERROR: id is not exist in the array ");
      DataSource.products.Remove(p1);   
    }
    /// <summary>
    /// Function for updating an object if the ID number exists
    /// </summary>
    /// <param name="p"></param>
    /// <exception cref="Exception"></exception>
    public void UpDateObject(Product p)
    {
        for (int i = 0; i < DataSource.products.Count; i++)
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