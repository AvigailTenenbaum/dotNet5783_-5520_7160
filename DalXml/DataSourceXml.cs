using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dal.DataSourceXml;
using DO;
using Dal;
using System.Xml.Linq;
using System.Data;
using System.Collections;
using System.Net.Sockets;

namespace Dal;


internal static class DataSourceXml
{
    static readonly Random num = new Random();
    private static XElement initialize;
    internal static string productPath = @"Product.xml";
    internal static string orderPath = @"Order.xml";
    internal static string orderItemPath = @"OrderItem.xml";
    internal static List<DO.Product?> products = new List<DO.Product?>();
    internal static List<DO.Order?> orders = new List<DO.Order?>();
    internal static List<DO.OrderItem?> items = new List<DO.OrderItem?>();

    private static int _lastProductID = 100000;
    private static int _lastOrderID = 1;
    private static int _lastOrderItemsID = 1;
    internal static int getLastProductID()
    {
        return _lastProductID++;
    }
    internal static int getLastOrderID()
    {
        return _lastOrderID++;
    }
    internal static int getLastOrderItemsID()
    {
        return _lastOrderItemsID++;
    }
    static DataSourceXml()
    {
        s_Initialize();
    }
    static private void s_Initialize()
    {
        initProducts();
        initOrders();
        initOrdersItems();
       
    }

    private static void initOrdersItems()
    {
        for (int i = 0; i < 40; i++)
        {
            DO.Product? product = new DO.Product();
            DO.OrderItem orderItem = new DO.OrderItem();
            product = products[num.Next(0, products.Count)];
            orderItem.ID = getLastOrderID();
            orderItem.ProductID = product?.ID ?? 0;
            orderItem.Amount = num.Next(1, 11);
            orderItem.OrderID = orders[num.Next(0, orders.Count)]?.ID ?? 0;
            orderItem.Price = product?.Price ?? 0;
            items.Add(orderItem);
        }
    }

    private static void initOrders()
    {
        string[] firstNames = new string[10] { "Noa", "Avigail", "Chaya", "Rinat", "Michal", "Efrat", "Daniel", "Yair", "Netanel", "David" };
        string[] lastNames = new string[10] { "Landman", "Tenenbaum", "Levi", "Cohen", "Rhein", "Poper", "Miller", "Kaminer", "Plutzki", "Atzmon" };
        string[] cities = new string[10] { "Jerusalem", "Bnei-Brak", "Petach-Tiqwa", "Rechovot", "Tzfat", "Haifa", "Lod", "Netanya", "Modiin", "Herzelia" };
        string[] streets = new string[10] { "Admor Meruzin", "Sechtman", "Booblik", "Hagefen", "Yafo", "Brand", "Chai-Taib", "Gordon", "Gutmacher", "Rojovski" };
        for (int i = 0; i < 20; i++)
        {
            DO.Order order = new DO.Order();
            order.ID = getLastOrderID();
            order.CustomerName = firstNames[num.Next(0, 10)] + " " + lastNames[num.Next(0, 10)];
            order.CustomerEmail = order.CustomerName.Replace(" ", String.Empty) + "@gmail.com";
            order.CustomerAddress = cities[num.Next(0, 10)] + " " + streets[num.Next(0, 10)] + " " + num.Next(1, 150);
            order.OrderDate = DateTime.Now.AddMinutes(num.Next(-100, -10));
            if (num.Next(0, 100) > 20)
            {
                order.ShipDate = order.OrderDate.Value.AddMinutes(num.Next(10, 100));
                if (num.Next(0, 100) > 40)
                    order.DeliveryDate = order.ShipDate.Value.AddDays(num.Next(1, 4));
                else
                    order.DeliveryDate = null;
            }

            else
            {
                order.ShipDate = null;
                order.DeliveryDate = null;
            }
            orders.Add(order);
        }
    }
    static void initProducts()
    {
        List<List<string>> productsNames = new List<List<string>>
        {
           new List<string>{"candlesticks", "goblet", "challahTray"," HavdalahSet"," lastWater", "candlestickTray"," knife"},
            new List<string>{" honeycomb"," etrogBox"," menorah"," sederPlate"," scrollBag","eliyahuCup"},
            new List<string>{"crown"," breastplate","finger"," pomegranates"},
            new List<string>{"mezuzah"," fruitBowl", "charityBox"," waza", "lampStand"},
            new List<string>{  "goblet", "chanukah"," havdalahSet"," candlesticks"}
        };
        int size = productsNames.Count();
        for (int i = 0; i < size; i++)
        {
            Category category = (Category)i;
            int per = (int)(productsNames.Count() * 0.05) + 1;
            foreach (var item in productsNames[i])
            {
                DO.Product product = new DO.Product();
                product.ID = getLastProductID();
                product.Category = category;
                product.Name = item;
                product.Price = product.Category switch
                {
                    Category.saturday => num.Next(2000, 3000),
                    Category.holidays => num.Next(3000, 5000),
                    Category.toSeferTorah => num.Next(1000, 1700),
                    Category.handMade => num.Next(5000, 7000),
                    Category.giftsForHome => num.Next(100, 1000),
                };

                product.InStock = per-- > 0 ? 0 : num.Next(50, 100);
                products.Add(product);

            }
        }
    }

    private static void SaveProductListLinq(List<DO.Product?> listPruducts)
    {
        var v = from p in listPruducts
                select new XElement("Product",
                    new XElement("Id", p?.ID),
                    new XElement("Name", p?.Name),
                    new XElement("InStock", p?.InStock),
                    new XElement("Price", p?.Price)
                    );

        initialize = new XElement("Product", v);
        initialize.Save(productPath);
    }

    private static void SaveOrdertListLinq(List<DO.Order?> listOrders)
    {
        var v = from p in listOrders
                select new XElement("Order",
                    new XElement("id", p?.ID),
                    new XElement("Name", p?.CustomerName),
                    new XElement("Email", p?.CustomerEmail),
                    new XElement("Adress", p?.CustomerAddress),
                    new XElement("OrderDate", p?.OrderDate),
                    new XElement("ShipDate", p?.ShipDate),
                    new XElement("DeliveryrDate", p?.DeliveryDate)
                    );

        initialize = new XElement("Order", v);
        initialize.Save(orderPath);
    }

    private static void SaveOrderItemtListLinq(List<DO.OrderItem?> listOrderItems)
    {
        var v = from p in listOrderItems
                select new XElement("OrderItem",
                    new XElement("id", p?.ID),
                    new XElement("name", p?.ProductID),
                    new XElement("firstName", p?.OrderID),
                    new XElement("lastName", p?.Price),
                    new XElement("name", p?.Amount)
                    );

        initialize = new XElement("OrderItem", v);
        initialize.Save(orderItemPath);
    }
}
