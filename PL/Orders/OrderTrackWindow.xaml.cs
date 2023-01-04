﻿using System;
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
    /// Interaction logic for OrderTrackWindow.xaml
    /// </summary>
    public partial class OrderTrackWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        public BO.OrderTracking? Order
        {
            get { return (BO.OrderTracking?)GetValue(OrderProperty); }
            set { SetValue(OrderProperty, value); }
        }
        public int ID;
        public static readonly DependencyProperty OrderProperty =
            DependencyProperty.Register("Order", typeof(BO.OrderTracking), typeof(Window), new PropertyMetadata(null));
        public OrderTrackWindow(int id)
        {
           
            InitializeComponent();
            this.ID = id;
            try
            {
                Order = bl.Order.OrderTracking(id);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Order order = bl!.Order.GetOrderDetails(ID);
                new OrderWindow(order).ShowDialog();
            }
            catch (BO.NotExist ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
