using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace wowCVAHK
{
    internal class DeviceControl
    {
        //internal static string findCOM() 
        //{
        //    string arduinoPort = FindArduinoPort();
        //    if (!string.IsNullOrEmpty(arduinoPort))
        //    {
        //        Console.WriteLine($"Arduino 设备已找到，端口为: {arduinoPort}");
        //        return arduinoPort;               
        //    }
        //    else 
        //    {
        //        return null;
        //    }
        //}

        // 查找Arduino端口
        //static string FindArduinoPort()
        //{
        //    string[] ports = SerialPort.GetPortNames();  // 获取所有可用的串口端口
        //    foreach (string port in ports)
        //    {
        //        try
        //        {
        //            using (SerialPort serialPort = new SerialPort(port, 9600))
        //            {
        //                serialPort.Open();
        //                serialPort.ReadTimeout = 1000;  // 设置超时时间
        //                Thread.Sleep(2000);  // 给Arduino Leonardo足够时间重置并启动
        //                string response = serialPort.ReadLine();

        //                if (response.Contains("HELLO"))  // 检查Arduino是否响应特定字符串
        //                {
        //                    serialPort.Close();
        //                    return port;  // 找到Arduino设备的端口
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"无法访问端口 {port}: {ex.Message}");
        //        }
        //    }
        //    return null;  // 未找到Arduino设备
        //}
        // 向Arduino发送命令
        internal static void SendCommandToArduino(string port, string command)
        {
            try
            {
                using (SerialPort serialPort = new SerialPort(port, 9600))
                {
                    serialPort.Open();
                    serialPort.WriteLine(command);  // 发送指令给Arduino
                    Console.WriteLine($"已向Arduino发送指令: {command}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发送命令失败: {ex.Message}");
            }
        }
    }
}
