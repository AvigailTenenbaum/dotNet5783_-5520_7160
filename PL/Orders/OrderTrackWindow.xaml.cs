using System;
using System.Windows;

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderTrackWindow.xaml
    /// </summary>
    public partial class OrderTrackWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        public BO.OrderTracking? OrderT
        {
            get { return (BO.OrderTracking?)GetValue(OrderTProperty); }
            set { SetValue(OrderTProperty, value); }
        }
        public int ID;
        public static readonly DependencyProperty OrderTProperty =
            DependencyProperty.Register("OrderT", typeof(BO.OrderTracking), typeof(Window), new PropertyMetadata(null));
        public OrderTrackWindow(int id)
        {

            InitializeComponent();
            this.ID = id;

            try
            {
                OrderT = bl.Order.OrderTracking(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Order order = bl!.Order.GetOrderDetails(ID);
                new OrderWindow(order).ShowDialog();
            }
            catch (BO.NotExist ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
