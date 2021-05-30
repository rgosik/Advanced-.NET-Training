using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsSamples
{
    internal class ProgressStatus
    {
        public int Current { get; set; }
        public int Total { get; set; }      
    }

    public partial class BackgroundWorkerSample : Form
    {
        public BackgroundWorkerSample()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Can't change GUI from async method
            //txtOutput.Text = $"DoWork ThreadId{Thread.CurrentThread.ManagedThreadId}" + Environment.NewLine;

            Action action = () => { txtOutput.Text = "BackgroundWorker started" + Environment.NewLine; };

            txtOutput.Invoke(action);

            var progressStatus = (ProgressStatus)e.Argument;

            for (int i = 1; i <= progressStatus.Total; i++)
            {
                LongOperation();
                var progress = (i * 100) /(double)progressStatus.Total;
                progressStatus.Current = i;
                backgroundWorker1.ReportProgress((int)progress, progressStatus);
            }
        }

        private void BackgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            var progress = e.ProgressPercentage;
            var ps = (ProgressStatus)e.UserState; 
            txtOutput.Text += 
                $"{ps.Current} out of {ps.Total}. {progress}% ThreadId {Thread.CurrentThread.ManagedThreadId}" 
                + Environment.NewLine;
        }

        private void BackgroundWorker1_OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnStart.Text = "Start";
            btnStart.Enabled = true;
        }

        private void btnStart_Click(object sender, System.EventArgs e)
        {
            btnStart.Text = "Running";
            btnStart.Enabled = false;
            backgroundWorker1.RunWorkerAsync(new ProgressStatus(){Current = 0, Total = 6});
        }

        private void LongOperation()
        {
            Thread.Sleep(1000);
        }

    }
}
