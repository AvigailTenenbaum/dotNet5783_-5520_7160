using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    internal interface IDal
    {
        Iorder Order { get; }
        Iproduct Product { get; }
        IorderItem IorderItem { get; }

    }
}
