using BO;
using System;
using System.Linq;
using System.Windows;

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductWindowFinal.xaml
    /// </summary>
    public partial class ProductWindowFinal : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public BO.Product Product { set; get; }
        private Action<ProductForList> action;
        public Array array { set; get; } = Enum.GetValues(typeof(BO.Category));
        public ProductWindowFinal(Action<ProductForList> action)
        {
            //categorycomboBox.DataContext = Enum.GetValues(typeof(BO.Category));
            this.action = action;  
            InitializeComponent();
        }
        public ProductWindowFinal(int id, Action<ProductForList> action)
        {
            this.action = action;
            Product = bl!.Product.GetProductDetails(id);
            InitializeComponent();
            // categorycomboBox.ItemsSource = Enum.GetValues(typeof(BO.Category));
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
                action(bl?.Product.GetListOfProducts(p=>p.ID== product?.ID).FirstOrDefault());
                this.Close();
            }
            catch (BO.InCorrectData ex) { messageBoxResult = MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Information); }
        }
    }
}
