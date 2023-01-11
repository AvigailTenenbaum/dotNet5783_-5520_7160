using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

sealed internal class DalXml : IDal
{
    public Iproduct Product { get; } = new Dal.Product();
    public Iorder Order { get; } = new Dal.Order();
    public IorderItem OrderItem { get; } = new Dal.OrderItem();
    
}
