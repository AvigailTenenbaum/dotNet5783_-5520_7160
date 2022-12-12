using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
 public class ProductForList
 {
    /// <summary>
    /// A unique identifier for a listed product
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// product name
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// product price
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// The product category
    /// </summary>
    public Category? Category { get; set; }
    public override string ToString() => this.ToStringProperty();
}
