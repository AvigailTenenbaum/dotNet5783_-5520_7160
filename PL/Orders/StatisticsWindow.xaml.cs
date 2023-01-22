using BlImplementation;
using System.Collections.ObjectModel;
using System.Windows;

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public ObservableCollection<StatisticsOrders> Statistics { get; set; }
        public StatisticsWindow()
        {
            Statistics = new ObservableCollection<StatisticsOrders>(bl!.Order.GetStatisticsOrders());
            InitializeComponent();
        }
    }
}
