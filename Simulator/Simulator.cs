using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Simulator;

internal static class Simulator
{
    BlApi.IBl? bl = BlApi.Factory.Get();
    public static BackgroundWorker orderWorker = new BackgroundWorker;
    static List<BO.Order> orders =;//נקסל את רשימת הזמנות

    public static void StartSimulation()
    {
        orderWorker.ProgressChanged += OrderWorker_ProgressChanged;
        orderWorker.WorkerReportsProgress = true;
        orderWorker.WorkerSupportsCancellation = true;
        orderWorker.RunWorkerAsync();
    }



    private static void OrderWorker_DoWork(object sender, DoWorkEventArgs e)
    {
        while (!orderWorker.CancellationPending)
        {
            Thread.Sleep(1000);//צריך להרגיל מספר שניות
                               //לבדוק לפי הסטטוס ולעדכן תאריך בהתאם
        }
    }

    public static void StoptSimulation()
    {

    }
    private static void NextOrder()
    {
        BO.Order ord = orders.OrderBy(order => (DateTime.Now -).Ticks).FirstOrDefault();
        //עדכון תאריך השילוח
        //מחיקה מהרשימה והוספת חדש
        orderWorker.ReportProgress(1, ord);
    }
}

