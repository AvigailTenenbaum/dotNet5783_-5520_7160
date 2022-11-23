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
    /// Product availability
    /// </summary>
    public bool InStock { get; set; }
}

