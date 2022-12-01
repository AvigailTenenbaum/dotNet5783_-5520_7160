

namespace DO;
/// <summary>
///structure for order details
/// </summary>
public struct Order
{
    /// <summary>
    /// uniqe id of every order
    /// </summary>
   public int ID { get; set;}
    /// <summary>
    /// the name of the customer
    /// </summary>
   public string? CustomerName { get; set;}
    /// <summary>
    /// the email of the customer
    /// </summary>
    public string? CustomerEmail { get; set;}
    /// <summary>
    /// the address of the customer
    /// </summary>
   public string? CustomerAddress { get; set;}
    /// <summary>
    /// date of placing the order
    /// </summary>
   public DateTime? OrderDate { get; set;}
    /// <summary>
    ///  date of shipping
    /// </summary>
   public DateTime? ShipDate { get; set;}
    /// <summary>
    /// date of delivery 
    /// </summary>
    public DateTime? DeliveryDate { get; set;}
    public override string ToString() => $@"
Order ID={ID}
Customr Name: {CustomerName}
Customer Email: {CustomerEmail}
Customer Address: {CustomerAddress}
Order Date: {OrderDate}
Ship Date: {ShipDate}
Delivery date: {DeliveryDate}
";
}
