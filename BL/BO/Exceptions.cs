using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public class InCorrectData : Exception
{
    public override string Message => "ERROR: One or more of the data you entered is incorrect";
    public override string ToString()
    {
        return Message;
    }

}
public class NotPossibleToFillRequest : Exception
{
    public override string Message => "ERROR: It is not possible to fill out the request";
    public override string ToString()
    {
        return Message;
    }

}
public class OutOfStock : Exception
{
    public override string Message => "ERROR: The product you requested is out of stock";
    public override string ToString()
    {
        return Message;
    }

}

