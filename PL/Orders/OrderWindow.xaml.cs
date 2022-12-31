using System;
using System.Collections.Generic;
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

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public BO.Order Order { set; get; }
        public OrderWindow(int id)
        {
            Order = bl!.Order.GetOrderDetails(id);
            DataContext = this;
            InitializeComponent();
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
             bl!.Order.OrderShippingUpdate((int)id.Content);
            Order = bl.Order.GetOrderDetails(Order.ID);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
          
            bl!.Order.OrderDeliveryUpdate((int)id.Content);
            Order = bl.Order.GetOrderDetails(Order.ID);
        }
    }
}
