using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace wowCVAHK
{
    internal static class Program
    {
        public static Thread runningThread;        
        public static bool stopFlag = false;                
        public static readonly object _lock = new object();
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ControlForm());

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
        }

        static void OnProcessExit(object sender, EventArgs e)
        {
            stopFlag = true;
        }
    }
}
