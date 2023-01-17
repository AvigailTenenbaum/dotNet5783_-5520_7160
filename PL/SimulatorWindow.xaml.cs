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

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
  
    public partial class SimulatorWindow : Window
    {
        BackgroundWorker clockWorker;
        DateTime display = DateTime.Now;
        public SimulatorWindow()
        {
            InitializeComponent();
            ShowTime();

            clockWorker =new BackgroundWorker();
            clockWorker.DoWork += ClockWorker_DoWork;
            clockWorker.ProgressChanged += ClockWorker_UpdateDisplay;
            clockWorker.WorkerReportsProgress = true;
            clockWorker.RunWorkerAsync();
        }

        private void ClockWorker_UpdateDisplay(object? sender, ProgressChangedEventArgs e)
        {
           display= display.AddSeconds(e.ProgressPercentage);
            ShowTime();
        }

        private void ClockWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int sec = 1;
            while(true)
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
            Simulator.orderWorker.PrograssChanged += OrderWorker_PrograssChanged;
            Simulator.StartSimulator(); 
        }
    }
}
