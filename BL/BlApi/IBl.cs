namespace BlApi;
/// <summary>
/// A main interface that will bring together all the interfaces of the layer
/// </summary>
public interface IBl
{
    /// <summary>
    /// attribute for a shopping cart entity
    /// </summary>
    public ICart Cart { get; }
    /// <summary>
    /// attribute for an order entity
    /// </summary>
    public IOrder Order { get; }
    /// <summary>
    /// attribute for a product entity
    /// </summary>
    public IProduct Product { get; }

}

