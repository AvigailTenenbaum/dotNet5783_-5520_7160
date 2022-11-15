﻿
using DO;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Dal;

 static internal class DataSource
{
    static readonly Random num = new Random();
    internal static Product[] products = new Product[50];
    internal static Order[] orders = new Order[100];
    internal static OrderItem[] items = new OrderItem[200];

    private static int _lastProductID = 100000;
    private static int _lastOrderID = 1;
    private static int _lastOrderItemsID = 1;
    internal static int _indexOfProduct = 0;
    internal static int _indexOfOrder = 0;
    internal static int _indexOfOrderItem = 0;
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
    static DataSource()
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
            Product product = new Product();
            OrderItem orderItem = new OrderItem();
            product = products[num.Next(0, _indexOfProduct)];
            orderItem.ID =getLastOrderID();
            orderItem.ProductID = product.ID;
            orderItem.Amount = num.Next(1, 11);
            orderItem.OrderID = orders[num.Next(0,_indexOfOrderItem)].ID;
            orderItem.Price =  product.Price;
            items[_indexOfOrderItem++] = orderItem;
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
            Order order = new Order();
            order.ID = getLastOrderID();
            order.CustomerName = firstNames[num.Next(0, 10)] + " " + lastNames[num.Next(0, 10)];
            order.CustomerEmail = order.CustomerName.Replace(" ", String.Empty) + "@gmail.com";
            order.CustomerAddress = cities[num.Next(0, 10)] + " " + streets[num.Next(0, 10)] + " " + num.Next(1, 150);
            order.OrderDate = DateTime.Now.AddMinutes(num.Next(-100, -10));
            if (num.Next(0, 100) > 20)
            {
                order.ShipDate = order.OrderDate.AddMinutes(num.Next(10, 100));
                if (num.Next(0, 100) > 40)
                    order.DeliveryDate = order.ShipDate.AddDays(num.Next(1, 4));
                else
                    order.DeliveryDate = DateTime.MinValue;
            }

            else
            {
                order.ShipDate = DateTime.MinValue;
                order.DeliveryDate = DateTime.MinValue;
            }
            orders[_indexOfOrder++] = order;
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
            Enums.Category category = (Enums.Category)i;
            int per = (int)(productsNames.Count() * 0.05)+1;
            foreach (var item in productsNames[i])
            {
                Product product = new Product();
                product.ID = getLastProductID();
                product.Category = category;
                product.Name = item;
                product.Price = product.Category switch
                {
                    Enums.Category.saturday => num.Next(2000,3000),
                    Enums.Category.holidays => num.Next(3000, 5000),
                    Enums.Category.toSeferTorah => num.Next(1000, 1700),
                    Enums.Category.handMade => num.Next(5000, 7000),
                    Enums.Category.giftsForHome => num.Next(100, 1000),
                };

                product.InStock = per-- > 0 ? 0 : num.Next(50, 100);
                    products[_indexOfProduct++] = product; 
              
            }
        }
    }
}



