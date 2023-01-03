using BO;
using System.Windows;

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductDetailsWindow.xaml
    /// </summary>
    public partial class ProductDetailsWindow : Window
    {
        public ProductItem ProductItem { get; set; }
        public ProductDetailsWindow(ProductItem productItem)
        {
            ProductItem= productItem;
            InitializeComponent();
        }
    }
}
