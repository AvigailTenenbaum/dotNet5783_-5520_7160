using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ProductListWindowFinal.xaml
    /// </summary>

    public enum Category
    {
        saturday, holidays, toSeferTorah, giftsForHome, handMade, NONE
    };

    public partial class ProductListWindowFinal : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
       
        
        public ProductListWindowFinal()
        {
            InitializeComponent();
            ObservableCollection<ProductForList?> productsForLists = new ObservableCollection<ProductForList?>(bl!.Product.GetListOfProducts());
            DataContext = productsForLists;
            CategorySelector.DataContext = Enum.GetValues(typeof(Category));
    }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindowFinal().ShowDialog();
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<ProductForList?> productsForLists;
            if (CategorySelector.SelectedItem.Equals(Category.NONE))//Back to the state where you see the whole list
            {
                productsForLists = new ObservableCollection<ProductForList?>(bl!.Product.GetListOfProducts());
                DataContext = productsForLists;
            }
            else
            {
                productsForLists = new ObservableCollection<ProductForList?>(bl!.Product.GetListOfProducts(x => x!.Category == (BO.Category)CategorySelector.SelectedItem));
                DataContext = productsForLists;
            }
        }

        private void ProductListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ProductListview.SelectedItem == null) return;
            new ProductWindowFinal(((ProductForList)ProductListview.SelectedItem).ID).ShowDialog();
        }
    }

}
