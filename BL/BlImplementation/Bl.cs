using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

sealed internal class Bl : IBl
{
    internal Bl() { }
    public ICart Cart { get; } = new BlImplementation.Cart();
    public IOrder Order { get; } = new BlImplementation.Order();
    public IProduct Product { get; } = new  BlImplementation.Product();
}


