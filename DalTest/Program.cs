﻿//Noa Landman 213877160
//Avigail Tenenbaum 213865520
//we did the bonus!!!!!!!!!
using Dal;
using DO;
using System.Security.Cryptography.X509Certificates;

namespace DalTest
{
    internal class Program
    {
        private static DalOrder order=new DalOrder();
        private static DalOrderItem orderItem = new DalOrderItem();
        private static DalProduct product = new DalProduct();
        /// <summary>
        /// A function for operations on a product
        /// </summary>
        void productFunction()
        {
            DalProduct product=new DalProduct();
            int id;
            char tav;
            string name;
            double price;
            int unitsInStock;  
            do
            {
                Console.WriteLine("Select one of the following, press f to exit\r\na: add an object to an entity's list\r\nb: Object display by ID\r\nc: Entity list view \r\nd: update object data\r\ne: delete an object from an entity's list");
                char.TryParse(Console.ReadLine(), out tav);
                switch(tav)
                {
                    case 'a':
                        {
                            
                            Console.WriteLine("Enter the product id");
                            int.TryParse(Console.ReadLine(), out id);
                            Console.WriteLine("Enter the product name");
                            name = Console.ReadLine();
                            Console.WriteLine("Enter the product category");
                            Enums.Category category;
                            Enums.Category.TryParse(Console.ReadLine(), out category);
                            Console.WriteLine("Enter the product price");
                            double.TryParse(Console.ReadLine(), out price);
                            Console.WriteLine("Enter how many units are in stock");
                            int.TryParse(Console.ReadLine(), out unitsInStock);
                           
                            Product p1 = new Product();
                            p1.ID = id;
                            p1.Name = name;
                            p1.Price = price;
                            p1.Category= category;
                            p1.InStock = unitsInStock; product.AddObject(p1); break; }
                    case 'b':
                        {
                            Console.WriteLine("Enter the product id");
                            int.TryParse(Console.ReadLine(), out id); Product p = product.GetObject(id); Console.WriteLine(p); break;
                        }
                    case 'c': Product[] pArr = product.GetAllObject(); for (int i = 0; i < pArr.Length; i++) Console.WriteLine(pArr[i]); break;
                    case 'd':
                        {
                            Console.WriteLine("Enter the product id");
                            int.TryParse(Console.ReadLine(), out id);
                            Console.WriteLine("Enter the product name");
                            name = Console.ReadLine();
                            Console.WriteLine("Enter the product category");
                            Enums.Category category;
                            Enums.Category.TryParse(Console.ReadLine(), out category);
                            Console.WriteLine("Enter the product price");
                            double.TryParse(Console.ReadLine(), out price);
                            Console.WriteLine("Enter how many units are in stock");
                            int.TryParse(Console.ReadLine(), out unitsInStock);
                            Product p1 = new Product();
                            p1.ID = id;
                            p1.Name = name;
                            p1.Price = price;
                            p1.Category= category;
                            p1.InStock = unitsInStock; product.UpDateObject(p1); break;
                        }
                    case 'e':
                        {
                            Console.WriteLine("Enter the product id");
                            int.TryParse(Console.ReadLine(), out id); product.DeleteObject(id); break;
                        }
                    default: Console.WriteLine("finish"); break;
                }
            } while (tav!='f');
        }
        /// <summary>
        /// A function for operations on an order
        /// </summary>
        void orderFunction()
        {
            DalOrder order = new DalOrder();
            int id;
            char tav;
            string name;
            string email;
            string address;
            DateTime orderDate;
           
            do
            {
                Console.WriteLine("Select one of the following, press f to exit\r\na: add an object to an entity's list\r\nb: Object display by ID\r\nc: Entity list view \r\nd: update object data\r\ne: delete an object from an entity's list");
                char.TryParse(Console.ReadLine(), out tav);
                switch (tav)
                {
                    case 'a':
                        {
                            Console.WriteLine("Enter the customer name");
                            name = Console.ReadLine();
                            Console.WriteLine("Enter the customer email");
                            email = Console.ReadLine();
                            Console.WriteLine("Enter the customer address");
                            address = Console.ReadLine();
                            Console.WriteLine("Enter the ship date");
                            DateTime.TryParse(Console.ReadLine(), out orderDate);  
                            Order o1 = new Order();
                            o1.ShipDate=orderDate;
                            Console.WriteLine("Enter the order date");
                            DateTime.TryParse(Console.ReadLine(), out orderDate);
                            o1.OrderDate=orderDate;
                            Console.WriteLine("Enter the delivery date");
                            DateTime.TryParse(Console.ReadLine(), out orderDate);
                            o1.DeliveryDate=orderDate;

                            o1.CustomerName = name;
                            o1.CustomerEmail = email;
                            o1.CustomerAddress = address; order.AddObject(o1); break;
                        }
                    case 'b':
                        {
                            Console.WriteLine("Enter the order id");
                            int.TryParse(Console.ReadLine(), out id); Order o = order.GetObject(id); Console.WriteLine(o); break;
                        }
                    case 'c': Order[] oArr = order.GetAllObject(); for (int i = 0; i < oArr.Length; i++) Console.WriteLine(oArr[i]); break;
                    case 'd':
                        {
                            Console.WriteLine("Enter the order id");
                            int.TryParse(Console.ReadLine(), out id);
                            Console.WriteLine("Enter the customer name");
                            name = Console.ReadLine();
                            Console.WriteLine("Enter the customer email");
                            email = Console.ReadLine();
                            Console.WriteLine("Enter the customer address");
                            address = Console.ReadLine();
                            Order o1 = new Order();
                            o1.ID = id;
                            o1.CustomerName = name;
                            o1.CustomerEmail = email;
                            o1.CustomerAddress = address; order.UpDateObject(o1); break;
                        }
                    case 'e':
                        {
                            Console.WriteLine("Enter the order id");
                            int.TryParse(Console.ReadLine(), out id); order.DeleteObject(id); break;
                        }
                    default: Console.WriteLine("finish"); break;
                }
            } while (tav != 'f');
        }
        /// <summary>
        /// Function for actions on items in the order
        /// </summary>
        void orderItemFunction()
        {
            DalOrderItem orderI = new DalOrderItem();
            int id;
            char tav;
            int orderId;
            int productId;
            double price;
            int amount;           
            do
            {
                Console.WriteLine("Select one of the following, press f to exit\r\na: add an object to an entity's list\r\nb: Object display by ID\r\nc: Entity list view \r\nd: update object data\r\ne: delete an object from an entity's list");
                char.TryParse(Console.ReadLine(), out tav);
                switch (tav)
                {
                    case 'a':
                        {
                            Console.WriteLine("Enter the order id");
                            int.TryParse(Console.ReadLine(), out orderId);
                            Console.WriteLine("Enter the item ID");
                            int.TryParse(Console.ReadLine(), out productId);
                            Console.WriteLine("Enter the price");
                            double.TryParse(Console.ReadLine(), out price);
                            Console.WriteLine("Enter the amount");
                            int.TryParse(Console.ReadLine(), out amount);
                            OrderItem oi1 = new OrderItem();
                            oi1.OrderID = orderId;
                            oi1.ProductID = productId;
                            oi1.Price = price;
                            oi1.Amount = amount; orderItem.AddObject(oi1); break;
                        }
                    case 'b':
                        {
                            Console.WriteLine("Enter the order Item id");
                            int.TryParse(Console.ReadLine(), out id); OrderItem o = orderItem.GetObject(id); Console.WriteLine(o); break;
                        }
                    case 'c': OrderItem[] oArr = orderItem.GetAllObject(); for (int i = 0; i < oArr.Length; i++) Console.WriteLine(oArr[i]); break;
                    case 'd':
                        {
                            Console.WriteLine("Enter the order Item id");
                            int.TryParse(Console.ReadLine(), out id);
                            Console.WriteLine("Enter the order id");
                            int.TryParse(Console.ReadLine(), out orderId);
                            Console.WriteLine("Enter the item ID");
                            int.TryParse(Console.ReadLine(), out productId);
                            Console.WriteLine("Enter the price");
                            double.TryParse(Console.ReadLine(), out price);
                            Console.WriteLine("Enter the amount");
                            int.TryParse(Console.ReadLine(), out amount);
                            OrderItem oi1 = new OrderItem();
                            oi1.ID = id;
                            oi1.OrderID = orderId;
                            oi1.ProductID = productId;
                            oi1.Price = price;
                            oi1.Amount = amount; orderItem.UpDateObject(oi1); break;
                        }
                    case 'e':
                        {
                            Console.WriteLine("Enter the order Item id");
                            int.TryParse(Console.ReadLine(), out id); orderItem.DeleteObject(id); break;
                        }
                    default: Console.WriteLine("finish"); break;
                }
            } while (tav != 'f');
        }

        static void Main(string[]args)
        {
            try
            {
                Program program = new Program();
                Console.WriteLine("choose one of the following:\n1: for product\n2:for Order\n3:for orderItem\n0: exist");
                char ch;
                do
                {
                    char.TryParse(Console.ReadLine(), out ch);
                    switch (ch)
                    {
                        case '1': program.productFunction(); break;
                        case '2': program.orderFunction(); break;
                        case '3': program.orderItemFunction(); break;
                        default: Console.WriteLine("finish"); break;
                    }

                } while (ch!=0);
            }
            catch(Exception e)
            { Console.WriteLine(e.Message); }
        }
    }
}





