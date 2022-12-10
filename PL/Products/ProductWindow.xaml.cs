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
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        IBl bl = new Bl();
        public ProductWindow()
        {
            InitializeComponent();
        }
        public ProductWindow(IBl bl)
        {
            InitializeComponent();
            categorycomboBox.ItemsSource = Enum.GetValues(typeof(BO.Category));
            updateButton.Visibility=Visibility.Collapsed;
        }
        public ProductWindow(int id)
        {
            InitializeComponent();
            categorycomboBox.ItemsSource = Enum.GetValues(typeof(BO.Category));
            addButton.Visibility = Visibility.Collapsed;
            BO.Product product=bl.Product.GetProductDetails(id);
            idTextBox.Text =Convert.ToString(product.ID);
            idTextBox.IsReadOnly= true;
            nameTextBox.Text = product.Name;
            categorycomboBox.SelectedItem= product.Category;
            priceTextBox.Text = Convert.ToString(product.Price);
            inStockTextBox.Text = Convert.ToString(product.InStock);
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            try
            {
                BO.Product product = new BO.Product()
                {
                ID = int.Parse(idTextBox.Text),
                Name = nameTextBox.Text,
                InStock = int.Parse(inStockTextBox.Text),
                Category = (BO.Category)categorycomboBox.SelectedItem,
                Price = double.Parse(priceTextBox.Text),
                };
             bl.Product.AddProduct(product);
            this.Close();
            }
            catch (BO.InCorrectData ex)
            {
                messageBoxResult= MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK,MessageBoxImage.Information);
            }
            catch (BO.AllReadyExist ex) { messageBoxResult = MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Information); }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            try
            {
                BO.Product product = new BO.Product()
                {
                    ID = int.Parse(idTextBox.Text),
                    Name = nameTextBox.Text,
                    InStock = int.Parse(inStockTextBox.Text),
                    Category = (BO.Category)categorycomboBox.SelectedItem,
                    Price = double.Parse(priceTextBox.Text),
                };
                bl.Product.UpdateProduct(product);
                this.Close();
            }
            catch(BO.InCorrectData ex) { messageBoxResult = MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Information); }
           
        }
    }
}
