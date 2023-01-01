using DalApi;

namespace Dal
{
    internal sealed class DalList : IDal
    {
        public static IDal Instance { get; } = new DalList();
        public Iorder Order { get; } = new DalOrder();
        public Iproduct Product { get; } = new DalProduct();
        public IorderItem OrderItem { get; } = new DalOrderItem();
        private DalList() { }

    }
}
