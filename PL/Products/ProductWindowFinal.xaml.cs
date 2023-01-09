using BO;
using DO;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductWindowFinal.xaml
    /// </summary>
    public partial class ProductWindowFinal : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public BO.Product Product { set; get; }
        private Action<ProductForList> action;//Variable for performing actions in the previous window
        private Action action1;//Variable for performing actions in the previous window
        public Array array { set; get; } = Enum.GetValues(typeof(BO.Category));
        /// <summary>
        /// Opening the window in insert mode
        /// </summary>
        /// <param name="action"></param>
        public ProductWindowFinal(Action<ProductForList> action)
        {
            this.action = action;  
            InitializeComponent();
        }
        /// <summary>
        /// Opening the window in update mode
        /// </summary>
        /// <param name="id"></param>
        /// <param name="action"></param>
        /// <param name="action1"></param>
        public ProductWindowFinal(int id, Action<ProductForList> action, Action action1)
        {
            this.action = action;
            this.action1 = action1;
            Product = bl!.Product.GetProductDetails(id);
            InitializeComponent();
        }
        /// <summary>
        /// Adding a new product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            //Checking the correctness of the details
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
                action(bl?.Product.GetListOfProducts(p => p.ID == product?.ID).FirstOrDefault());
                this.Close();
            }
            catch (BO.InCorrectData ex)
            {
                messageBoxResult = MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (BO.AllReadyExist ex) { messageBoxResult = MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Information); }
            catch(Exception ex) { messageBoxResult = MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Information); }
        }
        /// <summary>
        /// Update an existing product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;
            //Checking the correctness of the details

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
        /// <summary>
        /// Deleting an existing product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                bl?.Product.DeleteProduct(Product.ID);
                action1();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void idTextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
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
