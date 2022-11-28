using DO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BO;

    public class OrderTracking
    {
    /// <summary>
    /// Unique ID for shipment tracking
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// order status
    /// </summary>
    public OrderStatus orderStatus { get; set; }
    /// <summary>
    /// A list of dates and the status of the reservation is hosted
    /// </summary>
    public List<Tuple<string, DateTime>> TrackingInformation { get; set; }
    public override string ToString() => $@"
Order Tracking ID: {ID}
order Status: {orderStatus}
Order Status By Dates: {string.Join('\n',TrackingInformation)}
";
}

