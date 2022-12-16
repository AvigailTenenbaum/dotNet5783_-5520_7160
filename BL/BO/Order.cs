using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

    /// <summary>
    /// the entity for the order
    /// </summary>
    public class Order
    {
        /// <summary>
        /// the id for the order
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// the name of the custumer who did the order
        /// </summary>
        public string? CustomerName { get; set; }
        /// <summary>
        /// the email of the custumer
        /// </summary>
        public string? CustomerEmail { get; set; }
        /// <summary>
        /// the adress of the custumer
        /// </summary>
        public string? CustomerAdress { get; set; }
        /// <summary>
        /// the date that the order גone
        /// </summary>
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// where the order is found
        /// </summary>
        public OrderStatus? Status { get; set; }
        /// <summary>
        /// the shipDate for the order
        /// </summary>
        public DateTime? ShipDate { get; set; }
        /// <summary>
        /// the day of the delivery
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        ///List of order details
        /// </summary>
        public List< OrderItem?>? Items { get; set; }
        /// <summary>
        /// Total price of order
        /// </summary>
        public double TotalPrice { get; set; }
    public override string ToString() => this.ToStringProperty();
    }

