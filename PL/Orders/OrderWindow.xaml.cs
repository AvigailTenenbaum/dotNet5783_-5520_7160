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
        public BO.Order OrderForCustomer { set; get; }
        public OrderWindow(int id)
        {
            try
            {
                Order = bl!.Order.GetOrderDetails(id);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            InitializeComponent();
        }
        public OrderWindow(Order o)
        {
            Order= o;
            OrderForCustomer = o;
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

        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement? framework = sender as FrameworkElement;
                OrderItem? orderItem = (OrderItem?)framework?.DataContext;
                int productId = orderItem!.ProductID;
                int amount = orderItem.Amount;
                int orderId = orderItem.ID;
                Order = bl!.Order.UpdateOrder((int)id.Content, productId,amount);
                itemsListView.Items.Refresh();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
    }
}
