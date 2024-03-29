﻿namespace BO;

/// <summary>
/// the entity of cart
/// </summary>
public class Cart
{
    /// <summary>
    /// customer name
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// Buyer's email address
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    /// Buyer's address
    /// </summary>
    public string? CostumerAdress { get; set; }
    /// <summary>
    /// List of order details 
    /// </summary>
    public List<OrderItem?>? Items { get; set; }
    /// <summary>
    /// Total price of an order basket
    /// </summary>
    public double TotalPrice { get; set; }
    public override string ToString() => this.ToStringProperty();

}

