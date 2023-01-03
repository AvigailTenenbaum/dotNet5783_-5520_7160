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

        public Cart cart { get; set; }
        public ObservableCollection<ProductItem?> ProductsItemList { get; set; }
        private IEnumerable<ProductItem?> productsItemList { get; }
        public CatalogWindow()
        {
            productsItemList = bl!.Product.GetListOfProductsItem();
            ProductsItemList = new ObservableCollection<ProductItem?>(bl!.Product.GetListOfProductsItem());
            InitializeComponent();
        }
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Category? category = CategorySelector.SelectedItem as Category?;
            if (category != null)
            {
                if (category.Equals(Category.NONE))//Back to the state where you see the whole list
                {
                    ProductsItemList = new ObservableCollection<ProductItem?>(bl!.Product.GetListOfProductsItem());
                }
                else
                {
                    ProductsItemList = new ObservableCollection<ProductItem?>(bl!.Product.GetListOfProductsItem().Where(x => x!.Category == (BO.Category)CategorySelector.SelectedItem));
                }
            }
        }
    }
}
