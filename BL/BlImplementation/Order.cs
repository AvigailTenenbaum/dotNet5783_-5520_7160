using BO;
using Dal;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

    internal class Order:Iorder
    {
    private IDal _dal = new DalList();
    /// <summary>
    /// A method for requesting a list of orders
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList> GetListOfProducts()
    {
      try
        {
            IEnumerable<DO.Order> orders=
        }
    }
    /// <summary>
    /// A method for an order details request that receives the fat identifier and returns its details if the identifier exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Order GetOrderDetails(int id)
    {
        if (id > 0)
        {

            try
            {
                DO.Order order = _dal.Order.GetObject(id);
                BO.Order newOrder = new BO.Order
                {
                    ID = order.ID,
                    CustomerName = order.CustomerName,
                    CustumerEmail = order.CustomerEmail,
                    CustumerAdress = order.CustomerAddress,
                    OrderDate= order.OrderDate,
                    ShipDate= order.ShipDate,
                    DeliveryDate= order.DeliveryDate,
                    Items=order.
                };
                return newOrder;
            }
            catch (DO.NotExist e) { throw e; }

        }
        else
            throw new BO.InCorrectData();
    }
    /// <summary>
    /// A method for updating an order shipment that receives an order number and updates the ship date if the order exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Order OrderShippingUpdate(int id)
    {
        DO.Order order;
        try { order = _dal.Order.GetObject(id); }catch(DO.NotExist e) { throw e; }
        if(order.ShipDate==null)
            order.ShipDate= DateTime.Now;
        _dal.Order.UpDateObject(order);
    }
    /// <summary>
    /// A method for updating an order delivery that receives an order number and updates the delivery date if the order exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Order OrderDeliveryUpdate(int id)
    {

    }
    /// <summary>
    /// A method for order tracking that receives an order number and returns an instance of order tracking if the order exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.OrderTracking OrderTracking(int id)
    {
        DO.Order order;
        try { order = _dal.Order.GetObject(id); } catch (DO.NotExist e) { throw e; }
        BO.OrderTracking orderTracking= new BO.OrderTracking {ID=order.ID,orderStatus=order. };
    }
}

