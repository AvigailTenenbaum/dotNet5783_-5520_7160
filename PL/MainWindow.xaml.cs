using PL.Orders;
using PL.Products;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BlApi.IBl? bl = BlApi.Factory.Get();

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new CatalogWindow().ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new AdminWindow().ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                 bl!.Order.OrderTracking(Convert.ToInt32(idtxt.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            new OrderTrackWindow(Convert.ToInt32( idtxt.Text)).ShowDialog();
            idtxt.Clear();
        }

        private void idtxt_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
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

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            new SimulatorWindow().Show();
         }
    }
}
