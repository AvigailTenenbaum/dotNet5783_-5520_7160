using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using BlApi;
using BO;

namespace Simulator;

public static class Simulator
{
    static IBl? bl = Factory.Get();

    /// <summary>
    /// a random variable to use when it's needed.
    /// </summary>
    public static readonly Random random = new Random(DateTime.Now.Millisecond);

    private const int c_timeSleep = 1000;

    private static volatile bool stopSimulation;

    private static event Action? s_stopSimulation;

    public static event Action? s_StopSimulation
    {
        add => s_stopSimulation += value;
        remove => s_StopSimulation -= value;
    }

    private static event Action<int, object?>? s_updateSimulation;

    public static event Action<int, object>? s_UpdateSimulation
    {
        add => s_updateSimulation += value;
        remove => s_updateSimulation -= value;
    }

    public static void StartSimulator()
    {
        Thread thread = new Thread(simulatorAction);
        thread.Start();
       
    }

    private static void simulatorAction(object? obj)
    {
        while (!stopSimulation)
        {
            int? orderId = bl!.Order.nextOrderSending();

            if (orderId is null)
                Thread.Sleep(c_timeSleep);

            else
            {
                int _orderId = orderId ?? throw new InvalidCastException("");

                Order order = bl.Order.GetOrderDetails(_orderId);

                int nextOrderStatus = ((int)order.Status! + 1);
                int TreatmentTime = random.Next(3, 11);
                OrderProcess orderProcess = new OrderProcess
                {
                    CurrentOrder = order,
                    NextOrderStatus =
                    (BO.OrderStatus)nextOrderStatus,
                    EndTreatment = DateTime.Now.AddSeconds(TreatmentTime).ToString("HH:mm:ss")
            };

                s_updateSimulation?.Invoke(1, orderProcess);
                Thread.Sleep(TreatmentTime*1000);

                switch (orderProcess.CurrentOrder.Status)
                {
                    case OrderStatus.shipped:
                         orderProcess.CurrentOrder= bl.Order.OrderDeliveryUpdate(order.ID);
                        break;
                    case OrderStatus.Approved:
                        orderProcess.CurrentOrder = bl.Order.OrderShippingUpdate(order.ID);
                        break;

                    default:
                        break;
                }

                orderProcess.CurrentOrder = bl.Order.GetOrderDetails(_orderId);
                orderProcess.CurrentTime = null;
                orderProcess.EndTreatment = null;
                orderProcess.NextOrderStatus = orderProcess.CurrentOrder.Status == OrderStatus.deliveredTotheCustomer ? null :
                    (BO.OrderStatus)((int)orderProcess.CurrentOrder.Status! + 1);

            }
            Thread.Sleep(c_timeSleep);
        }
    }

    public static void StopSimulator()
    {
        stopSimulation = true;
        s_stopSimulation?.Invoke();
    }
}

public class OrderProcess
{
    public BO.Order? CurrentOrder { get; set; }

    public string? CurrentTime { get; set; } = DateTime.Now.ToString("HH:mm:ss");

    public BO.OrderStatus? NextOrderStatus { get; set; }

    public string? EndTreatment { get; set; }
}
















