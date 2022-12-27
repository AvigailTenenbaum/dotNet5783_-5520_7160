using BO;
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
    private DalApi.IDal? dal = DalApi.Factory.Get();
    /// <summary>
    /// A method for requesting a list of orders
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList?> GetListOfOrder()
    {
            IEnumerable<DO.Order?> orders=dal?.Order.GetAllObject()??throw new BO.NullData();
        IEnumerable<DO.OrderItem?> items = dal.OrderItem.GetAllObject();

            return from DO.Order item in orders
                   let orderItems = items.Where(items => items?.OrderID == item.ID)
                   select new BO.OrderForList()
                   {
                       ID = item.ID,
                       CustomerName = item.CustomerName,
                       Status = GetStatus(item),
                       AmountOfItems = orderItems.Count(),
                       TotalPrice = orderItems.Sum(items => items!.Value.Amount * items.Value.Price)
                   };
            //    List<OrderForList?> orderForLists= new List<OrderForList?>();
            //    foreach(DO.Order? order in orders)
            //    { 
            //    IEnumerable<DO.OrderItem?> orderItems=dal.OrderItem.GetAllObject();
            //        BO.OrderForList orderForList = new BO.OrderForList
            //        {
            //          ID = order?.ID??throw new BO.NullData(),
            //          CustomerName = order?.CustomerName ?? throw new BO.NullData(),
            //          AmountOfItems = 0,
            //         TotalPrice = 0 
            //        };
            //        if (order?.OrderDate< DateTime.Now)
            //            orderForList.Status = BO.OrderStatus.Approved;
            //        if (order?.ShipDate < DateTime.Now)
            //            orderForList.Status = BO.OrderStatus.shipped;
            //        if (order?.DeliveryDate < DateTime.Now)
            //            orderForList.Status = BO.OrderStatus.deliveredTotheCustomer;
            //        foreach (DO.OrderItem? oi in orderItems)
            //        {
            //            orderForList.AmountOfItems++;
            //            orderForList.TotalPrice += oi?.Price ?? throw new BO.NullData();
            //        }
            //        orderForLists.Add(orderForList);
            //    }
            //    return orderForLists;
            //}
            //catch (Exception ex) { throw ex; }
        }
    /// <summary>
    /// private function for calculate the status of the order by 3 station (ordered,shipped,deliverd)
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>

    private BO.OrderStatus GetStatus(DO.Order order)
    {
        return order.DeliveryDate != null ? BO.OrderStatus.deliveredTotheCustomer : order.ShipDate != null
            ? BO.OrderStatus.shipped : BO.OrderStatus.Approved;
    }
    /// <summary>
    /// A method for an order details request that receives the fat identifier and returns its details if the identifier exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Order GetOrderDetails(int id)
    {
        DO.Order? order/*=new DO.Order()*/;
        //IEnumerable<DO.OrderItem?> orderItems;
        if (id > 0)
        {
            try
            {
                order = dal?.Order.GetObject(id);
            }
            catch (DO.NotExist e) { throw new BO.NotExist(e); }
            //    try { orderItems = dal?.OrderItem.GetAllObject(item =>item?.OrderID==id)??throw new BO.NullData(); }

            //    catch (DO.NotExist e) { throw new BO.NotExist(e); }
            //    BO.Order getOrder = new BO.Order { ID = order?.ID??throw new BO.NullData(), CustomerName = order?.CustomerName??throw new BO.NullData(), CustomerEmail = order?.CustomerEmail??throw new BO.NullData(), CustomerAdress = order?.CustomerAddress??throw new BO.NullData(), OrderDate = order?.OrderDate, ShipDate = order?.ShipDate, DeliveryDate = order?.DeliveryDate,Items=new List<BO.OrderItem?>() };
            //    if (order?.OrderDate < DateTime.Now)
            //        getOrder.Status = BO.OrderStatus.Approved;
            //    if (order?.ShipDate < DateTime.Now)
            //        getOrder.Status = BO.OrderStatus.shipped;
            //    if (order?.DeliveryDate < DateTime.Now)
            //        getOrder.Status = BO.OrderStatus.deliveredTotheCustomer;

            //    foreach (DO.OrderItem? orderItem in orderItems)
            //    {
            //        BO.OrderItem orderItem1 = new BO.OrderItem
            //        {
            //            ID = orderItem?.ID?? throw new BO.NullData(),
            //            ProductID = orderItem?.ProductID ?? throw new BO.NullData(),
            //            Price = orderItem?.Price ?? throw new BO.NullData(),
            //            Amount = orderItem?.Amount ?? throw new BO.NullData(),
            //            TotalPrice = orderItem?.Price??0* orderItem?.Amount??0,//can't be null here
            //        };
            //        getOrder.Items.Add(orderItem1);
            //        getOrder.TotalPrice += orderItem1.TotalPrice;
            //    }
            //    return getOrder;
            //}
            return new BO.Order()
            {
                ID = order?.ID??throw new BO.NullData(),
                CustomerName = order?.CustomerName,
                Status = GetStatus((DO.Order)order),
                CustomerAdress = order?.CustomerAddress ?? throw new BO.NullData(),
                CustomerEmail = order?.CustomerEmail ?? throw new BO.NullData(),
                DeliveryDate = order?.DeliveryDate ?? throw new BO.NullData(),
                OrderDate = order?.OrderDate ?? throw new BO.NullData(),
                ShipDate = order?.ShipDate ?? throw new BO.NullData(),
                Items = (List<BO.OrderItem?>)GetListOfOrderItem(dal?.OrderItem.GetAllObject().Where(x => x?.OrderID == order?.ID)),
                TotalPrice = GetListOfOrderItem(dal?.OrderItem.GetAllObject().Where(x => x?.OrderID == order?.ID)).Sum(x => x!.TotalPrice),
            };
        }
        else
            throw new BO.InCorrectData();
    }
        /// <summary>
        /// private function for get the list of the  order item for each order instead of doing it inside the function above 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>

        private IEnumerable<BO.OrderItem?> GetListOfOrderItem(IEnumerable<DO.OrderItem?> list)
        {
            return from DO.OrderItem item in list
                   select new BO.OrderItem()
                   {
                       ID = item.ID,
                       Name = dal!.Product.GetObject(item.ProductID)?.Name,
                       Price = item.Price,
                       Amount = item.Amount,
                       TotalPrice = item.Price * item.Amount,
                       ProductID = dal.Product.GetObject(item.ProductID)?.ID ?? throw new BO.NullData()
                   };
        }

        /// <summary>
        /// A method for updating an order shipment that receives an order number and updates the ship date if the order exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BO.Order OrderShippingUpdate(int id)
    {
        DO.Order order;
        try { order = (DO.Order)dal?.Order.GetObject(id)!; }catch(DO.NotExist e) { throw new BO.NotExist(e); }
        if(order.ShipDate==null||!(order.ShipDate<DateTime.Now))
            order.ShipDate= DateTime.Now;
        try { dal.Order.UpDateObject(order); }catch(DO.NotExist e) { throw new BO.NotExist(e);}
        BO.Order logicOrder=new BO.Order { ID= order.ID,CustomerName=order.CustomerName, CustomerEmail= order.CustomerEmail,CustomerAdress=order.CustomerAddress,ShipDate=order.ShipDate ,DeliveryDate=order.DeliveryDate,Status=BO.OrderStatus.shipped,Items =new List<BO.OrderItem?>(),OrderDate=order.OrderDate};
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
        try { order = (DO.Order)dal?.Order.GetObject(id)!; } catch (DO.NotExist e) { throw new BO.NotExist(e); }
        if (order.DeliveryDate == null|| !(order.DeliveryDate < DateTime.Now))
            order.DeliveryDate = DateTime.Now;
        try { dal.Order.UpDateObject(order); } catch (DO.NotExist e) { throw new BO.NotExist(e); }
        BO.Order logicOrder = new BO.Order { ID = order.ID, CustomerName = order.CustomerName, CustomerEmail = order.CustomerEmail, CustomerAdress = order.CustomerAddress, DeliveryDate = order.DeliveryDate,ShipDate=order.ShipDate, Status = BO.OrderStatus.deliveredTotheCustomer, Items = new List<BO.OrderItem?>(), OrderDate = order.OrderDate };
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
        try { order =(DO.Order) dal?.Order.GetObject(id)!; } catch (DO.NotExist e) { throw new BO.NotExist(e); }
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
    /// <summary>
    /// A method for updating an order to the manager
    /// </summary>
    /// <param name="IDOrder"></param>
    /// <param name="IDProduct"></param>
    /// <param name="newAmount"></param>
    /// <returns></returns>
    /// <exception cref="BO.InCorrectData"></exception>
    /// <exception cref="BO.NotExist"></exception>
    /// <exception cref="BO.NotPossibleToFillRequest"></exception>
    public BO.Order UpdateOrder(int IDOrder, int IDProduct, int newAmount)
    {
        if (IDOrder < 0)
            throw new BO.InCorrectData();
        if (IDProduct < 0)
            throw new BO.InCorrectData();
        if (newAmount < 0)
            throw new BO.InCorrectData();
        try { dal?.Order.GetObject(IDOrder); }
        catch (Exception ex)
        {
            throw new BO.NotExist(ex);
        }
        if (dal?.Order.GetObject(IDOrder)?.ShipDate <= DateTime.Now)
            throw new BO.NotPossibleToFillRequest();
        BO.Order order = GetOrderDetails(IDOrder);
        foreach (BO.OrderItem? orderItem in order.Items??throw new BO.NullData())
        {
            if (orderItem?.ProductID == IDProduct)
            {
                order.TotalPrice -= orderItem.TotalPrice;//for calculate the new total price of the order
                orderItem.Amount = newAmount;
                orderItem.TotalPrice = newAmount * orderItem.Price;
                order.TotalPrice += orderItem.TotalPrice;//for calculate the new total price of the order
            }
        }
        return order;
    }
}

