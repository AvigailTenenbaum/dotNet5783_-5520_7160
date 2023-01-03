using BO;
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

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public BO.Cart Cart
        {
            get { return (BO.Cart)GetValue(CartProperty); }
            set { SetValue(CartProperty, value); }
        }
        public static readonly DependencyProperty CartProperty =
            DependencyProperty.Register("Cart", typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));


        public CartWindow(Cart myCart)
        {
            InitializeComponent();
            Cart = myCart;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cart.CustomerName = Nametxt.Text;
                Cart.CustomerEmail = Emailtxt.Text;
                Cart.CostumerAdress = AddressTxt.Text;
                bl!.Cart.OrderConfirmation(Cart);
                MessageBox.Show("Thank you, your order has been successfully placed!");
                Cart = new Cart { CostumerAdress = "", CustomerName = "", TotalPrice = 0, Items = new List<OrderItem?>(), CustomerEmail = "" };
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                FrameworkElement? framework = sender as FrameworkElement;
                OrderItem? orderItem = (OrderItem?)framework?.DataContext;
                int productId = orderItem!.ProductID;
                int amount= orderItem.Amount;
                Cart=bl!.Cart.UpdateProductAmount(Cart, productId, amount);
                OrderItem oI = new OrderItem()
                {
                    ID=productId,
                    Amount=amount,
                    ProductID=productId,
                    Name=orderItem.Name,
                    Price=orderItem.Price,
                    TotalPrice=Cart.Items!.FirstOrDefault(item=>item!.ID==orderItem.ID)!.TotalPrice,
                };
                Cart.Items![Cart.Items.IndexOf(orderItem)]=oI;
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
            
        }
    }
}
