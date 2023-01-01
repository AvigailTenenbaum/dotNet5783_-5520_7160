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
                    orderItem.TotalPrice = product?.Price ?? throw new BO.NullData();
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
                    cart.Items = (List<BO.OrderItem?>?)cart.Items!.Append(newOrderItem);
                }
                else
                {
                    throw new BO.InCorrectData();// חריגה שהמוצר לא במלאי

                }
            }
        }
        catch (DO.NotExist ex)
        {
            throw new BO.NotExist(ex);
        }

        cart.TotalPrice += product?.Price ?? throw new BO.NullData();
        return cart;
        //if (cart.CustomerName == null || cart.CostumerAdress == null || cart.CustomerEmail == null || cart.CustomerEmail == null || !cart.CustomerEmail.Contains('@') || cart.CustomerEmail.Contains(' ') || cart.CustomerEmail.IndexOf('@') == 0 || cart.CustomerEmail.IndexOf('@') == cart.CustomerEmail.Length - 1)
        //    throw new BO.InCorrectData();
        //DO.Product? product;
        //try
        //{
        //     product = dal?.Product.GetObject(id);
        //}
        //catch (DO.NotExist e) { throw e; }
        //foreach(BO.OrderItem? orderItem in cart?.Items??throw new BO.NullData())
        //{
        //    if(orderItem?.ProductID==id)
        //    {
        //        if (product?.InStock > 0)
        //        {
        //            orderItem.Amount++;
        //            orderItem.TotalPrice += orderItem.Price;
        //            cart.TotalPrice += orderItem.Price;
        //            return cart;
        //        }
        //        else
        //            throw new BO.OutOfStock();
        //    }

        //}
        //if(product?.InStock>0)
        //{
        //    BO.OrderItem? newOrderItem=new BO.OrderItem{ProductID=id,Name=product?.Name??throw new BO.NullData(),Price=product?.Price??throw new BO.NullData(),Amount=1,TotalPrice=product?.Price??throw new NullData()};
        //    (cart.Items ??= new List<BO.OrderItem?>()).Add(newOrderItem);
        //    cart.TotalPrice += newOrderItem.TotalPrice;
        //    return cart;
        //}
        //else
        //    throw new BO.OutOfStock();
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
        ///here we create the update 
        else
        {
            double oldTotalPrice = orderItem.TotalPrice;
            orderItem.Amount = amount;
            orderItem.TotalPrice = amount * orderItem.Price;
            cart.TotalPrice += orderItem.TotalPrice - oldTotalPrice;
        }
        ///return the update cart 
        return cart;

        //if (cart.CustomerName == null || cart.CostumerAdress == null || cart.CustomerEmail == null || cart.CustomerEmail == null || !cart.CustomerEmail.Contains('@') || cart.CustomerEmail.Contains(' ') || cart.CustomerEmail.IndexOf('@') == 0 || cart.CustomerEmail.IndexOf('@') == cart.CustomerEmail.Length - 1)
        //    throw new BO.InCorrectData();
        //if (amount < 0)
        //    throw new BO.InCorrectData();
        //DO.Product? product;
        //try
        //{
        //    product = dal?.Product.GetObject(id);
        //}
        //catch (DO.NotExist e) { throw new BO.NotExist(e); }
        //foreach (BO.OrderItem? orderItem in cart?.Items??throw new BO.NullData())
        //{

        //    if (orderItem?.ID == id)
        //    {
        //        if (amount == orderItem.Amount)
        //            return cart;
        //        if (amount == 0)
        //        {
        //            cart.TotalPrice -= orderItem.TotalPrice;
        //            cart.Items.Remove(orderItem);
        //            return cart;
        //        }
        //        if (orderItem.Amount < amount)
        //        {
        //            cart.TotalPrice += (amount - orderItem.Amount) * orderItem.Price;
        //            orderItem.Amount = amount;
        //            orderItem.TotalPrice = amount * orderItem.Price;
        //            return cart;
        //        }
        //        if (orderItem.Amount > amount)
        //        {
        //            cart.TotalPrice -= (orderItem.Amount - amount) * orderItem.Price;
        //            orderItem.Amount = amount;
        //            orderItem.TotalPrice = amount * orderItem.Price;
        //            return cart;
        //        }
        //    }
        //}
        //throw new BO.NotPossibleToFillRequest();
    }
    /// <summary>
    /// Method for placing the order, receives a shopping basket and adds the order if everything is in order
    /// </summary>
    /// <param name="cart"></param>
    public void OrderConfirmation(BO.Cart cart)
    {
        ///check the details 
        if (cart.CustomerName == null)
            throw new BO.NullData();
        if (!new EmailAddressAttribute().IsValid(cart.CustomerEmail))
            throw new BO.InCorrectData();
        if (cart.CostumerAdress == null)
            throw new BO.NullData();
        if (cart.Items!.Count() == 0)//no items in cart
            throw new BO.NullData();
        DO.Order order = new DO.Order()
        {
            CustomerName = cart.CustomerName,
            CustomerEmail = cart.CustomerEmail,
            CustomerAddress = cart.CostumerAdress,
            OrderDate = DateTime.Now,
            DeliveryDate = null,
            ShipDate = null,
        };

        try
        {
            int orderID = dal!.Order.AddObject(order);

            IEnumerable<int> orderItemsID = from item in cart.Items
                                            select dal.OrderItem.AddObject(
                                                new DO.OrderItem
                                                {
                                                    OrderID = orderID,
                                                    Price = item.Price,
                                                    ProductID = item.ProductID,
                                                    Amount = item.Amount,
                                                    ID = item.ID
                                                });

            IEnumerable<DO.Product> productUpdate = from item in cart.Items
                                                    select new DO.Product
                                                    {
                                                        ID = item.ProductID,
                                                        Name = item.Name,
                                                        Price = item.Price,
                                                        Category = dal.Product.GetObject(item.ProductID)?.Category,
                                                        InStock = (int)(dal.Product.GetObject(item.ProductID))?.InStock - item.Amount
                                                    };


            productUpdate.ToList().ForEach(x => dal.Product.UpDateObject(x));
        }
        catch (DO.NotExist ex)
        {
            throw new BO.NotExist(ex);
        }
    }
}
//    if (cart.CustomerName == null || cart.CostumerAdress == null || cart.CustomerEmail == null || cart.CustomerEmail==null || !cart.CustomerEmail.Contains('@') || cart.CustomerEmail.Contains(' ') || cart.CustomerEmail.IndexOf('@') == 0 || cart.CustomerEmail.IndexOf('@') == cart.CustomerEmail.Length - 1)
//        throw new BO.InCorrectData();
//    DO.Product? product;
//    foreach (BO.OrderItem? orderItem in cart?.Items??throw new BO.NullData())
//    {
//        try { product = dal?.Product.GetObject(orderItem?.ProductID ?? throw new BO.NullData()); }
//        catch(DO.NotExist e) { throw new BO.NotExist(e); }
//        if(orderItem?.Amount<=0)
//            throw new BO.InCorrectData();
//        if (product?.InStock- orderItem?.Amount<0)
//            throw new BO.NotPossibleToFillRequest();
//    }
//    DO.Order newOrder=new DO.Order { CustomerAddress=cart.CostumerAdress,CustomerEmail=cart.CustomerEmail,CustomerName=cart.CustomerName,OrderDate=DateTime.Now};
//    int id;
//    try { id=dal?.Order.AddObject(newOrder)??throw new BO.NullData(); }
//    catch(DO.AllReadyExist e) { throw new BO.AllReadyExist(e); }
//    foreach (BO.OrderItem? orderItem in cart.Items)
//    {
//        DO.OrderItem newOrderItem = new DO.OrderItem { ProductID = orderItem?.ProductID??throw new BO.NullData(), OrderID = id, Price = orderItem?.Price??throw new BO.NullData(), Amount = orderItem?.Amount??throw new BO.NullData() };
//        try { id = dal.OrderItem.AddObject(newOrderItem); }
//        catch (DO.NotExist e) { throw new BO.NotExist(e); }
//       DO.Product product1=(DO.Product)dal.Product.GetObject(orderItem.ProductID)!;
//        product1.InStock -= orderItem.Amount;
//        dal.Product.UpDateObject(product1);

//    }


//}

