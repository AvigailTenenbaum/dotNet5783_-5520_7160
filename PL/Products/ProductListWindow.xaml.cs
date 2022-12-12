using BlApi;
using BlImplementation;
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
public enum Category
{
   saturday, holidays, toSeferTorah, giftsForHome, handMade, NONE
};
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
            CategorySelector.ItemsSource = Enum.GetValues(typeof(Category));
            CategorySelector.SelectedItem = Category.NONE;
           
        }

        private void ProductListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (CategorySelector.SelectedItem.Equals(Category.NONE))
            {
                ProductListview.ItemsSource = bl.Product.GetListOfProducts();
            }
            else
            {
                ProductListview.ItemsSource = bl.Product.GetListOfProducts(product => product!.Category==(BO.Category) CategorySelector.SelectedItem);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow(bl).ShowDialog();
            ProductListview.ItemsSource= bl.Product.GetListOfProducts();
        }
        private void ProductListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(ProductListview.SelectedItem==null) return;
            new ProductWindow(((ProductForList)ProductListview.SelectedItem).ID).ShowDialog();
            if(CategorySelector.SelectedItem.Equals(Category.NONE))
                ProductListview.ItemsSource = bl.Product.GetListOfProducts();
            else
                ProductListview.ItemsSource = bl.Product.GetListOfProducts(product => product!.Category == (BO.Category)CategorySelector.SelectedItem);

        }


    }
}
