using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BO;

    public class OrderForList
    {
    /// <summary>
    /// A unique identifier for the order list
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// the customer's name
    /// </summary>
    public string CustomerName { get; set; }
    /// <summary>
    /// order status
    /// </summary>
    public OrderStatus Status { get; set; }
    /// <summary>
    /// amount of items
    /// </summary>
    public int AmountOfItems { get; set; }
    /// <summary>
    /// total price
    /// </summary>
    public double TotalPrice { get; set; }
    public override string ToString() => $@"
Product ID: {ID}
Customer Name: {CustomerName}
Order Status: {Status}
AmountOfItems: {AmountOfItems}
Total Price: {TotalPrice}
";
}

