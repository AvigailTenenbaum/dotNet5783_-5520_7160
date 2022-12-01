﻿using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
/// <summary>
/// the entity for the orderItem
/// </summary>
    public class OrderItem
    {
    /// <summary>
    /// the id for the orderItem
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// product name
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Order Item ID
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// product price
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// Quantity of items of a product in the basket/order
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    /// Total price of an item 
    /// </summary>
    public double TotalPrice { get; set; }
    public override string ToString() => $@"
Order Item ID: {ID}
Product Name: {Name}
Product ID: {ProductID}
Price: {Price}
Amount: {Amount}
TotalPrice: {TotalPrice}
";
}

