namespace DalApi
{
    public interface IDal
    {
        Iorder Order { get; }
        Iproduct Product { get; }
        IorderItem OrderItem { get; }

    }
}
