using BO;
using System.Collections.ObjectModel;
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
        public OrderListWindow()
        {
            InitializeComponent();
            ObservableCollection<OrderForList?> orderForLists = new ObservableCollection<OrderForList?>(bl!.Order.GetListOfOrder());
            DataContext = orderForLists;
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<OrderForList?> orderForLists = new ObservableCollection<OrderForList?>(bl!.Order.GetListOfOrder());
            DataContext = orderForLists;
        }

        private void OrderListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (OrderListview.SelectedItem == null) return;
            new OrderWindow(((OrderForList)OrderListview.SelectedItem).ID).ShowDialog();

        }
    }
}
