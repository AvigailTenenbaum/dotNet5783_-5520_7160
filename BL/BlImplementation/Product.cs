using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using Dal;
using DalApi;

namespace BlImplementation;
/// <summary>
/// method for product list request
/// </summary>
internal class Product : BlApi.IProduct
{
    private IDal _dal = new DalList();
    public IEnumerable<BO.ProductForList> GetListOfProducts()
    {
        var v = _dal.Product.GetAllObject();
        return _dal.Product.GetAllObject().Select(product => new BO.ProductForList { ID = product.ID, Name = product.Name, Category = (BO.Category)product.Category, Price = product.Price });
    }
    /// <summary>
    /// A method that receives a product ID number and returns product details if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidId"></exception>
    public BO.Product GetProductDetails(int id)
    {
        if (id > 0)
        {

            try
            {
                DO.Product product = _dal.Product.GetObject(id);
                BO.Product newProduct = new BO.Product
                {
                    ID = product.ID,
                    Name = product.Name,
                    Category = (BO.Category)product.Category,
                    Price = product.Price,
                    InStock = product.InStock
                };
                return newProduct;
            }
            catch (DO.NotExist e) { throw new BO.NotExist(e); }

        }
        else
            throw new BO.InCorrectData();
    }
    /// <summary>
    /// A method that receives an ID number and shopping basket of a product and returns product item details if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cart"></param>
    /// <returns></returns>
    public BO.ProductItem GetProductDetails(int id, BO.Cart cart)
    {
        if (id > 0)
        {
            try
            {
                DO.Product product = _dal.Product.GetObject(id);
                bool inStock = false;
                if (product.InStock > 0)
                    inStock = true;
                int amount = 0;
                if (cart.Items.Count > 0)
                {
                    foreach (BO.OrderItem orderItem in cart.Items)
                    {
                        if (orderItem.ID == product.ID)
                            amount = orderItem.Amount;
                    }
                }
                BO.ProductItem productItem = new BO.ProductItem { ID = product.ID, Name = product.Name, Category = (BO.Category)product.Category, Price = product.Price, InStock = inStock, AmountInCart = amount };
                return productItem;
            }
            catch (DO.NotExist e) { throw new BO.NotExist(e); }
        }
        else
            throw new BO.InCorrectData();
    }
    /// <summary>
    /// A method that receives a product and adds it to the list if the data is correct
    /// </summary>
    /// <param name="product"></param>
    public void AddProduct(BO.Product product)
    {
        if (product.ID <= 0 || product.Name == null || product.Price <= 0)
        {
            throw new BO.InCorrectData();
        }
        else
        {
            DO.Product p = new DO.Product() {Category=(DO.Category)product.Category,ID=product.ID,Name=product.Name,Price=product.Price,InStock=product.InStock };
            try
            {
                _dal.Product.AddObject(p);
            }
            catch (DO.AllReadyExist e) { throw new BO.AllReadyExist(e); }
        }
    }
    /// A method that receives a product and deletes it from the list if it is in it and is not found in any order
    /// </summary>
    /// <param name="id"></param>
    public void DeleteProduct(int id)
    {
        IEnumerable<DO.Order> orders = _dal.Order.GetAllObject();
        foreach (DO.Order order in orders)
        {
            IEnumerable<DO.OrderItem> orderItems = _dal.OrderItem.GetAllOrderItems(order.ID);
            foreach (DO.OrderItem item in orderItems)
            {
                if (item.ProductID == id)
                {
                    throw new BO.NotPossibleToFillRequest();
                }
            }
        }
        try
        {
            _dal.Product.DeleteObject(id);
        }
        catch (DO.NotExist ex) { throw new BO.NotExist(ex); }
    }
    /// <summary>
    /// A method that receives a product and updates the product to the received product if the data is correct
    /// </summary>
    /// <param name="product"></param>
    public void UpdateProduct(BO.Product product)
    {
        if (product.ID <= 0 || product.Name == null || product.Price <= 0)
        {
            throw new BO.InCorrectData();
        }
        else
        {
            DO.Product p = new DO.Product() { Category = (DO.Category)product.Category, ID = product.ID, Name = product.Name, Price = product.Price, InStock = product.InStock };
            try
            {
                _dal.Product.UpDateObject(p);
            }


            catch (DO.NotExist e) { throw e; }
        }
    }
}

