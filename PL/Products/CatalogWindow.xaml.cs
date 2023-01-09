using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public Array array { get; set; } = Enum.GetValues(typeof(Category));

        public BO.Cart Cart1
        {
            get { return (BO.Cart)GetValue(Cart1Property); }
            set { SetValue(Cart1Property, value); }
        }
        public static readonly DependencyProperty Cart1Property =
            DependencyProperty.Register("Cart1", typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));
        public ObservableCollection<ProductItem?> ProductsItemList { get; set; }//List of all products in the display
        private IEnumerable<ProductItem?> productsItemList { get; }//Private product list for filtering changes

        ICollectionView collectionView;

        PropertyGroupDescription propertyGroupDescription;

        public CatalogWindow()
        {
            productsItemList = bl!.Product.GetListOfProductsItem();
            ProductsItemList = new ObservableCollection<ProductItem?>(productsItemList);


            collectionView = CollectionViewSource.GetDefaultView(ProductsItemList);
            propertyGroupDescription = new PropertyGroupDescription("Category");
            //collectionView.GroupDescriptions.Add(propertyGroupDescription);

            Cart1 = new Cart { CostumerAdress = "", CustomerName = "", TotalPrice = 0, Items = new List<OrderItem?>(), CustomerEmail = "" };
          
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
        /// <summary>
        /// Private function for updating the list after changes made in category filtering
        /// </summary>
        /// <param name="productsItems"></param>
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
            ListView list=sender as ListView;
            if (list.SelectedItem == null) return;
            new ProductDetailsWindow(((ProductItem)list.SelectedItem),Cart1,UpdateProduct).ShowDialog();
            ProductsListView.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow(Cart1,DelCart, UpdateProduct).ShowDialog();
            ProductsListView.Items.Refresh();
        }
        /// <summary>
        /// Private function for deleting the basket after placing the order in the next window
        /// </summary>
        private void DelCart()
        {
            this.Cart1 = new Cart { CostumerAdress = "", CustomerName = "", TotalPrice = 0, Items = new List<OrderItem?>(), CustomerEmail = "" };
            var productsI = bl!.Product.GetListOfProductsItem().ToList();
            addProductsItem(productsI);
        }
        private void UpdateProduct(ProductItem product,Cart c)
        {
            var p = ProductsItemList?.FirstOrDefault(item => item?.ID == product.ID);
            int i = ProductsItemList.IndexOf(p);
            ProductsItemList[i].AmountInCart = product.AmountInCart;
            this.Cart1 = c;
        }

        private void IsGroupByCategory_Checked(object sender, RoutedEventArgs e)
        {
          
                collectionView.GroupDescriptions.Add(propertyGroupDescription);
            
            

        }

        private void IsGroupByCategory_Unchecked(object sender, RoutedEventArgs e)
        {
            collectionView.GroupDescriptions.Remove(propertyGroupDescription);

        }
    }
}
