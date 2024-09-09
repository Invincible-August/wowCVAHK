using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
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
            Console.WriteLine("开始调试");
            // 加载配置文件
            IniFile ini = new IniFile(Constant.INI_FILE_PATH);
            string windowId = ini.Get("windowHandle");  // 获取窗口 ID
            Console.WriteLine($"windowId = {windowId}");
            string[] locationParts = ini.Get("coordinate").Split(',');  // 获取坐标
            int coordX = int.Parse(locationParts[0]);
            int coordY = int.Parse(locationParts[1]);
            // 读取所有 RGB 与对应结果的映射
            Dictionary<string, string> colorMappings = ini.GetConfMappings();

            // 绑定窗口
            // 查找指定窗口
            if (int.TryParse(windowId, out int intWindowId)) 
            {
                IntPtr hWnd = new IntPtr(intWindowId);  // 假设 ini 中 windowId 是窗口标题
                if (hWnd == IntPtr.Zero)
                {
                    Console.WriteLine("找不到指定窗口。");
                    return;
                }
                IntPtr foregroundWindow = BaseAPI.GetForegroundWindow();

                Console.WriteLine($"hWnd ={hWnd}， foregroundWindow={foregroundWindow}");
                if (hWnd == foregroundWindow)
                {
                    Console.WriteLine("该窗口在最前面");
                }
                else
                {
                    bool showWindowResult = BaseAPI.ShowWindow(hWnd, Constant.SW_SHOWMAXIMIZED);
                    bool result = BaseAPI.SetForegroundWindow(hWnd);
                }


                //获取窗口坐标
                BaseAPI.GetWindowRect(hWnd, out BaseAPI.RECT rect);
                Console.WriteLine($"rect.Left={rect.Left},rect.Top={rect.Top},rect.Right - rect.Left={rect.Right - rect.Left},rect.Bottom - rect.Top={rect.Bottom - rect.Top}");

                //捕获窗口的屏幕截图并转换为 Mat 格式
                Bitmap screenshot = Utils.CaptureWindow(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
                Mat matImage = new Mat();
                Utils.BitmapToMat(screenshot, matImage);

                // 将 Mat 转换为 EmguCV 的 Image<Bgr, Byte>
                //Image<Bgr, Byte> img = matImage.ToImage<Bgr, Byte>();

                //// *************************************************************************
                //// TODO 获取到的图片先进行测试，测试代码，测试通过后需要删除
                //Bitmap bitmap = img.ToBitmap();
                //string filePath = "screenshot.jpg";
                //bitmap.Save(filePath);
                //Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                // *************************************************************************


                // 进行色块判断
                // 下发输入指令
            }
            else
            {
                MessageBox.Show("窗口未绑定", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
