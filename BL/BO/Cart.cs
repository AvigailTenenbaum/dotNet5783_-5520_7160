using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// the entity of cart
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// customer name
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Buyer's email address
        /// </summary>
        public string CustomerEmail { get; set; }
        /// <summary>
        /// Buyer's address
        /// </summary>
        public string CustomerAdress { get; set; }
        /// <summary>
        /// List of order details 
        /// </summary>
        public OrderItem Items { get; set; }
        /// <summary>
        /// Total price of an order basket
        /// </summary>
        public double TotalPrice { get; set; }
        public override string ToString() => $@"
Customr Name: {CustomerName}
Customer Email: {CustomerEmail}
Customer Address: {CustomerAdress}
Items: {Items}
TotalPrice {TotalPrice}

    }
}
