using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IDal
    {
        Iorder Order { get; }
        Iproduct Product { get; }
        IorderItem OrderItem { get; }

    }
}
