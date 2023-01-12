using DalApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

sealed internal class DalXml : IDal
{
   
    public static IDal Instance { get; } = new DalXml();
    public Iorder Order { get; } = new Order();
    public Iproduct Product { get; } = new Product();
    public IorderItem OrderItem { get; } = new OrderItem();
    private DalXml() { }
}
