using BO;
using System;
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
            try
            {
                Order = bl!.Order.GetOrderDetails(id);
            }
            catch(NotExist ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Order = bl!.Order.UpdateOrder((int)id.Content,Convert.ToInt32( productIdTxt.Text),Convert.ToInt32( Amounttxt.Text));
            lvProducts.Items.Refresh();
        }
    }
}
