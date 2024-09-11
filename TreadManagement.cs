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
        //internal static void detectionBlock()
        //{
        //    SerialPort serialPort = null;
        //    string inputKey = null;
        //    try 
        //    {
        //        Dictionary<string, object> config = Utils.loadingConfig();
        //        // 加载配置文件            
        //        string windowHandle = config["windowHandle"] as string;
        //        int coordX = Convert.ToInt32(config["coordX"]);
        //        int coordY = Convert.ToInt32(config["coordY"]);
        //        string arduinoPort = config["arduinoPort"] as string;
        //        Dictionary<string, string> colorMappings = config["colorMappings"] as Dictionary<string, string>;


        //        Console.WriteLine($"inputKey = {inputKey}");

        //        serialPort = new SerialPort(arduinoPort, 9600);
        //        serialPort.WriteTimeout = 500;
        //        serialPort.Handshake = Handshake.None;

        //        // 配置窗口
        //        if (int.TryParse(windowHandle, out int intwindowHandle))
        //        {
        //            IntPtr hWnd = new IntPtr(intwindowHandle);
        //            if (hWnd == IntPtr.Zero)
        //            {
        //                Console.WriteLine("找不到指定窗口。");
        //                return;
        //            }

        //            int times = 0;
        //            while (!Program.stopFlag)
        //            {
        //                bool startCheck = false;

        //                IntPtr foregroundWindow = BaseAPI.GetForegroundWindow();

        //                if (hWnd == foregroundWindow)
        //                {
        //                    startCheck = true;
        //                }
        //                else
        //                {
        //                    startCheck = false;
        //                }

        //                if (startCheck)
        //                {
        //                    float currentHue, currentSaturation, currentValue;
        //                    Point checkPosition = new Point(coordX, coordY);
        //                    Color checkColor = Utils.GetColorAt(checkPosition);
        //                    Utils.RgbToHsv(checkColor, out currentHue, out currentSaturation, out currentValue);

        //                    foreach (var colorMapping in colorMappings)
        //                    {
        //                        if (Utils.ValidateHSV(colorMapping.Key))
        //                        {
        //                            string[] parts = colorMapping.Key.Split(',');
        //                            if (Utils.IsHsvWithinTolerance(currentHue, currentSaturation, currentValue, float.Parse(parts[0]), float.Parse(parts[1]), float.Parse(parts[2])))
        //                            {
        //                                Console.WriteLine("判断通过");
        //                                inputKey = colorMapping.Value;
        //                            }
        //                        }
        //                    }
        //                    if (inputKey != null)
        //                    {                                                                
        //                        Console.WriteLine("获取serialPort初始化");
        //                        serialPort.Open();
        //                        if (serialPort.IsOpen)
        //                        {
        //                            Console.WriteLine("获取serialPort.open执行成功");
        //                            serialPort.Write(inputKey);  // 发送指令给Arduino
        //                            Console.WriteLine($"已向Arduino发送指令: {inputKey}");
        //                            times += 1;
        //                            Console.WriteLine($"times: {times}");
        //                        }
        //                        else
        //                        {
        //                            Console.WriteLine("串口未打开");
        //                        }

        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("窗口未绑定", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"发送命令失败: {ex.Message}");
        //    }
        //    finally 
        //    {
        //        // 确保在程序结束时关闭串口
        //        if (serialPort != null && serialPort.IsOpen)
        //        {
        //            serialPort.Close();
        //            inputKey = null;                    
        //            Thread.Sleep(100);                    
        //        }
        //    }                               
        //}

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
                                string[] parts = colorMapping.Key.Split(',');
                                if (Utils.IsHsvWithinTolerance(currentHue, currentSaturation, currentValue, float.Parse(parts[0]), float.Parse(parts[1]), float.Parse(parts[2])))
                                {
                                    string command = colorMapping.Value.ToString(); // 根据你的实际逻辑生成命令
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
            try
            {                
                // 从队列中获取需要发送的命令
                if (!_serialPort.IsOpen)
                {                    
                    _serialPort.Open();                    
                }

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
