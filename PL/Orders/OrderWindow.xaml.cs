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

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        BlApi.IBl? _bl;
        public BO.Order Order { set; get; }
        public OrderWindow(int id, BlApi.IBl? bl)
        {
            DataContext = this;
            InitializeComponent();
            _bl = bl;
            Order=_bl!.Order.GetOrderDetails(id);

        }
    }
}
