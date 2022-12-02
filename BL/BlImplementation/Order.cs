using BO;
using Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

    internal class Order: BlApi.IOrder
    {
    private IDal _dal = new DalList();
    /// <summary>
    /// A method for requesting a list of orders
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList?> GetListOfOrder()
    {
      try
        {
            IEnumerable<DO.Order?> orders=_dal.Order.GetAllObject();
            List<OrderForList?> orderForLists= new List<OrderForList?>();
            foreach(DO.Order order in orders)
            { 
            IEnumerable<DO.OrderItem?> orderItems=_dal.OrderItem.GetAllObject();
                BO.OrderForList orderForList = new BO.OrderForList
                {
                  ID = order.ID,
                  CustomerName = order.CustomerName,
                  AmountOfItems = 0,
                 TotalPrice = 0 
                };
                if (order.OrderDate < DateTime.Now)
                    orderForList.Status = BO.OrderStatus.Approved;
                if (order.ShipDate < DateTime.Now)
                    orderForList.Status = BO.OrderStatus.shipped;
                if (order.DeliveryDate < DateTime.Now)
                    orderForList.Status = BO.OrderStatus.deliveredTotheCustomer;
                foreach (DO.OrderItem oi in orderItems)
                {
                    orderForList.AmountOfItems++;
                    orderForList.TotalPrice += oi.Price;
                }
                orderForLists.Add(orderForList);
            }
            return orderForLists;
        }
        catch (Exception ex) { throw ex; }
    }
    /// <summary>
    /// A method for an order details request that receives the fat identifier and returns its details if the identifier exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Order GetOrderDetails(int id)
    {
        DO.Order order;
        IEnumerable<DO.OrderItem?> orderItems;
        if (id > 0)
        {
            try
            {
                order = _dal.Order.GetObject(id);
            }
            catch (DO.NotExist e) { throw new BO.NotExist(e); }
            try { orderItems = _dal.OrderItem.GetAllOrderItems(id); }
            catch (DO.NotExist e) { throw new BO.NotExist(e); }
            BO.Order getOrder = new BO.Order { ID = order.ID, CustomerName = order.CustomerName, CustomerEmail = order.CustomerEmail, CustomerAdress = order.CustomerAddress, OrderDate = order.OrderDate, ShipDate = order.ShipDate, DeliveryDate = order.DeliveryDate,Items=new List<BO.OrderItem>() };
            if (order.OrderDate < DateTime.Now)
                getOrder.Status = BO.OrderStatus.Approved;
            if (order.ShipDate < DateTime.Now)
                getOrder.Status = BO.OrderStatus.shipped;
            if (order.DeliveryDate < DateTime.Now)
                getOrder.Status = BO.OrderStatus.deliveredTotheCustomer;
            
            foreach (DO.OrderItem orderItem in orderItems)
            {
                BO.OrderItem orderItem1 = new BO.OrderItem
                {
                    ID = orderItem.ID,
                    ProductID = orderItem.ProductID,
                    Price = orderItem.Price,
                    Amount = orderItem.Amount,
                    TotalPrice = orderItem.Price * orderItem.Amount,
                };
                getOrder.Items.Add(orderItem1);
                getOrder.TotalPrice += orderItem1.TotalPrice;
            }
            return getOrder;
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
        try { order = _dal.Order.GetObject(id); }catch(DO.NotExist e) { throw new BO.NotExist(e); }
        if(order.ShipDate==DateTime.MinValue||!(order.ShipDate<DateTime.Now))
            order.ShipDate= DateTime.Now;
        try { _dal.Order.UpDateObject(order); }catch(DO.NotExist e) { throw new BO.NotExist(e);}
        BO.Order logicOrder=new BO.Order { ID= order.ID,CustomerName=order.CustomerName, CustomerEmail= order.CustomerEmail,CustomerAdress=order.CustomerAddress,ShipDate=order.ShipDate ,DeliveryDate=order.DeliveryDate,Status=BO.OrderStatus.shipped,Items =new List<BO.OrderItem>(),OrderDate=order.OrderDate};
        return logicOrder;
    }
    /// <summary>
    /// A method for updating an order delivery that receives an order number and updates the delivery date if the order exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Order OrderDeliveryUpdate(int id)
    {
        DO.Order order;
        try { order = _dal.Order.GetObject(id); } catch (DO.NotExist e) { throw new BO.NotExist(e); }
        if (order.DeliveryDate == DateTime.MinValue || !(order.DeliveryDate < DateTime.Now))
            order.DeliveryDate = DateTime.Now;
        try { _dal.Order.UpDateObject(order); } catch (DO.NotExist e) { throw new BO.NotExist(e); }
        BO.Order logicOrder = new BO.Order { ID = order.ID, CustomerName = order.CustomerName, CustomerEmail = order.CustomerEmail, CustomerAdress = order.CustomerAddress, DeliveryDate = order.DeliveryDate,ShipDate=order.ShipDate, Status = BO.OrderStatus.deliveredTotheCustomer, Items = new List<BO.OrderItem>(), OrderDate = order.OrderDate };
        return logicOrder;
    }
    /// <summary>
    /// A method for order tracking that receives an order number and returns an instance of order tracking if the order exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.OrderTracking OrderTracking(int id)
    {
        DO.Order order;
        try { order = _dal.Order.GetObject(id); } catch (DO.NotExist e) { throw new BO.NotExist(e); }
        BO.OrderTracking orderTracking = new BO.OrderTracking { ID = order.ID };
        orderTracking.TrackingInformation = new List<Tuple<string?, DateTime?>>();
        if (order.OrderDate < DateTime.Now)
        {
            orderTracking.orderStatus = BO.OrderStatus.Approved;
            orderTracking.TrackingInformation.Add(new Tuple<string?, DateTime?>("The order has been created", order.OrderDate));
        }
        if (order.ShipDate < DateTime.Now)
        {
            orderTracking.orderStatus = BO.OrderStatus.shipped;
            orderTracking.TrackingInformation.Add(new Tuple<string?, DateTime?>("The order is sent", order.ShipDate));
        }
        if (order.DeliveryDate < DateTime.Now)
        {
            orderTracking.orderStatus = BO.OrderStatus.deliveredTotheCustomer;
            orderTracking.TrackingInformation.Add(new Tuple<string?, DateTime?>("The order has been delivered to the customer", order.ShipDate));
        }
        return orderTracking;
    }
}

