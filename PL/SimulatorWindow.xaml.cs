using BlApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Simulator;

namespace PL;

    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
  
    public partial class SimulatorWindow : Window
    {
        IBl? bl = Factory.Get();
    bool flag;

        private OrderProcess? orderProcess
        {
            get { return (OrderProcess?)GetValue(orderProcessProperty); }
            set { SetValue(orderProcessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty orderProcessProperty =
            DependencyProperty.Register("orderProcess", typeof(OrderProcess), typeof(SimulatorWindow));



        private BackgroundWorker backgroundWorker;

        private const int c_timeSleep = 1000;

        private Dictionary<int, Action<object?>> _actions;

        public SimulatorWindow()
        {
            DataContext= this;
            InitializeComponent();
            _actions = new Dictionary<int, Action<object?>>();
            flag= false;

            backgroundWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.RunWorkerAsync();
            ShowTime();
            clockWorker = new BackgroundWorker();
            clockWorker.DoWork += ClockWorker_DoWork;
            clockWorker.ProgressChanged += ClockWorker_UpdateDisplay;
            clockWorker.WorkerReportsProgress = true;
            clockWorker.RunWorkerAsync();
    }

        private void BackgroundWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
        //backgroundWorker.CancelAsync();
        this.Close();
        }

        private void BackgroundWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            if (_actions.ContainsKey(e.ProgressPercentage))
                _actions[e.ProgressPercentage]?.Invoke(e.UserState);
        }

        private void BackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            Simulator.Simulator.s_StopSimulation += () => backgroundWorker.CancelAsync();

            Simulator.Simulator.s_UpdateSimulation += onProgressChangedClock;

            Simulator.Simulator.StartSimulator();

            addAction(1, updateOrder);

            while (!backgroundWorker.CancellationPending)
            {
                Thread.Sleep(c_timeSleep);
                onProgressChangedClock(0, DateTime.Now);
            }
        }


        private void updateOrder(object? obj)
        {
            if (obj is OrderProcess orderProcess)
                this.orderProcess = orderProcess;
        }

        private void onProgressChangedClock(int progressPercentage, object? userState) =>
            backgroundWorker.ReportProgress(progressPercentage, userState);

        private void addAction(int key, Action<object?> action)
           => _actions.Add(key, action);


    BackgroundWorker clockWorker;
    DateTime display = DateTime.Now;


    private void ClockWorker_UpdateDisplay(object? sender, ProgressChangedEventArgs e)
    {
        display = display.AddSeconds(e.ProgressPercentage);
        ShowTime();
    }

    private void ClockWorker_DoWork(object sender, DoWorkEventArgs e)
    {
        int sec = 1;
        while (true)
        {
            Thread.Sleep(1000);
            clockWorker.ReportProgress(sec);
        }
    }
    private void ShowTime()
    {
        string timerText = display.ToString();
        this.txtBlock.Text = timerText;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    { 
        flag= true;
        Simulator.Simulator.StopSimulator();
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        if (!flag)
        {
            e.Cancel = true;
        }
    }
}

