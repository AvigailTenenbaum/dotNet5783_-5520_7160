using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class ProductItem
{
    /// <summary>
    /// A unique identifier for a product item
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// product name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// product price
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// The product category
    /// </summary>
    public Category Category { get; set; }
    /// <summary>
    /// if their is a products in stock
    /// </summary>
    public bool InStock{ get; set; }
    /// <summary>
    /// how many products in cart
    /// </summary>
      public int AmountInCart { get; set; }

    
    public override string ToString() => $@"
Product ID: {ID}
Product Name: {Name}
Price: {Price}
Category: {Category}
In Stock?: {InStock}
Amount In Cart {AmountInCart}
";
}

