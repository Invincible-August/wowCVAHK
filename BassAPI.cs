using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace wowCVAHK
{
    internal class BaseAPI
    {
        //获取当前用户前台（正在与用户交互）的窗口句柄
        //返回值为一个 IntPtr 类型的窗口句柄，如果获取失败则返回 IntPtr.Zero
        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        //获取指定窗口的标题文本
        //参数: hWnd：目标窗口的句柄（IntPtr)
        //     text：一个 StringBuilder 对象，用于接收窗口标题文本
        //     count：缓冲区的大小（int），表示能接收的最大字符数
        //返回值：返回实际获取的字符串长度（不包括终止符）
        [DllImport("user32.dll")]
        internal static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        //获取指定窗口的线程 ID 和进程 ID
        //参数：hWnd：目标窗口的句柄（IntPtr
        //     processId：一个 uint 类型的输出参数，用于接收窗口所属进程的 ID
        //返回值：返回创建指定窗口的线程的线程 ID
        [DllImport("user32.dll")]
        internal static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        //注册一个全局热键，指定窗口在热键按下时将收到 WM_HOTKEY 消息
        //参数：hWnd：接收热键消息的窗口句柄（IntPtr），如果为 IntPtr.Zero，则热键为全局
        //     id：热键的标识符（int）
        //     fsModifiers：热键的修饰键（如 Ctrl、Alt 等的组合），int 类型
        //     vk：热键的虚拟键码（如 F1，A，B 等），int 类型
        //返回值：如果成功，返回 true，否则返回 false
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        //注销一个已注册的热键
        //参数：hWnd：接收热键消息的窗口句柄（IntPtr）
        //     id：要注销的热键的标识符（int）
        //返回值：如果成功，返回 true，否则返回 false
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        //获取当前鼠标指针的位置（屏幕坐标）
        //参数：lpPoint：一个 Point 结构体的输出参数，用于接收鼠标的屏幕坐标
        //返回值：如果成功，返回 true，否则返回 false
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(out Point lpPoint);

        //屏幕坐标转换为相对于指定窗口的客户端区域的坐标
        //参数：hWnd：窗口的句柄（IntPtr）lpPoint：一个
        //     Point 结构体，传入屏幕坐标，函数将其转换为客户端坐标
        //返回值：如果成功，返回 true，否则返回 false
        [DllImport("user32.dll")]
        internal static extern bool ScreenToClient(IntPtr hWnd, ref Point lpPoint);

        //查找指定类名和窗口名的窗口句柄。
        //参数：lpClassName：窗口的类名（string），可以为 null
        //     lpWindowName：窗口标题名称（string），可以为 null
        //返回值：返回匹配窗口的句柄（IntPtr），如果没有找到则返回 IntPtr.Zero
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        //获取指定窗口的矩形边界（相对于屏幕的坐标）
        //参数：hWnd：窗口的句柄（IntPtr）
        //     lpRect：一个 RECT 结构体的输出参数，用于接收窗口的矩形坐标
        //返回值：如果成功，返回 true，否则返回 false
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        //将指定的窗口带到前台（最前面）并激活它，使其成为当前与用户交互的窗口,只有一个前台窗口可以与用户进行交互，用户的键盘和鼠标输入将发送到该窗口。
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        // 最大化窗口
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        // 获取DPI 缩放因子
        [DllImport("user32.dll")]
        internal static extern int GetDpiForWindow(IntPtr hWnd);

        // P/Invoke 调用，获取 DPI 缩放比例
        [DllImport("user32.dll")]
        internal static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        internal static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        [DllImport("user32.dll")]
        internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
    }

}
