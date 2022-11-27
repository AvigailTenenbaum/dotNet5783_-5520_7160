
using Dal;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

    internal class Cart:BlApi.ICart
    {
    private IDal _dal = new DalList();
    /// <summary>
    /// A method for adding a product to the shopping cart, receives a shopping cart and a product ID and returns a checked shopping cart if everything is correct
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Cart AddProductToCart(BO.Cart cart, int id)
    {
        DO.Product product;
        try
        {
             product = _dal.Product.GetObject(id);
        }
        catch (DO.NotExist e) { throw e; }
        foreach(BO.OrderItem orderItem in cart.Items)
        {
            if(orderItem.ProductID==id)
            {
                if (product.InStock > 0)
                {
                    orderItem.Amount++;
                    orderItem.TotalPrice += orderItem.Price;
                    cart.TotalPrice += orderItem.Price;
                    return cart;
                }
                else
                    throw new BO.OutOfStock();
            }

        }
        if(product.InStock>0)
        {
            BO.OrderItem newOrderItem=new BO.OrderItem{ProductID=id,Name=product.Name,Price=product.Price,Amount=1,TotalPrice=product.Price};
            cart.Items.Add(newOrderItem);
            cart.TotalPrice += newOrderItem.TotalPrice;
            return cart;
        }
        else
            throw new BO.OutOfStock();
    }
    /// <summary>
    /// A method for updating the quantity of a product in the basket, receives a shopping basket, identifies a new product and quantity and returns an updated basket if everything is correct
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public BO.Cart UpdateProductAmount(BO.Cart cart, int id, int amount)
    {
        if (amount < 0)
            throw new BO.InCorrectData();
        DO.Product product;
        try
        {
            product = _dal.Product.GetObject(id);
        }
        catch (DO.NotExist e) { throw e; }
        foreach (BO.OrderItem orderItem in cart.Items)
        {

            if (orderItem.ID == id)
            {
                if (amount == orderItem.Amount)
                    return cart;
                if (amount == 0)
                {
                    cart.TotalPrice -= orderItem.TotalPrice;
                    cart.Items.Remove(orderItem);
                    return cart;
                }
                if (orderItem.Amount < amount)
                {
                    cart.TotalPrice += (amount - orderItem.Amount) * orderItem.Price;
                    orderItem.Amount = amount;
                    orderItem.TotalPrice = amount * orderItem.Price;
                    return cart;
                }
                if (orderItem.Amount > amount)
                {
                    cart.TotalPrice -= (orderItem.Amount - amount) * orderItem.Price;
                    orderItem.Amount = amount;
                    orderItem.TotalPrice = amount * orderItem.Price;
                    return cart;
                }
            }
        }
        throw new BO.NotPossibleToFillRequest();
    }
    /// <summary>
    /// Method for placing the order, receives a shopping basket and adds the order if everything is in order
    /// </summary>
    /// <param name="cart"></param>
    public void OrderConfirmation(BO.Cart cart)
    {
        if (cart.CustomerName == null || cart.CostumerAdress == null || cart.CustomerEmail == null || cart.CustomerEmail==null || !cart.CustomerEmail.Contains('@') || cart.CustomerEmail.Contains(' ') || cart.CustomerEmail.IndexOf('@') == 0 || cart.CustomerEmail.IndexOf('@') == cart.CustomerEmail.Length - 1)
            throw new BO.InCorrectData();
        DO.Product product;
        foreach (BO.OrderItem orderItem in cart.Items)
        {
            try { product = _dal.Product.GetObject(orderItem.ProductID); }
            catch(DO.NotExist e) { throw e; }
            if(orderItem.Amount<=0)
                throw new BO.InCorrectData();
            if (product.InStock- orderItem.Amount<0)
                throw new BO.NotPossibleToFillRequest();
        }
        DO.Order newOrder=new DO.Order { CustomerAddress=cart.CostumerAdress,CustomerEmail=cart.CustomerEmail,CustomerName=cart.CustomerName,OrderDate=DateTime.Now};
        int id;
        try { id=_dal.Order.AddObject(newOrder); }
        catch(DO.AllReadyExist e) { throw e; }
        foreach (BO.OrderItem orderItem in cart.Items)
        {
            DO.OrderItem newOrderItem = new DO.OrderItem { ProductID = orderItem.ProductID, OrderID = id, Price = orderItem.Price, Amount = orderItem.Amount };
            try { id = _dal.OrderItem.AddObject(newOrderItem); }
            catch (DO.NotExist e) { throw e; }
            DO.Product product1=_dal.Product.GetObject(orderItem.ProductID);
            product1.InStock -= orderItem.Amount;
            _dal.Product.UpDateObject(product1);
        }


    }
}

