﻿using BlImplementation;

namespace BlApi;
/// <summary>
/// Interface for on-order operations
/// </summary>
public interface IOrder
{
    /// <summary>
    /// A method for requesting a list of orders
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList?> GetListOfOrder();
    /// <summary>
    /// A method for an order details request that receives the fat identifier and returns its details if the identifier exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Order GetOrderDetails(int id);
    /// <summary>
    /// A method for updating an order shipment that receives an order number and updates the ship date if the order exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Order OrderShippingUpdate(int id);
    /// <summary>
    /// A method for updating an order delivery that receives an order number and updates the delivery date if the order exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Order OrderDeliveryUpdate(int id);
    /// <summary>
    /// A method for order tracking that receives an order number and returns an instance of order tracking if the order exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.OrderTracking OrderTracking(int id);
    /// <summary>
    /// A method for updating an order to the manager
    /// </summary>
    /// <param name="IDOrder"></param>
    /// <param name="IDProduct"></param>
    /// <param name="newAmount"></param>
    /// <returns></returns>
    public BO.Order UpdateOrder(int IDOrder, int IDProduct, int newAmount);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerable<StatisticsOrders> GetStatisticsOrders();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int? nextOrderSending();
}

