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
        public Action Action;
        public Action<ProductItem,Cart> Action1;

        public CartWindow(Cart myCart,Action action, Action<ProductItem,Cart> action1)
        {
            InitializeComponent();
            Cart = myCart;
            this.Action = action;
            this.Action1 = action1;
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
                Action();
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Changing the quantity of a product in the cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement? framework = sender as FrameworkElement;
                OrderItem? orderItem = (OrderItem?)framework?.DataContext;
                int productId = orderItem!.ProductID;
                int amount= orderItem.Amount;
                Cart=bl!.Cart.UpdateProductAmount(Cart, productId, amount);
                ProductsInCart.Items.Refresh();
                ProductItem p = new ProductItem()
                {
                    ID = productId,
                    AmountInCart = amount,

                };
                Action1(p,Cart);
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
            
        }
    }
}
