using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;

namespace BlImplementation;
/// <summary>
/// method for product list request
/// </summary>
internal class Product : BlApi.IProduct
{
    private DalApi.IDal? dal = DalApi.Factory.Get();
    public IEnumerable<BO.ProductForList?> GetListOfProducts(Func<BO.ProductForList?, bool>? func = null)
    {
        //var v = dal.Product.GetAllObject();
        IEnumerable<BO.ProductForList?> productList= dal?.Product.GetAllObject().Select(
            product => new BO.ProductForList
            { 
                    ID = product?.ID ?? throw new BO.NullData(),
                    Name = product?.Name?? throw new BO.NullData(), 
                    Category =(BO.Category?)product?.Category ?? throw new BO.NullData(), 
                    Price = product?.Price?? throw new BO.NullData()
             }
        )??throw new BO.NullData();
        if (func!=null)
        {
            return productList.Where(product => func(product));
        }
        return productList;

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
                DO.Product? product = dal?.Product.GetObject(id);
                BO.Product newProduct = new BO.Product
                {
                    ID = product?.ID??throw new NullData(),
                    Name = product?.Name?? throw new NullData(),
                    Category = (BO.Category?)product?.Category,
                    Price = product?.Price?? throw new NullData(),
                    InStock = product?.InStock??0
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
                DO.Product? product = dal?.Product.GetObject(id);
                bool inStock = false;
                if (product?.InStock > 0)
                    inStock = true;
                int amount = 0;
                if (cart.Items?.Count > 0)
                {
                    foreach (BO.OrderItem? orderItem in cart.Items)
                    {
                        if (orderItem?.ID == product?.ID)
                            amount = orderItem?.Amount??0;
                    }
                }
                BO.ProductItem productItem = new BO.ProductItem { ID = product?.ID??throw new BO.NullData(), Name = product?.Name ?? throw new BO.NullData(), Category = (BO.Category?)product?.Category, Price = product?.Price ?? throw new BO.NullData(), InStock = inStock, AmountInCart = amount };
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
            DO.Product p = new DO.Product() {Category=(DO.Category?)product.Category,ID=product.ID,Name=product.Name,Price=product.Price,InStock=product.InStock };
            try
            {
                dal?.Product.AddObject(p);
            }
            catch (DO.AllReadyExist e) { throw new BO.AllReadyExist(e); }
        }
    }
    /// A method that receives a product and deletes it from the list if it is in it and is not found in any order
    /// </summary>
    /// <param name="id"></param>
    public void DeleteProduct(int id)
    {
        IEnumerable<DO.Order?> orders = dal?.Order.GetAllObject()??throw new BO.NullData();
        foreach (DO.Order? order in orders)
        {
            IEnumerable<DO.OrderItem?> orderItems = dal.OrderItem.GetAllObject(item => item?.OrderID == order?.ID);
            foreach (DO.OrderItem? item in orderItems)
            {
                if (item?.ProductID == id)
                {
                    throw new BO.NotPossibleToFillRequest();
                }
            }
        }
        try
        {
            dal.Product.DeleteObject(id);
        }
        catch (DO.NotExist ex) { throw new BO.NotExist(ex); }
    }
    /// <summary>
    /// A method that receives a product and updates the product to the received product if the data is correct
    /// </summary>
    /// <param name="product"></param>
    public void UpdateProduct(BO.Product product)
    {
        if (product.ID <= 0 || product.Name == null || product.Price <= 0||product.InStock<0)
        {
            throw new BO.InCorrectData();
        }
        else
        {
            DO.Product p = new DO.Product() { Category = (DO.Category?)product.Category, ID = product.ID, Name = product.Name, Price = product.Price, InStock = product.InStock };
            try
            {
                dal?.Product.UpDateObject(p);
            }


            catch (DO.NotExist e) { throw e; }
        }
    }
}

