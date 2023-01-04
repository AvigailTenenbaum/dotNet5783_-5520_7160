using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


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
        public ObservableCollection<ProductForList?> ProductsForLists { get; set; }

        private IEnumerable<ProductForList?> productsForLists { get; }
        public Array array { get; set; } = Enum.GetValues(typeof(Category));
        public ProductListWindowFinal()
        {

            productsForLists = bl!.Product.GetListOfProducts();
            ProductsForLists = new ObservableCollection<ProductForList?>(productsForLists);

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindowFinal(addProduct).ShowDialog();
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Category? category = CategorySelector.SelectedItem as Category?;
            if (category != null)
            {

                if (category.Equals(Category.NONE))//Back to the state where you see the whole list
                {
                    var products = bl!.Product.GetListOfProducts().ToList();
                    addProducts(products);

                }
                else
                {
                    var products = bl!.Product.GetListOfProductsByCondition(productsForLists, product => product.Category == (BO.Category)category).ToList();
                    addProducts(products);
                }
            }

        }

        private void addProducts(IEnumerable<ProductForList> products)
        {
            if (products.Any())
            {
                ProductsForLists.Clear();
                foreach (var item in products)
                {
                    ProductsForLists.Add(item);
                }
            }
        }

        private void ProductListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ProductListview.SelectedItem == null) return;
            new ProductWindowFinal(((ProductForList)ProductListview.SelectedItem).ID,UpdateProduct).ShowDialog();
        }
        private void addProduct(ProductForList product) =>ProductsForLists?.Add(product);
        private void UpdateProduct(ProductForList product)
        {
            var p = ProductsForLists?.FirstOrDefault(item => item?.ID == product.ID);
            int i = ProductsForLists.IndexOf(p);
            ProductsForLists[i]= product;
        }
    }

}
