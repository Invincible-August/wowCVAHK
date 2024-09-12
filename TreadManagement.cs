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

namespace wowCVAHK
{
    internal class TreadManagement
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

        private bool alreadyACK = true;
        private AutoResetEvent _ackReceivedEvent = new AutoResetEvent(false); // 用于同步ACK确认

        public TreadManagement()
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

        }

        public void StartDetection()
        {
            Console.WriteLine("start StartDetection");
            // 60FPS 每帧的时间间隔
            int delayPerFrame = 1000 / 60;
            if (int.TryParse(windowHandle, out int intwindowHandle))
            {
                IntPtr hWnd = new IntPtr(intwindowHandle);
                if (hWnd == IntPtr.Zero)
                {
                    Console.WriteLine("找不到指定窗口。");
                }

                int times = 0;

                while (!Program.stopFlag)
                {
                    bool startCheck = false;

                    IntPtr foregroundWindow = BaseAPI.GetForegroundWindow();


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
                        float currentHue, currentSaturation, currentValue;
                        Point checkPosition = new Point(coordX, coordY);
                        Color checkColor = Utils.GetColorAt(checkPosition);
                        Utils.RgbToHsv(checkColor, out currentHue, out currentSaturation, out currentValue);

                        foreach (var colorMapping in colorMappings)
                        {
                            if (Utils.ValidateHSV(colorMapping.Key))
                            {
                                Console.WriteLine("Utils.ValidateHSV(colorMapping.Key) = true");
                                string[] parts = colorMapping.Key.Split(',');
                                if (Utils.IsHsvWithinTolerance(currentHue, currentSaturation, currentValue, float.Parse(parts[0]), float.Parse(parts[1]), float.Parse(parts[2])))
                                {
                                    string command = colorMapping.Value; // 根据你的实际逻辑生成命令
                                    SendCommand(command);
                                    Console.WriteLine($"command = {command}");
                                    times += 1;
                                    Console.WriteLine($"times = {times}");
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
            Console.WriteLine("进入函数SendCommand");
            try
            {
                // 从队列中获取需要发送的命令
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }

                Console.WriteLine("端口已打开");
                // 发送命令                               
                _serialPort.Write(command);
                Console.WriteLine($"Sent Command: {command}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
