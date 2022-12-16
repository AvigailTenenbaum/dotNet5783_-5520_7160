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
        BlApi.IBl? bl = BlApi.Factory.Get();
        public ProductWindow()
        {
            InitializeComponent();
        }
        public ProductWindow(BlApi.IBl? bl)
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
        /// <summary>
        /// Button to try adding a product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                messageBoxResult= MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK,MessageBoxImage.Information);
            }
            catch (BO.AllReadyExist ex) { messageBoxResult = MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Information); }
        }
        /// <summary>
        /// Button to try product update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            catch(BO.InCorrectData ex) { messageBoxResult = MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Information); }
           
        }
        /// <summary>
        /// Typing option for normal input only
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void idTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;

            if (text == null) return;

            if (e == null) return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

            //allow control system keys

            if (Char.IsControl(c)) return;

            //allow digits (without Shift or Alt)

            if (Char.IsDigit(c))

                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))

                    return;


            e.Handled = true;

            return;
        }
        /// <summary>
        /// Typing option for normal input only
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void priceTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;

            if (text == null) return;

            if (e == null) return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);



            if (Char.IsControl(c)) return;



            if (Char.IsDigit(c))

                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))

                    return;

            e.Handled = true;



            return;

        }
        /// <summary>
        /// Typing option for normal input only
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inStockTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;

            if (text == null) return;

            if (e == null) return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

            //allow control system keys

            if (Char.IsControl(c)) return;


            if (Char.IsDigit(c))

                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))

                    return;



            e.Handled = true;



            return;

        }
    }
}
