using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace wowCVAHK
{
    public partial class ControlForm : Form
    {

        public ControlForm()
        {
            InitializeComponent();
            BaseAPI.RegisterHotKey(this.Handle, 1, Constant.MOD_ALT | Constant.MOD_CONTROL | Constant.MOD_SHIFT, (int)Keys.A); //注册快捷键
            BaseAPI.RegisterHotKey(this.Handle, 2, Constant.MOD_ALT | Constant.MOD_CONTROL | Constant.MOD_SHIFT, (int)Keys.S);
            BaseAPI.RegisterHotKey(this.Handle, 3, Constant.MOD_ALT | Constant.MOD_CONTROL | Constant.MOD_SHIFT, (int)Keys.D);
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
                    case 3:
                        // 热键 Shift+Ctrl+Alt+G 触发，调用获取窗口信息函数
                        stopRunning.PerformClick();
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

        private void stopRunning_Click(object sender, EventArgs e)
        {
            ButtonControl.stop();
        }

        private void txtCom_TextChanged(object sender, EventArgs e)
        {

        }

        private void bindCom_Click(object sender, EventArgs e)
        {
            string COMInfo = txtCom.Text;
            ButtonControl.bindCOM(COMInfo);
        }
    }
}
