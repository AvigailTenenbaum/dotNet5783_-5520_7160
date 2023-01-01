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
    public OrderStatus? orderStatus { get; set; }
    /// <summary>
    /// A list of dates and the status of the reservation is hosted
    /// </summary>
    public List<Tuple<string?, DateTime?>>? TrackingInformation { get; set; }
    public override string ToString() => this.ToStringProperty();
}

