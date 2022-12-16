using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Dal
{
    internal sealed class DalList : IDal
    {
        public static IDal Instance { get; } = new DalList();
        public Iorder Order { get; }= new DalOrder();
        public Iproduct Product { get; } = new DalProduct();
        public IorderItem OrderItem { get; } = new DalOrderItem();
        private DalList() { }

    }
}
