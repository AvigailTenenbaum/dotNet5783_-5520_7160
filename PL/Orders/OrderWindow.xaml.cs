using System.Windows;

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
