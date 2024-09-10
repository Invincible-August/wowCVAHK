namespace wowCVAHK
{
    internal class Constant
    {
        internal const int MOD_ALT = 0x0001;
        internal const int MOD_CONTROL = 0x0002;
        internal const int MOD_SHIFT = 0x0004;
        internal const int WM_HOTKEY = 0x0312;

        internal const string INI_FILE_PATH = "config.ini";

        internal const int SW_SHOWMAXIMIZED = 3;

        internal const int LOGPIXELSX = 88; // DPI x轴
        internal const int LOGPIXELSY = 90; // DPI y轴

        // 定义容差值
        internal const float HUETOLERANCE = 15.0f; // 色调容差
        internal const float SATURATIONTOLERANCE = 0.1f; // 饱和度容差
        internal const float VALUETOLERANCE = 0.1f; // 明度容差
    }
}
