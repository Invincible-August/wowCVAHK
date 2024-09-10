using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

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

            if (colorInfo == "" || keyInfo == "")
            {
                Console.WriteLine("colorInfo or keyInfo == 空");
                return;
            }
            else
            {
                Console.WriteLine("colorInfo != null");
                Console.WriteLine($"type colorInfo == {colorInfo.GetType()}");
            }
            IniFile.saveIni(colorInfo, keyInfo);
        }

        internal static void running()
        {
            ControlForm.stopFlag = false;
            ControlForm.workerThread = new Thread(new ThreadStart(runningThreadMethod));
            ControlForm.workerThread.Start();            
        }

        internal static void bindCOM(string deviceCOM) 
        {            
            IniFile.saveIni("deviceCOM", deviceCOM.ToUpper());            
        }

        internal static void stop() 
        {
            ControlForm.stopFlag = true;            
        }

        static void runningThreadMethod() 
        {
            // 加载配置文件
            IniFile ini = new IniFile(Constant.INI_FILE_PATH);
            string windowId = ini.Get("windowHandle");  // 获取窗口 ID            
            string[] locationParts = ini.Get("coordinate").Split(',');  // 获取坐标
            int coordX = int.Parse(locationParts[0]);
            int coordY = int.Parse(locationParts[1]);
            string arduinoPort = ini.Get("coordinate");
            // 读取所有 RGB 与对应结果的映射
            Dictionary<string, string> colorMappings = ini.GetConfMappings();

            if (int.TryParse(windowId, out int intWindowId))
            {
                IntPtr hWnd = new IntPtr(intWindowId);  // 假设 ini 中 windowId 是窗口标题
                if (hWnd == IntPtr.Zero)
                {
                    Console.WriteLine("找不到指定窗口。");
                    return;
                }
            }
            else
            {
                MessageBox.Show("窗口未绑定", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Stopwatch stopwatch = new Stopwatch();
            int times = 0;

            while (!ControlForm.stopFlag)
            {
                stopwatch.Start(); // 开始计时 
                bool startCheck = false;

                IntPtr foregroundWindow = BaseAPI.GetForegroundWindow();

                IntPtr hWnd = new IntPtr(intWindowId);

                if (hWnd == foregroundWindow)
                {
                    startCheck = true;
                }
                else
                {

                    startCheck = false;
                }

                if (startCheck)
                {
                    Point checkPosition = new Point(coordX, coordY);


                    Color checkColor = Utils.GetColorAt(checkPosition);

                    string colorText = $"{checkColor.R},{checkColor.G},{checkColor.B}";

                    Console.WriteLine($"colorText = {colorText}");

                    foreach (var colorMapping in colorMappings)
                    {
                        if (colorMapping.Key == colorText)
                        {
                            //执行下发到硬件的控制函数
                            Console.WriteLine(colorMapping.Value);
                            Console.WriteLine($"times = {times+=1}");
                            
                            DeviceControl.SendCommandToArduino(arduinoPort, colorMapping.Value);
                        }
                    }
                }

                stopwatch.Stop(); // 停止计时 
                long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                int delay = 300 - (int)elapsedMilliseconds; // 计算剩余时间以保持至少100毫秒的迭代时间
                if (delay > 0)
                {
                    Thread.Sleep(delay); // 如果实际操作时间小于100毫秒，则等待剩余时间  
                }
                stopwatch.Reset();
            }
        }
    }
}
