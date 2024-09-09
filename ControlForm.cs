using System;
using System.Windows.Forms;

namespace wowCVAHK
{
    public partial class ControlForm : Form
    {
        public ControlForm()
        {
            InitializeComponent();
            BaseAPI.RegisterHotKey(this.Handle, 1, Constant.MOD_ALT | Constant.MOD_CONTROL | Constant.MOD_SHIFT, (int)Keys.F); //注册快捷键
            BaseAPI.RegisterHotKey(this.Handle, 2, Constant.MOD_ALT | Constant.MOD_CONTROL | Constant.MOD_SHIFT, (int)Keys.G);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constant.WM_HOTKEY)
            {
                int hotkeyId = m.WParam.ToInt32();
                switch (hotkeyId)
                {
                    case 1:
                        // 热键 Shift+Ctrl+Alt+F 触发，调用获取窗口信息函数
                        btnSaveBaseInfo.PerformClick();
                        break;
                    case 2:
                        // 热键 Shift+Ctrl+Alt+G 触发，调用获取窗口信息函数
                        btnGetColor.PerformClick();
                        break;
                }
            }
            base.WndProc(ref m);
        }

        private void btnGetColor_Click(object sender, EventArgs e)
        {
            Utils.EnterSelectionMode();
        }

        private void btnBindKey_Click(object sender, EventArgs e)
        {
            string colorInfo = txtColor.Text;
            string keyInfo = txtBindKey.Text;
            ButtonControl.ColorBindKey(colorInfo, keyInfo);
        }

        private void btnSaveBaseInfo_Click(object sender, EventArgs e)
        {
            string handleTitle = ButtonControl.saveWindowHandle();
            ControlForm mainForm = Application.OpenForms["ControlForm"] as ControlForm;
            if (mainForm != null)
            {
                mainForm.DisplayClickedWindowHandleInfo(handleTitle);
            }
        }

        private void stratRunning_Click(object sender, EventArgs e)
        {
            ButtonControl.running();
        }
    }
}
