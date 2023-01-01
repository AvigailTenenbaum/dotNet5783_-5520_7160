using PL.Orders;
using PL.Products;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new OrderListWindow().ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new ProductListWindowFinal().ShowDialog();
        }
    }
}
