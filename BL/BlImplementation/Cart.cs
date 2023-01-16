using BO;
using DalApi;
using System.ComponentModel.DataAnnotations;

namespace BlImplementation;

internal class Cart : BlApi.ICart
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
        DO.Product? product = new DO.Product();
        try
        {
            product = dal!.Product.GetObject(id);



            BO.OrderItem? orderItem = cart.Items?.FirstOrDefault(item => item?.ID == id);
            if (orderItem != null)
            {
                if (product?.InStock > 0)
                {
                    orderItem.Amount += 1;
                    orderItem.TotalPrice += product?.Price ?? throw new BO.NullData();
                }
            }
            else///if the item is not exist in the cart
            {

                if (product?.InStock > 0)
                {
                    BO.OrderItem? newOrderItem = new BO.OrderItem()
                    {
                        ID = product?.ID ?? throw new BO.NullData(),
                        Name = product?.Name ?? throw new BO.NullData(),
                        Price = product?.Price ?? throw new BO.NullData(),
                        Amount = 1,
                        TotalPrice = product?.Price ?? throw new BO.NullData(),
                        ProductID = product?.ID ?? throw new BO.NullData()
                    };

                    cart.Items!.Add(newOrderItem); /*=cart.Items!.Append(newOrderItem);*/
                }
                else
                {
                    throw new BO.OutOfStock();// חריגה שהמוצר לא במלאי

                }
            }
        }
        catch (DO.NotExist ex)
        {
            throw new BO.NotExist(ex);
        }

        cart.TotalPrice += product?.Price ?? throw new BO.NullData();
        return cart;
    
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
        ///cheking by the do if the id exist else throw execption 
        DO.Product product = new DO.Product();
        try
        {
            product = (DO.Product)(dal!.Product.GetObject(id)!);
        }
        catch (DO.NotExist ex)
        {
            throw new BO.NotExist(ex);
        }
        ///if the product in stock

        if (product.InStock < amount)
            throw new BO.NotPossibleToFillRequest();
        ///find the id which answeres the critiorion 

        BO.OrderItem? orderItem = cart.Items!.FirstOrDefault(x => x?.ID == id);


        int x = cart.Items!.ToList().FindIndex(x => x?.ID == id);
        if (orderItem == null)
            throw new BO.NotPossibleToFillRequest();//the object is not exist

        /// if the amount is 0 delete the product 
        if (amount == 0)
        {
            ((List<BO.OrderItem?>)cart.Items!).RemoveAt(x);
        }
        // the update 
        else
        {
            double oldTotalPrice = orderItem.TotalPrice;
            orderItem.Amount = amount;
            orderItem.TotalPrice = amount * orderItem.Price;
            cart.TotalPrice += orderItem.TotalPrice - oldTotalPrice;
        }
        ///return the update cart 
        return cart;
    }
    /// <summary>
    /// Method for placing the order, receives a shopping basket and adds the order if everything is in order
    /// </summary>
    /// <param name="cart"></param>

    public void OrderConfirmation(BO.Cart finalCart)
    {
        if (finalCart.Items?.Count() == 0)
        {
            throw new InCorrectData();
        }
        IEnumerable<DO.Product?> products = dal?.Product.GetAllObject() ?? throw new NullData(); 
        if (finalCart.CostumerAdress == "" || finalCart.CustomerName == "" || finalCart.CustomerEmail == "" 
                || finalCart.CustomerEmail[0] == '@' || finalCart.CustomerEmail[finalCart.CustomerEmail.Length - 1] == '@')
            throw new BO.InCorrectData();

        bool isExistTab = finalCart.CustomerEmail.Contains(' ');
        if (isExistTab)
            throw new BO.InCorrectData();

        bool isExistShtrudel = finalCart.CustomerEmail.Contains('@');
        if (!isExistShtrudel)
            throw new BO.InCorrectData();

        OrderItem? WrongAmount = finalCart?.Items?.Find(o => o?.Amount <= 0);
        if (WrongAmount != null)
            throw new InCorrectData();

        IEnumerable<OrderItem?>? checkExistProduct = finalCart?.Items?.Where
            (oi => products.Any(p => p?.ID == oi?.ProductID)); 
        if (!checkExistProduct?.Any() ?? throw new NullData()) 
            throw new InCorrectData();

        if (!checkExistProduct!.Any(oi => products.Any(p => p?.ID == oi?.ProductID)))
            throw new InCorrectData();

      
        DO.Order finalOrder = new DO.Order
        {
            CustomerAddress = finalCart!.CostumerAdress,   
            CustomerName = finalCart.CustomerName,
            CustomerEmail = finalCart.CustomerEmail,
            OrderDate = DateTime.Now,
            DeliveryDate = null,
            ShipDate = null,
           
        };
        int id;
        try { id=dal.Order.AddObject(finalOrder); }   
        catch (AllReadyExist ex) { throw new AllReadyExist(ex); }
        IEnumerable<DO.OrderItem> orderitems;
        try
        {
            orderitems = from o in finalCart?.Items//converts the all orderItems to DO 
                         let orderItem111 = new DO.OrderItem()
                         {
                             Amount = o.Amount,
                             OrderID = id,
                             Price = o.Price,
                             ProductID = o.ProductID,
                         }
                         let t = dal.OrderItem.AddObject(orderItem111)
                         select orderItem111;

        }
        //Insert the order items details to the order items list.
        catch (DO.NotExist ex) { throw new NotExist(ex); }
        IEnumerable<DO.Product> productsInCart = new List<DO.Product>();
        try
        {
            productsInCart = from o in orderitems
                             let productInCart = dal.Product.GetObject(o.ProductID)
                             let productToSelect = new DO.Product()//Update the new amount.
                             {
                                 ID = productInCart?.ID??throw new NullData(),
                                 InStock = productInCart?.InStock - o.Amount?? throw new NullData(),
                                 Category = productInCart?.Category,
                                 Name = productInCart?.Name,
                                 Price = productInCart?.Price?? throw new NullData()
                             }
                             select productToSelect;
        }
        catch (DO.AllReadyExist ex) { throw new BO.AllReadyExist(ex); }
        try { productsInCart.ToList().ForEach((p => dal.Product.UpDateObject(p))); }//Update the new amount of each product in the cart, in the database. 
        catch (DO.AllReadyExist ex) { throw new BO.AllReadyExist(ex); }
    }

}