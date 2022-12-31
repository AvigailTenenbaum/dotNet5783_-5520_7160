using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
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
    /// Interaction logic for ProductWindowFinal.xaml
    /// </summary>
    public partial class ProductWindowFinal : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public BO.Product Product { set; get; }
  
        public ProductWindowFinal()
        {
            categorycomboBox.DataContext = Enum.GetValues(typeof(BO.Category));
            DataContext = this;
            InitializeComponent();
        }
        public ProductWindowFinal(int id)
        {
            DataContext = this;
            Product = bl!.Product.GetProductDetails(id);
            InitializeComponent();
           categorycomboBox.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            if (idTextBox.Text.Length == 0 || nameTextBox.Text.Length == 0 || inStockTextBox.Text.Length == 0 || priceTextBox.Text.Length == 0)
            {

                messageBoxResult = MessageBox.Show("One or more of the required data is missing", "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
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
                bl?.Product.AddProduct(product);
                this.Close();
            }
            catch (BO.InCorrectData ex)
            {
                messageBoxResult = MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (BO.AllReadyExist ex) { messageBoxResult = MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Information); }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            if (nameTextBox.Text.Length == 0 || inStockTextBox.Text.Length == 0 || priceTextBox.Text.Length == 0)
            {
                messageBoxResult = MessageBox.Show("One or more of the required data is missing", "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
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
                bl?.Product.UpdateProduct(product);
                this.Close();
            }
            catch (BO.InCorrectData ex) { messageBoxResult = MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Information); }
        }
    }
}
