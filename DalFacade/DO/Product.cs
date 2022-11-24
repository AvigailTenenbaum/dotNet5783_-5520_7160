
namespace DO;
/// <summary>
/// structer for products details
/// </summary>
public struct Product
{
    /// <summary>
    /// uniqe of the id product
    /// </summary>
  public  int ID { get; set; }
    /// <summary>
    /// name of the product
    /// </summary>
  public  string Name { get; set; }
    /// <summary>
    /// price of product
    /// </summary>
   public double Price { get; set; }
    /// <summary>
    /// category of the product
    /// </summary>

    public Category Category { get; set; }
    /// <summary>
    /// amount in stock
    /// </summary>
    public int InStock { get; set; }
    public override string ToString() => $@"
Product ID: {ID}
Product Name: {Name}
Price: {Price}
Category: {Category}
Amount In Stock: {InStock}
";


}
