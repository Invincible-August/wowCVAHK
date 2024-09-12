using Emgu.CV;
using Emgu.CV.Aruco;
using Emgu.CV.Ocl;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using wowCVAHK;

namespace wowCVAHK
{
    internal class ButtonControl
    {
        internal static string saveWindowHandle()
        {
            // 获取最上方窗口句柄
            IntPtr handle = BaseAPI.GetForegroundWindow();
            // 获取窗口名称
            StringBuilder windowTitle = new StringBuilder(256);
            BaseAPI.GetWindowText(handle, windowTitle, 256);

            // 绑定窗口句柄
            IniFile.saveIni("windowHandle", handle.ToString());

            return windowTitle.ToString();
        }

        internal static void ColorBindKey(string colorInfo, string keyInfo)
        {
            IniFile.saveIni(colorInfo, keyInfo);
        }

        internal static void running()
        {
            TreadManagement hsvDetection = new TreadManagement();
            Program.runningThread = new Thread(new ThreadStart(hsvDetection.StartDetection));
            Program.runningThread.Start();
        }

            internal static void bindCOM(string deviceCOM) 
        {            
            IniFile.saveIni("deviceCOM", deviceCOM.ToUpper());            
        }

        internal static void stop() 
        {
            Program.stopFlag = true;            
        }      
    }
}
