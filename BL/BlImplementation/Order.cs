using BlApi;
using BO;
using DalApi;
using DO;
using System.Runtime.CompilerServices;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    private DalApi.IDal? dal = DalApi.Factory.Get();
    /// <summary>
    /// A method for requesting a list of orders
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList?> GetListOfOrder()
    {
        IEnumerable<DO.Order?> orders = dal?.Order.GetAllObject() ?? throw new BO.NullData();
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
        DO.Order order = new DO.Order();
        IEnumerable<DO.OrderItem?> orderItems;
        if (id > 0)
        {
            try
            {
                order = dal?.Order.GetObject(id) ?? throw new BO.NullData();
            }
            catch (DO.NotExist e) { throw new BO.NotExist(e); }
            try { orderItems = dal?.OrderItem.GetAllObject(item => item?.OrderID == id) ?? throw new BO.NullData(); }

            catch (DO.NotExist e) { throw new BO.NotExist(e); }
            BO.Order getOrder = new BO.Order
            {
                ID = order.ID,
                CustomerName = order.CustomerName,
                CustomerEmail = order.CustomerEmail,
                CustomerAdress = order.CustomerAddress,
                OrderDate = order.OrderDate,
                ShipDate = order.ShipDate,
                DeliveryDate = order.DeliveryDate,
                Items = new List<BO.OrderItem?>()
            };
            if (order.OrderDate < DateTime.Now)
                getOrder.Status = BO.OrderStatus.Approved;
            if (order.ShipDate < DateTime.Now)
                getOrder.Status = BO.OrderStatus.shipped;
            if (order.DeliveryDate < DateTime.Now)
                getOrder.Status = BO.OrderStatus.deliveredTotheCustomer;
            IEnumerable<BO.OrderItem?> orderItems1;
            try
            {
                orderItems1 = from oi in orderItems
                              let OItem = new BO.OrderItem()
                              {
                                  ID = oi?.ID ?? throw new BO.NullData(),
                                  Name = dal.Product.GetObject(oi?.ProductID ?? throw new BO.NullData())?.Name,
                                  ProductID = oi?.ProductID ?? throw new NullData(),
                                  Price = oi?.Price ?? throw new NullData(),
                                  Amount = oi?.Amount ?? throw new NullData(),
                                  TotalPrice = oi?.Price * oi?.Amount ?? throw new NullData(),
                              }
                              select OItem;
            }
            catch (Exception ex)
            {
                throw new NotPossibleToFillRequest();
            }
            getOrder.Items = orderItems1.ToList();
            getOrder.TotalPrice = getOrder.Items.Sum(o => o?.TotalPrice ?? throw new NullData());
            return getOrder;
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
    [MethodImpl(MethodImplOptions.Synchronized)]

    /// <summary>
    /// A method for updating an order shipment that receives an order number and updates the ship date if the order exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Order OrderShippingUpdate(int id)
    {
        DO.Order order;
        try { order = (DO.Order)dal?.Order.GetObject(id)!; } catch (DO.NotExist e) { throw new BO.NotExist(e); }
        if (order.ShipDate == null || !(order.ShipDate < DateTime.Now))
            order.ShipDate = DateTime.Now;
        try { dal.Order.UpDateObject(order); } catch (DO.NotExist e) { throw new BO.NotExist(e); }
        BO.Order logicOrder = new BO.Order { ID = order.ID, CustomerName = order.CustomerName, CustomerEmail = order.CustomerEmail, CustomerAdress = order.CustomerAddress, ShipDate = order.ShipDate, DeliveryDate = order.DeliveryDate, Status = BO.OrderStatus.shipped, Items = new List<BO.OrderItem?>(), OrderDate = order.OrderDate };
        return logicOrder;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]

    /// <summary>
    /// A method for updating an order delivery that receives an order number and updates the delivery date if the order exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Order OrderDeliveryUpdate(int id)
    {
        DO.Order order;
        try { order = (DO.Order)dal?.Order.GetObject(id)!; } catch (DO.NotExist e) { throw new BO.NotExist(e); }
        if (order.DeliveryDate == null || !(order.DeliveryDate < DateTime.Now))
            order.DeliveryDate = DateTime.Now;
        try { dal.Order.UpDateObject(order); } catch (DO.NotExist e) { throw new BO.NotExist(e); }
        BO.Order logicOrder = new BO.Order { ID = order.ID, CustomerName = order.CustomerName, CustomerEmail = order.CustomerEmail, CustomerAdress = order.CustomerAddress, DeliveryDate = order.DeliveryDate, ShipDate = order.ShipDate, Status = BO.OrderStatus.deliveredTotheCustomer, Items = new List<BO.OrderItem?>(), OrderDate = order.OrderDate };
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
        try { order = (DO.Order)dal?.Order.GetObject(id)!; } catch (DO.NotExist e) { throw new BO.NotExist(e); }
        BO.OrderTracking orderTracking = new BO.OrderTracking { ID = order.ID };
        orderTracking.TrackingInformation = new List<Tuple<string?, DateTime?>>();
        if (order.OrderDate !=null)
        {
            orderTracking.orderStatus = BO.OrderStatus.Approved;
            orderTracking.TrackingInformation.Add(new Tuple<string?, DateTime?>("The order has been created", order.OrderDate));
        }
        if (order.ShipDate != null)
        {
            orderTracking.orderStatus = BO.OrderStatus.shipped;
            orderTracking.TrackingInformation.Add(new Tuple<string?, DateTime?>("The order is sent", order.ShipDate));
        }
        if (order.DeliveryDate != null)
        {
            orderTracking.orderStatus = BO.OrderStatus.deliveredTotheCustomer;
            orderTracking.TrackingInformation.Add(new Tuple<string?, DateTime?>("The order has been delivered to the customer", order.DeliveryDate));
        }
        return orderTracking;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]

    /// <summary>
    /// A method for updating an order to the manager
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="productId"></param>
    /// <param name="newAmount"></param>
    /// <returns></returns>
    /// <exception cref="BO.InCorrectData"></exception>
    /// <exception cref="BO.NotExist"></exception>
    /// <exception cref="BO.NotPossibleToFillRequest"></exception>
    public BO.Order UpdateOrder(int orderId, int productId, int newAmount)
    {
        lock (dal)
        {
            if (orderId < 0)
                throw new BO.InCorrectData();
            if (productId < 0)
                throw new BO.InCorrectData();
            if (newAmount < 0)
                throw new BO.InCorrectData();
            DO.Order? order;
            try { order = dal?.Order.GetObject(orderId); }
            catch (Exception ex)
            {
                throw new BO.NotExist(ex);
            }
            if (order?.DeliveryDate <= DateTime.Today)
                throw new NotPossibleToFillRequest();
            DO.Product product;
            try { product = (DO.Product)dal!.Product.GetObject(productId); }//for update in stock field
            catch (Exception ex) { throw new BO.NotExist(ex); }
            BO.Order? wantedOrder = GetOrderDetails(orderId);
            BO.OrderItem? oi = wantedOrder?.Items?.FirstOrDefault(oi => oi?.ProductID == productId);
            if (product.InStock <= 0 || product.InStock < newAmount)
                throw new NotPossibleToFillRequest();
            if (newAmount > product.InStock - oi?.Amount)
                throw new NotPossibleToFillRequest();
            if (newAmount != 0)
            {
                if (oi == null)//if he product is not in the order, add it
                {
                    DO.Product? productHelp = dal?.Product.GetObject(productId);
                    IEnumerable<DO.OrderItem?> orderItems = dal?.OrderItem.GetAllObject();
                    oi = new BO.OrderItem()
                    {

                        Amount = newAmount,
                        Name = productHelp?.Name,
                        Price = productHelp?.Price ?? 0,
                        ProductID = productId,
                        TotalPrice = newAmount * productHelp?.Price ?? 0,
                    };
                    DO.OrderItem add = new DO.OrderItem()//update in the data layer
                    {

                        Amount = oi.Amount,
                        OrderID = orderId,
                        Price = oi.Price,
                        ProductID = productId
                    };
                    wantedOrder?.Items?.Add(oi);
                    dal?.OrderItem.AddObject(add);
                    product.InStock -= add.Amount;
                    dal?.Product.UpDateObject(product);//update the amount in stocp of product
                    return wantedOrder!;
                }
                //if the product has been in the order already
                product.InStock += oi.Amount;
                wantedOrder!.TotalPrice -= oi!.TotalPrice;//for calculate the new total price of the order
                oi.Amount = newAmount;
                oi.TotalPrice = newAmount * oi.Price;
                wantedOrder.TotalPrice += oi.TotalPrice;//for calculate the new total price of the order
                DO.OrderItem update = new DO.OrderItem()
                {
                    ID = oi.ID,
                    Amount = oi.Amount,
                    OrderID = orderId,
                    Price = oi.Price,
                    ProductID = productId
                };

                product.InStock -= oi.Amount;
                dal.Product.UpDateObject(product);
                dal?.OrderItem.UpDateObject(update);
                return wantedOrder;
            }
            else
            {
                product.InStock += oi.Amount;
                dal.Product.UpDateObject(product);
                wantedOrder?.Items?.Remove(oi);
                dal?.OrderItem.DeleteObject(oi.ID);
                if (wantedOrder?.Items?.Count() == 0)
                {
                    dal?.Order.DeleteObject(orderId);
                }
                return wantedOrder;
            }
        }
    }

   public IEnumerable<StatisticsOrders> GetStatisticsOrders()
    {
        return from order in GetListOfOrder()
               group order by order.Status into newGroup
               select new StatisticsOrders
               {
                   Status = newGroup.Key,
                   Count = newGroup.Count()
               };
    }
    public int? nextOrderSending()
    {
        try
        {
            IEnumerable<OrderTracking> orderTrackings = dal!.Order.GetAllObject(order => order?.DeliveryDate is null).Select(order => OrderTracking(order.GetValueOrDefault().ID));
            return orderTrackings.MinBy(orderTracking =>
                                        orderTracking.TrackingInformation![orderTracking.TrackingInformation.Count() - 1].Item2
                                        .GetValueOrDefault())?.ID;
        }
        catch (Exception)
        {

            throw;
        }
    }
}

public struct StatisticsOrders
{
    public OrderStatus? Status{ get; set; }
    public int Count { get; set; }
}
