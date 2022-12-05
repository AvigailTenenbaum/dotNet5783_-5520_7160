

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
            if (DataSource.products[i].Value.ID == o1.ID)
            {
                throw new AllReadyExist();
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
        Product product = GetObjectByFilter(product => product.Value.ID == id);
        return product;
    }
    /// <summary>
    /// A function that returns an array of all objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product?> GetAllObject(Func<Product?, bool>? func=null)
    {
        if (func == null)
        {
            return DataSource.products.Select(product => product);
        }
        return DataSource.products.Where(product => func(product));
    }
    /// <summary>
    /// A function that receives an ID number of an object and deletes it if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void DeleteObject(int id)
    {
        Product? p1 = DataSource.products.Find(o => o.Value.ID == id);
        if (p1==null)
            throw new NotExist();
      DataSource.products.Remove(p1.Value);   
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
            if (DataSource.products[i].Value.ID == p.ID)
            {
                DataSource.products[i] = p;
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
    public Product GetObjectByFilter(Func<Product?, bool>? func)
    {
        foreach (var product in DataSource.products)
        {
            if (func(product))
            {
                return product.Value;
            }
        }
        throw new NotExist();
    }
}