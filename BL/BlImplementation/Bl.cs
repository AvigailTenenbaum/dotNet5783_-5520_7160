using BlApi;

namespace BlImplementation;

sealed internal class Bl : IBl
{
    internal Bl() { }
    public ICart Cart { get; } = new BlImplementation.Cart();
    public IOrder Order { get; } = new BlImplementation.Order();
    public IProduct Product { get; } = new BlImplementation.Product();
}


