using BlApi;
using BlImplementation;
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
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        IBl bl = new Bl();
        public ProductListWindow()
        {
            InitializeComponent();
            ProductListview.ItemsSource = bl.Product.GetListOfProducts();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void ProductListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductListview.ItemsSource = bl.Product.GetListOfProducts(product => product!.Category.Equals(CategorySelector.SelectedItem));
            CategorySelector.Items.Remove(CategorySelector.SelectedItem);
        }
    }
}
