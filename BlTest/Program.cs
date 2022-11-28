using BO;
using BlApi;
using BlImplementation;
namespace BlTest
{
    public enum Options { EXIT, PRODUCT, ORDER, CART };
    public enum ActionsOnProducts { EXIT, PRODUCTLIST, GETPRODUCTDETAILS, GETPRODUCTDETAILSFORMANGER, ADDPRODUCT, DELETEPRODUCT, UPDATEPRODUCT };
    public enum ActionOnCart { EXIT, ADDPRODUCTTOCART, UPDATEAMOUNT, CONFIRMORDER }
    public enum ActionOnOrder { EXIT, ORDERDATAILS , ORDERLIST, ORDERSHIPINGUPDATE, ORDERDALIVERYUPDATE, ORDERTRACKING };

    internal class Program
    {
        static IBl s_bl = new Bl();

        static Cart newCart = new Cart() {Items=new List<OrderItem>()};
        static Product addProduct = new Product();

        static void ActionOnProduct()
        {
            ActionsOnProducts choice;
            Console.WriteLine(@"Choose one of the following options:
1: list of products
2:details of product for manager
3:details of product for costumer
4:add product 
5:delete product
6:update product
0:Exit");
            if (!ActionsOnProducts.TryParse(Console.ReadLine(), out choice)) throw new Exception("This option not exist!");
            while (choice != ActionsOnProducts.EXIT)
            {
                try
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
                            double price;
                            Category category;
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
                            if (!Category.TryParse(Console.ReadLine(), out category)) throw new Exception("wrong input type");
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

                            if (!int.TryParse(Console.ReadLine(), out id))
                                throw new Exception("wrong input type");

                            updateProduct.ID = id;
                            Console.WriteLine("enter name of product:");
                            updateProduct.Name = Console.ReadLine();
                            Console.WriteLine("enter price of product:");

                            if (!double.TryParse(Console.ReadLine(), out price)) 
                                throw new Exception("wrong input type");

                            updateProduct.Price = price;
                            Console.WriteLine("enter category of product:");

                            if (!Category.TryParse(Console.ReadLine(), out category)) throw new Exception("wrong input type");
                            updateProduct.Category = (Category)category;
                            Console.WriteLine("enter amount in stock of product:");
                            if (!int.TryParse(Console.ReadLine(), out stock)) throw new Exception("wrong input type");
                            updateProduct.InStock = stock;
                            s_bl.Product.UpdateProduct(updateProduct);
                            break;

                        case ActionsOnProducts.GETPRODUCTDETAILS:
                            Console.WriteLine("enter id of product:");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type");
                            Console.WriteLine(s_bl.Product.GetProductDetails(id, newCart));
                            break;
                        case ActionsOnProducts.EXIT:
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine(@"Choose one of the following options:
1: list of products
2:details of product for manager
3:details of product for costumer
4:add product 
5:delete product
6:update product
0:Exit");
                    if (!ActionsOnProducts.TryParse(Console.ReadLine(), out choice)) throw new Exception("This option not exist!");
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }



        }

        static void ActionsOnOrder()
        {
            int id;
            ActionOnOrder choice = ActionOnOrder.EXIT;

            do
            {
                Console.WriteLine(@"Choose one of the following options:
1: order details
2:list of orders
3:update ship date 
4:update delivery date
5:order tracking
0:for exit");
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
                            Console.WriteLine(s_bl.Order.OrderShippingUpdate(id));
                            break;
                        case ActionOnOrder.ORDERTRACKING:
                            Console.WriteLine("please insert order Id");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                            Console.WriteLine(s_bl.Order.OrderTracking(id));
                            break;
                        case ActionOnOrder.EXIT:
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (choice != ActionOnOrder.EXIT);

        }
        static void ActionsOnCart()
        {
            ActionOnCart choice;
            Console.WriteLine(@"Choose one of the following options:
1:add product to cart
2:update amount of product in cart
3:create a new order:");
            if (!ActionOnCart.TryParse(Console.ReadLine(), out choice)) throw new Exception("wrong input type");
            while (choice != ActionOnCart.EXIT)
            {
                try
                {
                    switch (choice)
                    {
                        case ActionOnCart.ADDPRODUCTTOCART:
                            int id, amount;
                            Console.WriteLine("please insert name:");
                            newCart.CustomerName = Console.ReadLine();
                            Console.WriteLine("please insert address:");
                            newCart.CostumerAdress = Console.ReadLine();
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
                            newCart.CostumerAdress = Console.ReadLine();
                            Console.WriteLine("please insert email address:");
                            newCart.CustomerEmail = Console.ReadLine();
                            Console.WriteLine("enter id of product to add to cart:");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                            Console.WriteLine("enter new amount of product:");
                            if (!int.TryParse(Console.ReadLine(), out amount)) throw new Exception("wrong input type ");
                            Console.WriteLine(s_bl.Cart.UpdateProductAmount(newCart, amount, id));
                            break;
                        case ActionOnCart.CONFIRMORDER:
                            Console.WriteLine("please insert name:");
                            newCart.CustomerName = Console.ReadLine();
                            Console.WriteLine("please insert address:");
                            newCart.CostumerAdress = Console.ReadLine();
                            Console.WriteLine("please insert email address:");
                            newCart.CustomerEmail = Console.ReadLine();
                            s_bl.Cart.OrderConfirmation(newCart);
                            break;
                        case ActionOnCart.EXIT:
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine(@"Choose one of the following options:
1:enter id of product to add to cart:
2:enter amount of products to add to cart:
3:create a new order:");
                    if (!ActionOnCart.TryParse(Console.ReadLine(), out choice)) throw new Exception("wrong input type");
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }
        }


        static void Main(string[] args)
        {
            try
            {
                
                Console.WriteLine(@"Choose one of the following options:
1: Products
2:Orders
3:Carts
0:Exit");
                Options choice;
                if (!Options.TryParse(Console.ReadLine(), out choice)) throw new Exception("This option not exist!");
                while (choice != Options.EXIT)
                {
                    switch (choice)
                    {
                        case Options.PRODUCT:
                            ActionOnProduct();
                            break;
                        case Options.ORDER:
                            ActionsOnOrder();
                            break;
                        case Options.CART:
                           ActionsOnCart();
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine(@"Choose one of the following options:
1: Products
2:Orders
3:Carts
0:Exit");
                    if (!Options.TryParse(Console.ReadLine(), out choice)) throw new Exception("This option not exist!");
                }
            }
            catch(Exception e) { Console.WriteLine(e.Message); }
        }
    }
}
