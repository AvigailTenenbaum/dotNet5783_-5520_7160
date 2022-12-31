using BO;
using PL.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
            DataContext= orderForLists;
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
