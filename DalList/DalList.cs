using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Dal
{
    sealed public class DalList: IDal
    {
        public Iorder Order => new DalOrder();
        public Iproduct Product => new DalProduct();
        public IorderItem OrderItem => new DalOrderItem();

    }
}
