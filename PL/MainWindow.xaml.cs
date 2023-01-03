using PL.Orders;
using PL.Products;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            BlApi.IBl? bl = BlApi.Factory.Get();
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new CatalogWindow().ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new AdminWindow().ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new OrderTrackWindow(Convert.ToInt32( idtxt.Text)).ShowDialog();
        }
    }
}
