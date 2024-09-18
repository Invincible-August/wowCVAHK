using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using wowCVAHK;
using System.Collections.Generic;
using System.IO;

namespace wowCVAHK
{
    public partial class ControlForm : Form
    {
        private IKeyboardMouseEvents globalHook;
        private bool isAltPressed = false;
        private bool isCtrlPressed = false;
        private bool isShiftPressed = false;

        private bool isRunning = false; // 用于标记是否启用了监听
        private bool toggleState = false; // 用于存储 Toggle 模式的状态
        private string battleHotKey;
        private string battleHotKeyType;

        private ButtonControl buttonCtrl;

        public ControlForm()
        {
            InitializeComponent();
            BaseAPI.RegisterHotKey(this.Handle, 1, Constant.MOD_ALT | Constant.MOD_CONTROL | Constant.MOD_SHIFT, (int)Keys.D1); //注册快捷键
            BaseAPI.RegisterHotKey(this.Handle, 2, Constant.MOD_ALT | Constant.MOD_CONTROL | Constant.MOD_SHIFT, (int)Keys.D2);
            BaseAPI.RegisterHotKey(this.Handle, 3, Constant.MOD_ALT | Constant.MOD_CONTROL | Constant.MOD_SHIFT, (int)Keys.D3);
            BaseAPI.RegisterHotKey(this.Handle, 4, Constant.MOD_ALT | Constant.MOD_CONTROL | Constant.MOD_SHIFT, (int)Keys.D4);

            //SubscribeGlobalHook();

            txtBindKey.Enter += bindKey_Enter;
            txtBindKey.Leave += bindKey_Leave;

            txtBattleKey.Enter += bindKey_Enter;
            txtBattleKey.Leave += bindKey_Leave;

            buttonCtrl = new ButtonControl();

            // 初始化LogManager并传递用于显示日志的TextBox
            LogManager.Initialize(txtLogInfo);

            // 初始隐藏TextBox
            txtLogInfo.Visible = false;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constant.WM_HOTKEY)
            {
                int hotkeyId = m.WParam.ToInt32();
                switch (hotkeyId)
                {
                    case 1:
                        // 热键 Shift+Ctrl+Alt+1 触发，调用获取窗口信息函数
                        btnSaveBaseInfo.PerformClick();
                        break;
                    case 2:
                        // 热键 Shift+Ctrl+Alt+2 触发，调用获取窗口信息函数
                        btnGetColor.PerformClick();
                        break;
                    case 3:
                        // 热键 Shift+Ctrl+Alt+3 触发，调用获取窗口信息函数
                        stratRunning.PerformClick();
                        break;
                    case 4:
                        // 热键 Shift+Ctrl+Alt+3 触发，调用获取窗口信息函数
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
            string handleTitle = ButtonControl.SaveWindowHandle();
            ControlForm mainForm = Application.OpenForms["ControlForm"] as ControlForm;
            if (mainForm != null)
            {
                mainForm.DisplayClickedWindowHandleInfo(handleTitle);
            }
        }

        private void battleKey_Click(object sender, EventArgs e)
        {
            string battleHotKey = txtBattleKey.Text;
            if (battleHotKey != null) 
            {
                if (battleHotKey.Length == 1)
                {
                    ButtonControl.SaveBattleHotKey(battleHotKey);
                }
                else 
                {
                    if (battleHotKey == "Space")
                    {
                        ButtonControl.SaveBattleHotKey(battleHotKey);
                    }
                    else 
                    {
                        txtBattleKey.Text = "不可以使用组合按键";
                    }

                }
                
            }            
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            string selectedMode = hotKeyType.SelectedItem.ToString();
            
            if (selectedMode != null)
            {
                if (selectedMode ==  "按住触发")
                { ButtonControl.SaveBattleHotKeyType("1"); }
                else if (selectedMode == "按下触发") 
                { ButtonControl.SaveBattleHotKeyType("2"); }
                
            }
        }

        private void stratRunning_Click(object sender, EventArgs e)
        {
            
            try 
            { 
                Dictionary<string, object> config = Utils.loadingConfig();
                battleHotKey = config["battleHotKey"] as string;
                battleHotKeyType = config["battleHotKeyType"] as string;
                if (!isRunning)
                {
                    isRunning = true;
                    buttonCtrl.running(); // 启动检测线程
                    HookKeys();
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show("错误发生: " + ex.Message + "\n详细信息: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            
                  
        }

        private void pauseRunning_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                isRunning = false;
                UnhookKeys();
                buttonCtrl.PauseDetection(); // 暂停检测线程
            }
        }

        // 恢复检测
        private void continueRunning_Click(object sender, EventArgs e)
        {
            buttonCtrl.ResumeDetection(); // 恢复检测线程
        }

        // 键盘钩子，用于捕获键盘事件
        private void HookKeys()
        {
            if (globalHook == null)  // 防止重复订阅
            {
                globalHook = Hook.GlobalEvents();
                globalHook.KeyDown += ControlForm_KeyDown;
                globalHook.KeyUp += ControlForm_KeyUp;
            }
        }

        private void UnhookKeys()
        {
            if (globalHook != null)
            {
                globalHook.KeyDown -= ControlForm_KeyDown;
                globalHook.KeyUp -= ControlForm_KeyUp;
                globalHook.Dispose();
                globalHook = null;  // 释放资源后设置为 null
            }
        }

        // 按键按下事件处理
        private void ControlForm_KeyDown(object sender, KeyEventArgs e)
        {            
            if (!isRunning) return;

            // 检查按下的键是否与 battleHotKey 匹配
            if (IsHotKeyPressed(e))
            {
                if (battleHotKeyType == "1")
                {
                    // 持续按住快捷键时执行操作
                    buttonCtrl.ResumeDetection();
                }
                else if (battleHotKeyType == "2")
                {
                    // Toggle 模式，按一次执行，再次按下停止
                    if (!toggleState)
                    {
                        buttonCtrl.ResumeDetection();
                    }
                    else
                    {
                        buttonCtrl.PauseDetection();
                    }
                    toggleState = !toggleState; // 切换状态
                }
            }
        }

        // 按键抬起事件处理
        private void ControlForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (!isRunning) return;

            // 如果是 battleHotKeyType = "1"，在按键抬起时停止操作
            if (battleHotKeyType == "1" && IsHotKeyPressed(e))
            {
                buttonCtrl.PauseDetection();
            }
        }

        private bool IsHotKeyPressed(KeyEventArgs e)
        {
            var key = e.KeyCode.ToString();            
            //Console.WriteLine($"key is {key}");

            if (key == "Space") 
            {
                key = key.ToUpper();
                Console.WriteLine($"key.ToUpper() = {key}");

            }

            if (battleHotKey.Contains("Alt") && e.Alt)
            {
                key = "Alt+" + key;
            }
            if (battleHotKey.Contains("Ctrl") && e.Control)
            {
                key = "Ctrl+" + key;
            }
            if (battleHotKey.Contains("Shift") && e.Shift)
            {
                key = "Shift+" + key;
            }
            return key == battleHotKey;
        }


        private void stopRunning_Click(object sender, EventArgs e)
        {
            ButtonControl.stop();
            if (isRunning)
            {
                isRunning = false;
                UnhookKeys(); // 停止键盘监听                
            }
        }
        

        private void bindCom_Click(object sender, EventArgs e)
        {
            string COMInfo = txtCom.Text;
            ButtonControl.bindCOM(COMInfo);
        }

        private void bindKey_Enter(object sender, EventArgs e)
        {            
            SubscribeGlobalHook();
        }


        // 当光标离开 txtBindKey 时取消订阅全局键盘钩子
        private void bindKey_Leave(object sender, EventArgs e)
        {
            UnsubscribeGlobalHook();
        }
        

        private void SubscribeGlobalHook()
        {
            if (globalHook == null)  // 防止重复订阅
            {
                globalHook = Hook.GlobalEvents();
                globalHook.KeyDown += TxtBindKey_KeyDown;
                globalHook.KeyUp += TxtBindKey_KeyUp;
            }
        }

        private void UnsubscribeGlobalHook()
        {
            if (globalHook != null)
            {
                globalHook.KeyDown -= TxtBindKey_KeyDown;
                globalHook.Dispose();
                globalHook = null;  // 释放资源后设置为 null
            }
        }

        internal void TxtBindKey_KeyDown(object sender, KeyEventArgs e)
        {
            Control activeControl = this.ActiveControl;
            string keyPressed = "";

            if (e.Alt && e.KeyCode != Keys.Menu)
            {
                isAltPressed = true;
            }

            // 检查是否按下了 Ctrl 键
            if (e.Control && e.KeyCode != Keys.ControlKey)
            {
                Console.WriteLine($"e.KeyCode ={e.KeyCode}");
                isCtrlPressed = true;
            }

            // 检查是否按下了 Shift 键
            if (e.Shift && e.KeyCode != Keys.ShiftKey)
            {
                isShiftPressed = true;
            }

            // 检查修饰键状态并添加到 keyPressed 中
            if (isAltPressed) keyPressed += "Alt+";
            if (isCtrlPressed) keyPressed += "Ctrl+";
            if (isShiftPressed) keyPressed += "Shift+";

            // 处理字母和数字按键的显示
            //() ||
            if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
            {
                keyPressed += e.KeyCode.ToString().Replace("D", "");
            }
            else if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
            {
                keyPressed += e.KeyCode.ToString();
            }
            // 处理特殊符号键的显示
            else if (e.KeyCode == Keys.Oemtilde)
            {
                keyPressed += "`";
            }
            else if (e.KeyCode == Keys.OemMinus)
            {
                keyPressed += "-";
            }
            else if (e.KeyCode == Keys.Oemplus)
            {
                keyPressed += "=";
            }
            else if (e.KeyCode == Keys.Space) 
            {
                keyPressed += "Space";
            }
            else
            { }            
            if(keyPressed == "")
            {
                // 如果按下了不符合的按键
                activeControl.Text = "请更换按键";
                return;
            }
            else
            {
                // 显示按键组合
                activeControl.Text = keyPressed;
            }            

            // 阻止按键继续传播
            e.Handled = true;            
        }

        private void TxtBindKey_KeyUp(object sender, KeyEventArgs e) 
        {
            // 重置修饰键的状态
            if (e.KeyCode == Keys.LMenu)
            {
                isAltPressed = false;
            }
            else if (e.KeyCode == Keys.LControlKey)
            {
                isCtrlPressed = false;
            }
            else if (e.KeyCode == Keys.LShiftKey)
            {
                isShiftPressed = false;
            }
        }        

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnsubscribeGlobalHook();
        }

        private void btnLogInfo_Click(object sender, EventArgs e)
        {
            LogManager.ToggleLog(); // 调用LogManager的ToggleLog方法
            btnLogInfo.Text = LogManager.isLogVisible ? "隐藏日志" : "显示日志"; // 根据状态更新按钮文字
        }
    }
}
