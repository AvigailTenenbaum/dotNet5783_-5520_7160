using BO;
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
        public ProductItem ProductItem { get; set; }
        public ProductDetailsWindow(ProductItem productItem,Cart myCart)
        {
            ProductItem= productItem;
            InitializeComponent();
            this.cart= myCart;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cart = bl!.Cart.AddProductToCart(cart, (int)idLbl.Content);
                ProductItem.AmountInCart++;
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
                ProductItem.AmountInCart--;
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
