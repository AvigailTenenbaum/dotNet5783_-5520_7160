using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

    /// <summary>
    /// entity for product
    /// </summary>
    public class Product
    {
        /// <summary>
        /// the id for the product
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// the name of the product
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// the price for the product
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// the category that the product belongs
        /// </summary>
        public Category? Category { get; set; }
        /// <summary>
        /// how much products in stock
        /// </summary>
        public int InStock { get; set; }
    public override string ToString() => this.ToStringProperty(); 
}

