using BO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
public enum OrderStatus { Approved, shipped, deliveredTotheCustomer };
namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public ObservableCollection<OrderForList?> OrderForLists { get; set; }
    
        public OrderListWindow()
        {
            OrderForLists = new ObservableCollection<OrderForList?>(bl!.Order.GetListOfOrder());
            InitializeComponent();
        }


        /// <summary>
        /// Double clicking on an item in the list and opening an order details window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (OrderListview.SelectedItem == null) return;
            new OrderWindow(((OrderForList)OrderListview.SelectedItem).ID,UpdateOrder).ShowDialog();
        }
        private void UpdateOrder(OrderForList order)//A function for updating the list every time an order is updated
        {
            if (order.CustomerName == null)
            {
                var p = OrderForLists?.FirstOrDefault(item => item?.ID == order.ID);
                int i = OrderForLists.IndexOf(p);
                OrderForLists.RemoveAt(i);
            }
            else
            {
                var p = OrderForLists?.FirstOrDefault(item => item?.ID == order.ID);
                int i = OrderForLists.IndexOf(p);
                OrderForLists[i] = order;
            }
        }

    }
 
}
