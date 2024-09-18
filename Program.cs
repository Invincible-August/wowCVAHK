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
        public static bool startCheck = false;
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
            Thread.Sleep(15000);
            runningThread.Abort(); // 终止线程（不推荐使用Abort，更好的方法是设置一个标志位让线程有序退出）
            runningThread.Join();  // 等待线程终止
            Console.WriteLine("后台线程已关闭");
        }
    }
}