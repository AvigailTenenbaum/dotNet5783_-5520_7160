using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public class BlExtptions: Exception
{
    public BlExtptions(string message):base(message){ }
    public BlExtptions(string message, Exception ex):base(message, ex) { }
}
public class InCorrectData : BlExtptions
{
    public InCorrectData() : base("ERROR: One or more of the data you entered is incorrect") { }    

}
public class NotPossibleToFillRequest : BlExtptions
{
   public NotPossibleToFillRequest() : base("ERROR: It is not possible to fill out the request") { }
}
public class OutOfStock : BlExtptions
{
    public OutOfStock() : base("ERROR: The product you requested is out of stock") { }    
}
public class NotExist:BlExtptions
{
    public NotExist(Exception ex): base("ERROR: entity was not found", ex) { }
}
public class AllReadyExist:BlExtptions
{
    public AllReadyExist(Exception ex): base("ERROR: entity is allready exist in the list") { }
}

