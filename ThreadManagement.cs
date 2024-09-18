using Emgu.CV;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using wowCVAHK;

namespace wowCVAHK
{
    internal class ThreadManagement
    {
        private SerialPort _serialPort;
        private BlockingCollection<string> _commandsQueue = new BlockingCollection<string>();
        private Thread _serialThread;
        private string windowHandle;
        private int coordX;
        private int coordY;
        private string arduinoPort;
        private Dictionary<string, string> colorMappings;
        private Dictionary<string, object> config;
        
        private ManualResetEvent _pauseEvent;

        public ThreadManagement()
        {
            config = Utils.loadingConfig();
            // 加载配置文件            
            windowHandle = config["windowHandle"] as string;
            coordX = Convert.ToInt32(config["coordX"]);
            coordY = Convert.ToInt32(config["coordY"]);
            arduinoPort = config["arduinoPort"] as string;
            colorMappings = config["colorMappings"] as Dictionary<string, string>;

            // 初始化串口
            _serialPort = new SerialPort(arduinoPort, 9600, Parity.None, 8, StopBits.One);

            _pauseEvent = new ManualResetEvent(true); // 初始为 true 表示线程可运行

        }

        public void StartDetection()
        {
            // 60FPS 每帧的时间间隔

            Random random = new Random();
            int delayPerFrame = random.Next(20, 60);
            //int delayPerFrame = 1000 / 600;

            if (int.TryParse(windowHandle, out int intwindowHandle))
            {               
                IntPtr hWnd = new IntPtr(intwindowHandle);

                if (hWnd == IntPtr.Zero)
                {
                    MessageBox.Show("窗口绑定失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                int times = 0;                

                while (!Program.stopFlag)
                {
                    _pauseEvent.WaitOne(); // 等待继续信号，暂停时阻塞                    

                    IntPtr foregroundWindow = BaseAPI.GetForegroundWindow();

                    if (hWnd == foregroundWindow)
                    {
                        Program.startCheck = true;
                    }
                    else
                    {
                        Program.startCheck = false;
                    }

                    if (Program.startCheck)
                    {                        
                        float currentHue, currentSaturation, currentValue;
                        Point checkPosition = new Point(coordX, coordY);
                        Color checkColor = Utils.GetColorAt(checkPosition);
                        Utils.RgbToHsv(checkColor, out currentHue, out currentSaturation, out currentValue);

                        foreach (var colorMapping in colorMappings)
                        {
                            if (Utils.ValidateHSV(colorMapping.Key))
                            {                                
                                string[] parts = colorMapping.Key.Split(',');
                                if (Utils.IsHsvWithinTolerance(currentHue, currentSaturation, currentValue, float.Parse(parts[0]), float.Parse(parts[1]), float.Parse(parts[2])))
                                {
                                    string command = colorMapping.Value;

                                    if (times % 10 == 0)
                                    {
                                        LogManager.Log($"command = {command}");
                                        LogManager.Log("执行并获取到颜色色块");
                                    }
                                    else
                                    {
                                        //LogManager.Log($"times = {times}");
                                    }


                                    if (command.Length == 1)
                                    {
                                        command = "0" + command;
                                    }
                                    else 
                                    {
                                        if (command.StartsWith("ALT", StringComparison.OrdinalIgnoreCase))
                                        {
                                            command = 'A'+command.Split('+')[1];
                                        }
                                        else if (command.StartsWith("CTRL", StringComparison.OrdinalIgnoreCase)) 
                                        {
                                            command = 'C' + command.Split('+')[1];
                                        }
                                        else if (command.StartsWith("SHIFT", StringComparison.OrdinalIgnoreCase)) 
                                        {
                                            command = 'S' + command.Split('+')[1];
                                        }
                                    }
                                    SendCommand(command);
                                    //LogManager.Log($"command = {command}");                                    
                                    times += 1;
                                    //LogManager.Log($"times = {times}");                                    
                                }
                                Thread.Sleep(delayPerFrame);  // 控制每秒60帧
                            }
                        }
                    }                    
                }
            }
        }

        private void SendCommand(string command)
        {
            try
            {
                // 从队列中获取需要发送的命令
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }

                // 发送命令                               
                _serialPort.Write(command);                

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally 
            {
                _serialPort.Close();
            }
        }

        public void Pause()
        {
            _pauseEvent.Reset(); // 暂停执行
            Console.WriteLine("检测已暂停");
        }

        public void Resume()
        {
            _pauseEvent.Set(); // 恢复执行
            Console.WriteLine("检测继续");
        }
    }
}
