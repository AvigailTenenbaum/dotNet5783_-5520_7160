using BO;
using DO;
using System;
using System.Linq;
using System.Windows;

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
       
        public BO.Order Order
        {
            get { return (BO.Order)GetValue(OrderProperty); }
            set { SetValue(OrderProperty, value); }
        }
        public static readonly DependencyProperty OrderProperty =
            DependencyProperty.Register("Order", typeof(BO.Order), typeof(Window), new PropertyMetadata(null));
        public BO.Order OrderForCustomer { set; get; }
        private Action<OrderForList> action;//A variable for an action to be performed in the list in the previous window
        public OrderWindow(int id, Action<OrderForList> action)//Constructor for update mode
        {
            this.action= action;
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
        public OrderWindow(BO.Order o)//Constructor for view mode
        {
            Order= o;
            OrderForCustomer = o;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bl!.Order.OrderShippingUpdate((int)id.Content);
            Order = bl.Order.GetOrderDetails(Order.ID);
            action(bl?.Order.GetListOfOrder().Where((p => p.ID == Order?.ID)).FirstOrDefault());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            bl!.Order.OrderDeliveryUpdate((int)id.Content);
            Order = bl.Order.GetOrderDetails(Order.ID);
            action(bl?.Order.GetListOfOrder().Where((p => p.ID == Order?.ID)).FirstOrDefault());
        }

       private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                int idForDelete=Order.ID;
                Order = bl!.Order.UpdateOrder((int)id.Content, Convert.ToInt32(productIdTxt.Text),
                    Convert.ToInt32(Amounttxt.Text));
                MessageBox.Show("The order has been successfully updated");
                action(bl?.Order.GetListOfOrder().Where((p => p.ID == Order?.ID)).FirstOrDefault()
                    ??new OrderForList() { ID=idForDelete});
                itemsListView.Items.Refresh();
                this.Close();
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
            
        }
    }
}
