using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public Array array { get; set; } = Enum.GetValues(typeof(Category));

        public Cart Cart { get; set; }
        public ObservableCollection<ProductItem?> ProductsItemList { get; set; }
        private IEnumerable<ProductItem?> productsItemList { get; }
        public ObservableCollection<IGrouping< BO.Category?,ProductItem?>> CategoryG { get; set; }
        public CatalogWindow()
        {
            productsItemList = bl!.Product.GetListOfProductsItem();
            ProductsItemList = new ObservableCollection<ProductItem?>(productsItemList);
            Cart = new Cart { CostumerAdress = "", CustomerName = "", TotalPrice = 0, Items = new List<OrderItem?>(), CustomerEmail = "" };
            CategoryG = new ObservableCollection<IGrouping<BO.Category?, ProductItem?>>
                (from product in ProductsItemList
                 orderby product.Category
                 group product by product.Category into group1
                 select group1);
            InitializeComponent();
        }
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Category? category = CategorySelector.SelectedItem as Category?;
            if (category != null)
            {
                if (category.Equals(Category.NONE))//Back to the state where you see the whole list
                {
                    var productsI = bl!.Product.GetListOfProductsItem().ToList();
                    addProductsItem(productsI);
                }
                else
                {
                    var productsI = bl!.Product.GetListOfProductsItem().Where(product => product!.Category == (BO.Category)category).ToList();
                    addProductsItem(productsI);
                }
            }
        }
        private void addProductsItem(IEnumerable<ProductItem> productsItems)
        {
            if (productsItems.Any())
            {
                ProductsItemList.Clear();
                foreach (var item in productsItems)
                {
                    ProductsItemList.Add(item);
                }
            }
        }

        private void ProductsListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ProductsListView.SelectedItem == null) return;
            new ProductDetailsWindow(((ProductItem)ProductsListView.SelectedItem),Cart).ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow(Cart).ShowDialog();
        }
    }
}
