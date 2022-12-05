using BlApi;
using BlImplementation;
using PL.Products;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            IBl bl = new Bl();
            InitializeComponent();
        }
        /// <summary>
        /// Clicking on the button will open a product list window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnterToSystem_Click(object sender, RoutedEventArgs e) => new ProductListWindow().Show();
        
       
        
    }
}
