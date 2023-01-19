using System.Runtime.CompilerServices;

using DalApi;
using DO;

namespace Dal;

internal class DalProduct : Iproduct
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    /// <summary>
    /// A function for adding an object
    /// </summary>
    /// <param name="o1"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int AddObject(Product o1)
    {
        if (DataSource.products.FirstOrDefault(item => item?.ID == o1.ID) != null)
            throw new AllReadyExist();
        DataSource.products.Add(o1);
        return o1.ID;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    /// <summary>
    /// A function that receives an ID number of an object and returns the object if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>

    public Product? GetObject(int id)
    {
        return GetObjectByFilter(item => item?.ID == id);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    /// <summary>
    /// A function that returns an array of all objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product?> GetAllObject(Func<Product?, bool>? func = null)
    {
        if (func == null)
        {
            return DataSource.products.Select(product => product);
        }
        return DataSource.products.Where(product => func(product));
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    /// <summary>
    /// A function that receives an ID number of an object and deletes it if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void DeleteObject(int id)
    {
        Product? p1 = DataSource.products.Find(o => o?.ID == id);
        if (p1 == null)
            throw new NotExist();
        DataSource.products.Remove(p1.Value);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    /// <summary>
    /// Function for updating an object if the ID number exists
    /// </summary>
    /// <param name="p"></param>
    /// <exception cref="Exception"></exception>
    public void UpDateObject(Product p)
    {
        int i = DataSource.products.FindIndex(item => item?.ID == p.ID);
        if (i == -1)
            throw new NotExist();
        DataSource.products[i] = p;
  
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    /// <summary>
    /// Accepts a condition and returns the first object that meets this condition
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="CanNotFound"></exception>
    public Product? GetObjectByFilter(Func<Product?, bool>? func)
    {
        if (DataSource.products.FirstOrDefault(item => func!(item)) == null)
            throw new NotExist();
        return DataSource.products.FirstOrDefault(item => func!(item));

    }
}