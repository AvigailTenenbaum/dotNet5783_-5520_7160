using BO;
using BlApi;
using BlImplementation;
namespace BlTest
{
    public enum Options {EXIT,PRODUCT,ORDER,CART };
    public enum ActionsOnProducts {EXIT,PRODUCTLIST,GETPRODUCTDETAILS, GETPRODUCTDETAILSFORMANGER,ADDPRODUCT, DELETEPRODUCT,UPDATEPRODUCT};
    public enum ActionOnCart {EXIT, ADDPRODUCTTOCART,UPDATEAMOUNT,CONFIRMORDER}
    public enum ActionOnOrder { EXIT,ORDERLIST,ORDERDATAILS,ORDERSHIPINGUPDATE,ORDERDALIVERYUPDATE,ORDERTRACKING};
    internal class Program
    {
        static IBl s_bl = new Bl();
        public static void ActionOnProductForCostumer()
        {

            ActionsOnProducts choice;
            Console.WriteLine(@"Choose one of the following options:
1: list of products
2:details of product
3:add product
4:delete product
5:update product
6:show catalog
7:Exit");
            if (!ActionsOnProducts.TryParse(Console.ReadLine(), out choice)) throw new Exception("This option not exist!");
            while (choice != ActionsOnProducts.EXIT)
            {
                switch (choice)
                {
                    case ActionsOnProducts.PRODUCTLIST:
                        var lst = s_bl.Product.GetListOfProducts();
                        foreach (var item in lst)
                            Console.WriteLine(item);
                        break;
                    case ActionsOnProducts.GETPRODUCTDETAILSFORMANGER:
                        int id;
                        Console.WriteLine("enter id of product:");
                        if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type");
                        Console.WriteLine(s_bl.Product.GetProductDetails(id));
                        break;
                    case ActionsOnProducts.ADDPRODUCT:
                        Product addProduct = new Product();
                        double price;
                        int category;
                        int stock;

                        Console.WriteLine("enter id of product:");
                        if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type");
                        addProduct.ID = id;
                        Console.WriteLine("enter name of product:");
                        addProduct.Name = Console.ReadLine();
                        Console.WriteLine("enter price of product:");
                        if (!double.TryParse(Console.ReadLine(), out price)) throw new Exception("wrong input type");
                        addProduct.Price = price;
                        Console.WriteLine("enter category of product:");
                        if (!int.TryParse(Console.ReadLine(), out category)) throw new Exception("wrong input type");
                        addProduct.Category = (Category)(category);
                        Console.WriteLine("enter amount in stock of product:");
                        if (!int.TryParse(Console.ReadLine(), out stock)) throw new Exception("wrong input type");
                        addProduct.InStock = stock;
                        s_bl.Product.AddProduct(addProduct);
                        break;
                    case ActionsOnProducts.DELETEPRODUCT:
                        Console.WriteLine("enter id to delete product:");
                        if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type");
                        s_bl.Product.DeleteProduct(id);
                        break;
                    case ActionsOnProducts.UPDATEPRODUCT:
                        Product updateProduct = new Product();
                        Console.WriteLine("enter id of product:");
                        if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type");
                        updateProduct.ID = id;
                        Console.WriteLine("enter name of product:");
                        updateProduct.Name = Console.ReadLine();
                        Console.WriteLine("enter price of product:");
                        if (!double.TryParse(Console.ReadLine(), out price)) throw new Exception("wrong input type");
                        updateProduct.Price = price;
                        Console.WriteLine("enter category of product:");
                        if (!int.TryParse(Console.ReadLine(), out category)) throw new Exception("wrong input type");
                        updateProduct.Category = (Category)category;
                        Console.WriteLine("enter amount in stock of product:");
                        if (!int.TryParse(Console.ReadLine(), out stock)) throw new Exception("wrong input type");
                        updateProduct.InStock = stock;
                        s_bl.Product.UpdateProduct(updateProduct);
                        break;
                    case ActionsOnProducts.PRODUCTLIST:
                        foreach (var item in s_bl.Product.GetListOfProducts())
                            Console.WriteLine(item);
                        break;
                    case ActionsOnProducts.GETPRODUCTDETAILS:
                        Console.WriteLine("enter id of product:");
                        if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type");
                        Console.WriteLine(s_bl.Product.GetListOfProducts(newCart, id));
                        break;
                    case ActionsOnProducts.EXIT:
                        break;
                    default:
                        break;
                }
                Console.WriteLine("");
                Console.WriteLine(@"Choose one of the following options:
1: list of products
2:details of product
3:add product
4:delete product
5:update product
6:show catalog
7:delails of product item");
                if (!ActionsOnProducts.TryParse(Console.ReadLine(), out choice)) throw new Exception("This option not exist!");
            }


        }
        public static void ActionsOnOrder()
        {
            int id;
            Console.WriteLine(@"Choose one of the following options:
1: oder details
2:list of orders
3:update ship date 
4:update delivery date
5:order tracking
6:update order");
            ActionOnOrder choice = ActionOnOrder.EXIT;

            do
            {
                try
                {
                    if (!ActionOnOrder.TryParse(Console.ReadLine(), out choice)) throw new Exception("wrong input type");
                    switch (choice)
                    {
                        case ActionOnOrder.ORDERDATAILS:
                            Console.WriteLine("please insert order Id");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                            Console.WriteLine(s_bl.Order.GetOrderDetails(id));
                            break;
                        case ActionOnOrder.ORDERLIST:
                            Console.WriteLine(String.Join(" ", s_bl.Order.GetListOfOrder()));
                            break;
                        case ActionOnOrder.ORDERDALIVERYUPDATE:
                            Console.WriteLine("please insert order Id");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                            Console.WriteLine(s_bl.Order.OrderDeliveryUpdate(id));
                            break;
                        case ActionOnOrder.ORDERSHIPINGUPDATE:
                            Console.WriteLine("please insert order Id");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                            Console.WriteLine(bl.Order.UpdateDelivery(id));
                            break;
                        case ActionOnOrder.Order_Tracking:
                            Console.WriteLine("please insert order Id");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                            Console.WriteLine(bl.Order.OrderTracking(id));
                            break;
                        case OrderActions.Update_Order:

                            break;
                        case OrderActions.Exit:
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (choi != OrderActions.Exit);

        }
        public static void ActionsOnCart()
        {
            ActionOnCart choice;
            Console.WriteLine(@"Choose one of the following options:
1:add product to cart
2:update amount of product in cart
3:create a new order:");
            if (!ActionOnCart.TryParse(Console.ReadLine(), out choice)) throw new Exception("wrong input type");
            while (choice != ActionOnCart.EXIT)
            {
                switch (choice)
                {
                    case ActionOnCart.ADDPRODUCTTOCART:
                        int id, amount;
                        Console.WriteLine("please insert name:");
                        newCart.CustomerName = Console.ReadLine();
                        Console.WriteLine("please insert address:");
                        newCart.CustomerAddress = Console.ReadLine();
                        Console.WriteLine("please insert email address:");
                        newCart.CustomerEmail = Console.ReadLine();
                        Console.WriteLine("enter id of product to add to cart:");
                        if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                        Console.WriteLine(s_bl.Cart.AddProductToCart(newCart, id));
                        break;
                    case ActionOnCart.UPDATEAMOUNT:
                        Console.WriteLine("please insert name:");
                        newCart.CustomerName = Console.ReadLine();
                        Console.WriteLine("please insert address:");
                        newCart.CustomerAddress = Console.ReadLine();
                        Console.WriteLine("please insert email address:");
                        newCart.CustomerEmail = Console.ReadLine();
                        Console.WriteLine("enter id of product to add to cart:");
                        if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                        Console.WriteLine("enter new amount of product:");
                        if (!int.TryParse(Console.ReadLine(), out amount)) throw new Exception("wrong input type ");
                        Console.WriteLine(bl.Cart.UpdateProductInCart(newCart, amount, id));
                        break;
                    case ActionOnCart.CONFIRMORDER:
                        Console.WriteLine("please insert name:");
                        newCart.CustomerName = Console.ReadLine();
                        Console.WriteLine("please insert address:");
                        newCart.CustomerAddress = Console.ReadLine();
                        Console.WriteLine("please insert email address:");
                        newCart.CustomerEmail = Console.ReadLine();
                        s_bl.Cart.OrderConfirmation(newCart, newCart.CustomerName, newCart.CustomerEmail, newCart.CustomerAddress);
                        break;
                    case CartActions.Exit:
                        break;
                    default:
                        break;
                }
                Console.WriteLine(@"Choose one of the following options:
1:enter id of product to add to cart:
2:enter amount of products to add to cart:
3:create a new order:");
                if (!CartActions.TryParse(Console.ReadLine(), out choice)) throw new Exception("wrong input type");
            }
       


            static void Main(string[] args)
            {
              
            }
        }
}
