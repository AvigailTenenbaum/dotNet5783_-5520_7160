
using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

    internal class Cart:BlApi.ICart
    {
    private DalApi.IDal? dal = DalApi.Factory.Get();
    /// <summary>
    /// A method for adding a product to the shopping cart, receives a shopping cart and a product ID and returns a checked shopping cart if everything is correct
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Cart AddProductToCart(BO.Cart cart, int id)
    {
        if (cart.CustomerName == null || cart.CostumerAdress == null || cart.CustomerEmail == null || cart.CustomerEmail == null || !cart.CustomerEmail.Contains('@') || cart.CustomerEmail.Contains(' ') || cart.CustomerEmail.IndexOf('@') == 0 || cart.CustomerEmail.IndexOf('@') == cart.CustomerEmail.Length - 1)
            throw new BO.InCorrectData();
        DO.Product? product;
        try
        {
             product = dal?.Product.GetObject(id);
        }
        catch (DO.NotExist e) { throw e; }
        foreach(BO.OrderItem? orderItem in cart?.Items??throw new BO.NullData())
        {
            if(orderItem?.ProductID==id)
            {
                if (product?.InStock > 0)
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
        if(product?.InStock>0)
        {
            BO.OrderItem? newOrderItem=new BO.OrderItem{ProductID=id,Name=product?.Name??throw new BO.NullData(),Price=product?.Price??throw new BO.NullData(),Amount=1,TotalPrice=product?.Price??throw new NullData()};
            (cart.Items ??= new List<BO.OrderItem?>()).Add(newOrderItem);
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
        if (cart.CustomerName == null || cart.CostumerAdress == null || cart.CustomerEmail == null || cart.CustomerEmail == null || !cart.CustomerEmail.Contains('@') || cart.CustomerEmail.Contains(' ') || cart.CustomerEmail.IndexOf('@') == 0 || cart.CustomerEmail.IndexOf('@') == cart.CustomerEmail.Length - 1)
            throw new BO.InCorrectData();
        if (amount < 0)
            throw new BO.InCorrectData();
        DO.Product? product;
        try
        {
            product = dal?.Product.GetObject(id);
        }
        catch (DO.NotExist e) { throw new BO.NotExist(e); }
        foreach (BO.OrderItem? orderItem in cart?.Items??throw new BO.NullData())
        {

            if (orderItem?.ID == id)
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
        DO.Product? product;
        foreach (BO.OrderItem? orderItem in cart?.Items??throw new BO.NullData())
        {
            try { product = dal?.Product.GetObject(orderItem?.ProductID ?? throw new BO.NullData()); }
            catch(DO.NotExist e) { throw new BO.NotExist(e); }
            if(orderItem?.Amount<=0)
                throw new BO.InCorrectData();
            if (product?.InStock- orderItem?.Amount<0)
                throw new BO.NotPossibleToFillRequest();
        }
        DO.Order newOrder=new DO.Order { CustomerAddress=cart.CostumerAdress,CustomerEmail=cart.CustomerEmail,CustomerName=cart.CustomerName,OrderDate=DateTime.Now};
        int id;
        try { id=dal?.Order.AddObject(newOrder)??throw new BO.NullData(); }
        catch(DO.AllReadyExist e) { throw new BO.AllReadyExist(e); }
        foreach (BO.OrderItem? orderItem in cart.Items)
        {
            DO.OrderItem newOrderItem = new DO.OrderItem { ProductID = orderItem?.ProductID??throw new BO.NullData(), OrderID = id, Price = orderItem?.Price??throw new BO.NullData(), Amount = orderItem?.Amount??throw new BO.NullData() };
            try { id = dal.OrderItem.AddObject(newOrderItem); }
            catch (DO.NotExist e) { throw new BO.NotExist(e); }
           DO.Product product1=(DO.Product)dal.Product.GetObject(orderItem.ProductID)!;
            product1.InStock -= orderItem.Amount;
            dal.Product.UpDateObject(product1);

        }


    }
}

