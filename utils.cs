using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Reg;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace wowCVAHK
{
    internal class Utils
    {
        // 捕获指定窗口区域的屏幕截图
        internal static Bitmap CaptureWindow(int x, int y, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(x, y, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);
            }
            return bmp;
        }

        internal static void BitmapToMat(Bitmap bitmap, Mat mat)
        {
            // 将 Bitmap 数据锁定到内存中
            System.Drawing.Imaging.BitmapData bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly,
                bitmap.PixelFormat
            );

            Console.WriteLine(new Mat(bitmapData.Height, bitmapData.Width, DepthType.Cv8U, 3));

            // 使用 EmguCV 构造函数从 Bitmap 数据转换为 Mat
            CvInvoke.Imdecode(new Mat(bitmapData.Height, bitmapData.Width, DepthType.Cv8U, 3), ImreadModes.Color, mat);

            // 解锁 Bitmap 数据
            //bitmap.UnlockBits(bitmapData);
        }

        internal static void EnterSelectionMode()
        {
            // 置顶窗口
            IniFile ini = new IniFile(Constant.INI_FILE_PATH);
            string windowId = ini.Get("windowHandle");  // 获取窗口 ID
            if (int.TryParse(windowId, out int intWindowId))
            {
                IntPtr hWnd = new IntPtr(intWindowId); // 窗口句柄

                IntPtr foregroundWindow = BaseAPI.GetForegroundWindow();
                if (hWnd == foregroundWindow)
                {
                    Console.WriteLine("该窗口在最前面");
                }
                else
                {
                    bool showWindowResult = BaseAPI.ShowWindow(hWnd, Constant.SW_SHOWMAXIMIZED);
                    bool result = BaseAPI.SetForegroundWindow(hWnd);
                }

                // 显示全屏半透明窗口
                FullScreenOverlay overlay = new FullScreenOverlay();
                overlay.ShowDialog(); // 进入获取模式
            }
            else
            {
                MessageBox.Show("窗口未绑定", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal static Point ChangeCoordinate(Point Position)
        {
            IniFile ini = new IniFile(Constant.INI_FILE_PATH);
            string windowId = ini.Get("windowHandle");  // 获取窗口 ID
            if (int.TryParse(windowId, out int intWindowId))
            {
                IntPtr hWnd = new IntPtr(intWindowId); // 窗口句柄                                                       
                BaseAPI.ScreenToClient(hWnd, ref Position); // 将屏幕坐标转换为窗口内的坐标
            }
            else
            {
                Console.WriteLine("转换失败");
                MessageBox.Show("坐标绑定失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return Position;
        }

        internal static float GetScalingFactor()
        {
            IntPtr hdc = BaseAPI.GetDC(IntPtr.Zero);
            int dpiX = BaseAPI.GetDeviceCaps(hdc, Constant.LOGPIXELSX);
            BaseAPI.ReleaseDC(IntPtr.Zero, hdc);
            Console.WriteLine($"DPIX = {dpiX}");

            // 假设标准 DPI 为 96
            return dpiX / 96f;
        }

        internal static Size DESKTOP
        {
            get
            {
                IntPtr hdc = BaseAPI.GetDC(IntPtr.Zero);
                Size size = new Size();
                size.Height = BaseAPI.GetDeviceCaps(hdc, 117);
                size.Width = BaseAPI.GetDeviceCaps(hdc, 118);
                BaseAPI.ReleaseDC(IntPtr.Zero, hdc);
                return size;
            }
        }

        //获取屏幕显示大小
        Rectangle tScreenRect = Screen.PrimaryScreen.Bounds;

        internal static Bitmap GetScreenImage()
        {
            //获取屏幕显示大小
            Rectangle viewRect = Screen.PrimaryScreen.Bounds;
            Size phisicalRect = DESKTOP;
            Bitmap tSrcBmp = new Bitmap(phisicalRect.Width, phisicalRect.Height); // 用于屏幕原始图片保存
            Graphics gp = Graphics.FromImage(tSrcBmp);
            gp.CopyFromScreen(0, 0, 0, 0, phisicalRect);

            if (viewRect.Size != phisicalRect)
            {
                //当两者不相同时进行缩放
                Bitmap tSrcBmp2 = new Bitmap(viewRect.Width, viewRect.Height);
                Graphics gp2 = Graphics.FromImage(tSrcBmp2);
                gp2.DrawImage(tSrcBmp, new Rectangle(0, 0, viewRect.Width, viewRect.Height), 0, 0, phisicalRect.Width, phisicalRect.Height, GraphicsUnit.Pixel);
                return tSrcBmp2 as Bitmap;
            }
            else
            {
                return tSrcBmp;
            }

        }

        internal static Color GetColorAt(Point location)
        {
            Bitmap screenImage = GetScreenImage();


            // 检查给定坐标是否在图像范围内
            if (location.X >= 0 && location.X < screenImage.Width && location.Y >= 0 && location.Y < screenImage.Height)
            {
                // 从返回的图像中获取指定位置的颜色
                return screenImage.GetPixel(location.X, location.Y);
            }
            else
            {
                throw new ArgumentOutOfRangeException("location", "指定的坐标不在图像范围内");
            }
        }

        internal static void RgbToHsv(Color color, out float hue, out float saturation, out float value)
        {
            hue = color.GetHue();
            saturation = color.GetSaturation();
            value = color.GetBrightness();
        }

        internal static bool IsHsvWithinTolerance(float currentHue, float currentSaturation, float currentValue, float initialHue, float initialSaturation, float initialValue)
        {
            return Math.Abs(currentHue - initialHue) <= Constant.HUETOLERANCE &&
                   Math.Abs(currentSaturation - initialSaturation) <= Constant.SATURATIONTOLERANCE &&
                   Math.Abs(currentValue - initialValue) <= Constant.VALUETOLERANCE;
        }

        internal static bool ValidateHSV(string hsv)
        {
            //验证RGB的格式是否正确
            string[] parts = hsv.Split(',');

            if (parts.Length != 3) return false;

            foreach (string part in parts)
            {
                if (!float.TryParse(part, out float value))
                {
                    return false;
                }
            }
            return true;
        }

        internal static Dictionary<string, object> loadingConfig()
        {
            // 加载配置文件
            IniFile ini = new IniFile(Constant.INI_FILE_PATH);

            return new Dictionary<string, object>
            {
                { "windowHandle" , ini.Get("windowHandle")}, // 获取窗口 ID 
                { "coordX" , int.Parse(ini.Get("coordinate").Split(',')[0])}, // 窗口坐标X轴
                { "coordY" , int.Parse(ini.Get("coordinate").Split(',')[1])}, // 窗口坐标Y轴
                { "arduinoPort" , ini.Get("deviceCOM")}, // 获取串口号
                { "colorMappings" , ini.GetConfMappings()}, // 读取所有 HSV 与对应结果的映射
            };
        }
    }

    internal class IniFile
    {
        private Dictionary<string, string> data = new Dictionary<string, string>();

        public IniFile(string filePath)
        {
            // 读取或创建文件内容
            if (File.Exists(filePath))
            {
                foreach (var line in File.ReadAllLines(filePath))
                {
                    if (line.Contains("="))
                    {
                        var parts = line.Split('=');
                        data[parts[0].Trim()] = parts[1].Trim();
                    }
                }
            }
        }

        internal string Get(string key)
        {
            return data.ContainsKey(key) ? data[key] : null;
        }

        internal Dictionary<string, string> GetConfMappings()
        {
            Dictionary<string, string> colorMappings = new Dictionary<string, string>();

            foreach (var entry in data)
            {
                colorMappings[entry.Key] = entry.Value;
            }

            return colorMappings;
        }

        internal static void saveIni(string key, string value)
        {

            if (Utils.ValidateHSV(key) && !string.IsNullOrWhiteSpace(value))
            {
                IniFile ini = new IniFile(Constant.INI_FILE_PATH);
                // 读取所有映射
                Dictionary<string, string> colorMappings = ini.GetConfMappings();
                // 保存ini信息
                // 将色块和按键绑定                
                //******************** 校验 ********************
                if (colorMappings.ContainsKey(key))
                {
                    colorMappings.Remove(key);

                }

                var keyToRemove = colorMappings.FirstOrDefault(kvp => kvp.Value == value.ToUpper()).Key;
                if (keyToRemove != null)
                {
                    colorMappings.Remove(keyToRemove);
                }

                colorMappings.Add(key, value.ToUpper());

                WriteIniFile(colorMappings); // 保存到 INI 文件

            }
            else if (key == "windowHandle" || key == "coordinate" || key == "deviceCOM")
            {
                IniFile ini = new IniFile(Constant.INI_FILE_PATH);
                // 读取所有映射
                Dictionary<string, string> colorMappings = ini.GetConfMappings();
                if (colorMappings.ContainsKey(key))
                {
                    colorMappings[key] = value.ToUpper();
                }
                else
                {
                    colorMappings.Add(key, value.ToUpper());
                }

                WriteIniFile(colorMappings); // 保存到 INI 文件
            }
            else
            {
                MessageBox.Show("信息绑定失败", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal static string FindKeyByValue(Dictionary<string, string> iniData, string value)
        {
            // 查重
            foreach (var entry in iniData)
            {
                if (entry.Value == value)
                {
                    return entry.Key;
                }
            }
            return null;
        }

        static void WriteIniFile(Dictionary<string, string> iniData)
        {
            //写入ini
            using (StreamWriter writer = new StreamWriter(Constant.INI_FILE_PATH))
            {
                foreach (var entry in iniData)
                {
                    writer.WriteLine($"{entry.Key} = {entry.Value}");
                }
            }
        }
    }

    internal class FullScreenOverlay : Form
    {
        private Point mousePosition;
        private Bitmap screenCapture;
        public FullScreenOverlay()
        {
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Black;
            this.Opacity = 0.5;  // 设置半透明效果
            this.TopMost = true;

            // 截取整个屏幕
            screenCapture = CaptureScreen();

            this.MouseMove += new MouseEventHandler(OnMouseMove); // 监听鼠标移动事件
            this.MouseDoubleClick += new MouseEventHandler(OnMouseDoubleClick); // 监听双击事件
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            mousePosition = e.Location; // 更新鼠标位置
            this.Invalidate(); // 重新绘制窗口，显示不透明方块
        }

        //重写OnPaint方法，绘制非透明10x10方块
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int rectSize = 25;
            Rectangle rect = new Rectangle(mousePosition.X - rectSize / 2, mousePosition.Y - rectSize / 2, rectSize, rectSize);

            if (screenCapture != null)
            {
                // 只绘制框内部分
                e.Graphics.DrawImage(screenCapture, rect, rect, GraphicsUnit.Pixel);
            }

            // 绘制绿色边框
            using (Pen greenPen = new Pen(Color.Green, 2))
            {
                e.Graphics.DrawRectangle(greenPen, rect); // 画绿色边框
            }
        }

        private Bitmap CaptureScreen()
        {
            return screenCapture = Utils.GetScreenImage();
        }

        // 获取鼠标双击位置和颜色
        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            float initialHue, initialSaturation, initialValue;
            Point clickedPosition = e.Location;

            // 将屏幕坐标转换为窗口内的坐标
            Point ClientLocation = Utils.ChangeCoordinate(clickedPosition);

            IniFile.saveIni("coordinate", $"{clickedPosition.X},{clickedPosition.Y}");


            // 获取屏幕坐标的颜色
            Color clickedColor = Utils.GetColorAt(clickedPosition);
            Utils.RgbToHsv(clickedColor, out initialHue, out initialSaturation, out initialValue);

            // 关闭全屏窗口
            this.Close();

            // 显示坐标和颜色信息到主窗口的文本框
            ControlForm mainForm = Application.OpenForms["ControlForm"] as ControlForm;
            if (mainForm != null)
            {
                mainForm.DisplayClickedColorInfo(initialHue, initialSaturation, initialValue);
                mainForm.DisplayClickedCoordinateInfo(ClientLocation);
            }
        }



    }

    public partial class ControlForm : Form
    {
        public void DisplayClickedColorInfo(float initialHue, float initialSaturation, float initialValue)
        {
            // 在文本框中显示颜色信息
            txtColor.Text = $"{initialHue},{initialSaturation},{initialValue}";
        }

        public void DisplayClickedWindowHandleInfo(string title)
        {
            // 在文本框中显示窗口名称
            txtWindowHandle.Text = $"{title}";
        }

        public void DisplayClickedCoordinateInfo(Point position)
        {
            // 在文本框中显示坐标
            txtColorCoordinate.Text = $"X: {position.X}, Y: {position.Y}";
        }
    }
}
