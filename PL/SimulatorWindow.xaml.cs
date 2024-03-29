﻿using BlApi;
using Simulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

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
    public static readonly DependencyProperty orderProcessProperty =
        DependencyProperty.Register("orderProcess", typeof(OrderProcess), typeof(SimulatorWindow));



    private BackgroundWorker backgroundWorker;

    private const int c_timeSleep = 1000;

    private Dictionary<int, Action<object?>> _actions;

    public SimulatorWindow()
    {
        InitializeComponent();
        _actions = new Dictionary<int, Action<object?>>();
        flag = false;

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
        Simulator.Simulator.s_StopSimulation -= () => backgroundWorker.CancelAsync();

        Simulator.Simulator.s_UpdateSimulation -= onProgressChangedClock;

    }

    private void BackgroundWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        if (_actions.ContainsKey(e.ProgressPercentage))
        {
            _actions[e.ProgressPercentage]?.Invoke(e.UserState);
            progressbar.Value = ((int) orderProcess.CurrentOrder?.Status)*33.3333;
        }
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
        //if (orderProcess != null && orderProcess.EndTreatment != null)
        //{
        //    double duration = Duration(orderProcess?.CurrentTime, orderProcess.EndTreatment);
        //    duration = 100.0 / duration;
        //    progressbar.Value += duration;
        //}
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
        flag = true;
        Simulator.Simulator.StopSimulator();
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        if (!flag)
        {
            e.Cancel = true;
        }
    }
    private int Duration (string startTime,string endTime)
    {
        int startTime_secs = Convert.ToInt32(startTime.Split(':')[0]) * 3600 + Convert.ToInt32(startTime.Split(':')[1]) * 60 + Convert.ToInt32(startTime.Split(':')[2]);
        int endTime_secs = Convert.ToInt32(endTime.Split(':')[0]) * 3600 + Convert.ToInt32(endTime.Split(':')[1]) * 60 + Convert.ToInt32(endTime.Split(':')[2]);
        return endTime_secs - startTime_secs;

    }
}

