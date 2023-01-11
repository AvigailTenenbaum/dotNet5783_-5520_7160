using BO;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductDetailsWindow.xaml
    /// </summary>
    public partial class ProductDetailsWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        Cart cart;
        public Action<ProductItem, Cart> Action1;

        public BO.ProductItem ProductItem
        {
            get { return (BO.ProductItem)GetValue(ProductItemProperty); }
            set { SetValue(ProductItemProperty, value); }
        }
        public static readonly DependencyProperty ProductItemProperty =
            DependencyProperty.Register("ProductItem", typeof(BO.ProductItem), typeof(Window), new PropertyMetadata(null));
        public ProductDetailsWindow(ProductItem productItem,Cart myCart, Action<ProductItem, Cart> action1)
        {
            this.Action1= action1;
            ProductItem= productItem;
            InitializeComponent();
            this.cart= myCart;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cart = bl!.Cart.AddProductToCart(cart, (int)idLbl.Content);
                ProductItem= bl!.Product.GetProductDetails(ProductItem.ID, cart);
                Action1(ProductItem,cart);
                MessageBox.Show("The item has been successfully added");
            }
            catch(BO.NotExist ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.OutOfStock ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
             cart= bl!.Cart.UpdateProductAmount(cart, (int)idLbl.Content,ProductItem.AmountInCart-1);
                ProductItem = bl!.Product.GetProductDetails(ProductItem.ID, cart);
                Action1(ProductItem, cart);
                MessageBox.Show("The item has been successfully removed");
            }
            catch(BO.NotExist ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.OutOfStock ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(BO.NotPossibleToFillRequest ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
