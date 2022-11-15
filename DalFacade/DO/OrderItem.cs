

namespace DO;
/// <summary>
/// structere for item on order
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// uniqe id of the item in order
    /// </summary>
  public  int ID { get; set; }
    /// <summary>
    /// id of the product
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// id of the order
    /// </summary>
    public int OrderID { get; set; }
    /// <summary>
    /// price per unite
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// amount of products
    /// </summary>
   public int Amount { get; set; }
    public override string ToString() => $@"
Order Item ID: {ID}
Product ID: {ProductID}
Order Id: {OrderID}
Price: {Price}
Amount: {Amount}
";

}
