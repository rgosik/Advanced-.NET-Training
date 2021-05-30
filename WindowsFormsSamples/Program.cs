using System;
using System.Windows.Forms;

namespace WindowsFormsSamples
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FileControl());
            //Application.Run(new Browser());
            //Application.Run(new BackgroundWorkerSample());
        }
    }
}
