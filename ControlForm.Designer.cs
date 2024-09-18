namespace wowCVAHK
{
    partial class ControlForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSaveBaseInfo = new System.Windows.Forms.Button();
            this.btnGetColor = new System.Windows.Forms.Button();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.btnBindKey = new System.Windows.Forms.Button();
            this.txtBindKey = new System.Windows.Forms.TextBox();
            this.txtWindowHandle = new System.Windows.Forms.TextBox();
            this.txtColorCoordinate = new System.Windows.Forms.TextBox();
            this.stratRunning = new System.Windows.Forms.Button();
            this.stopRunning = new System.Windows.Forms.Button();
            this.bindCom = new System.Windows.Forms.Button();
            this.txtCom = new System.Windows.Forms.TextBox();
            this.battleKey = new System.Windows.Forms.Button();
            this.hotKeyType = new System.Windows.Forms.ComboBox();
            this.txtBattleKey = new System.Windows.Forms.TextBox();
            this.confirm = new System.Windows.Forms.Button();
            this.pauseRunning = new System.Windows.Forms.Button();
            this.continueRunning = new System.Windows.Forms.Button();
            this.btnLogInfo = new System.Windows.Forms.Button();
            this.txtLogInfo = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnSaveBaseInfo
            // 
            this.btnSaveBaseInfo.Location = new System.Drawing.Point(138, 12);
            this.btnSaveBaseInfo.Name = "btnSaveBaseInfo";
            this.btnSaveBaseInfo.Size = new System.Drawing.Size(120, 23);
            this.btnSaveBaseInfo.TabIndex = 0;
            this.btnSaveBaseInfo.Text = "绑定窗口信息";
            this.btnSaveBaseInfo.UseVisualStyleBackColor = true;
            this.btnSaveBaseInfo.Click += new System.EventHandler(this.btnSaveBaseInfo_Click);
            // 
            // btnGetColor
            // 
            this.btnGetColor.Location = new System.Drawing.Point(138, 41);
            this.btnGetColor.Name = "btnGetColor";
            this.btnGetColor.Size = new System.Drawing.Size(120, 23);
            this.btnGetColor.TabIndex = 1;
            this.btnGetColor.Text = "获取色块信息";
            this.btnGetColor.UseVisualStyleBackColor = true;
            this.btnGetColor.Click += new System.EventHandler(this.btnGetColor_Click);
            // 
            // txtColor
            // 
            this.txtColor.Location = new System.Drawing.Point(12, 70);
            this.txtColor.Multiline = true;
            this.txtColor.Name = "txtColor";
            this.txtColor.ReadOnly = true;
            this.txtColor.Size = new System.Drawing.Size(120, 23);
            this.txtColor.TabIndex = 2;
            // 
            // btnBindKey
            // 
            this.btnBindKey.Location = new System.Drawing.Point(138, 99);
            this.btnBindKey.Name = "btnBindKey";
            this.btnBindKey.Size = new System.Drawing.Size(120, 23);
            this.btnBindKey.TabIndex = 3;
            this.btnBindKey.Text = "绑定按键";
            this.btnBindKey.UseVisualStyleBackColor = true;
            this.btnBindKey.Click += new System.EventHandler(this.btnBindKey_Click);
            // 
            // txtBindKey
            // 
            this.txtBindKey.Location = new System.Drawing.Point(12, 99);
            this.txtBindKey.Multiline = true;
            this.txtBindKey.Name = "txtBindKey";
            this.txtBindKey.ReadOnly = true;
            this.txtBindKey.Size = new System.Drawing.Size(120, 23);
            this.txtBindKey.TabIndex = 4;
            // 
            // txtWindowHandle
            // 
            this.txtWindowHandle.Location = new System.Drawing.Point(12, 12);
            this.txtWindowHandle.Multiline = true;
            this.txtWindowHandle.Name = "txtWindowHandle";
            this.txtWindowHandle.Size = new System.Drawing.Size(120, 23);
            this.txtWindowHandle.TabIndex = 5;
            // 
            // txtColorCoordinate
            // 
            this.txtColorCoordinate.Location = new System.Drawing.Point(12, 41);
            this.txtColorCoordinate.Multiline = true;
            this.txtColorCoordinate.Name = "txtColorCoordinate";
            this.txtColorCoordinate.Size = new System.Drawing.Size(120, 23);
            this.txtColorCoordinate.TabIndex = 6;
            // 
            // stratRunning
            // 
            this.stratRunning.Location = new System.Drawing.Point(12, 186);
            this.stratRunning.Name = "stratRunning";
            this.stratRunning.Size = new System.Drawing.Size(120, 23);
            this.stratRunning.TabIndex = 7;
            this.stratRunning.Text = "运行";
            this.stratRunning.UseVisualStyleBackColor = true;
            this.stratRunning.Click += new System.EventHandler(this.stratRunning_Click);
            // 
            // stopRunning
            // 
            this.stopRunning.Location = new System.Drawing.Point(138, 186);
            this.stopRunning.Name = "stopRunning";
            this.stopRunning.Size = new System.Drawing.Size(120, 23);
            this.stopRunning.TabIndex = 8;
            this.stopRunning.Text = "停止";
            this.stopRunning.UseVisualStyleBackColor = true;
            this.stopRunning.Click += new System.EventHandler(this.stopRunning_Click);
            // 
            // bindCom
            // 
            this.bindCom.Location = new System.Drawing.Point(138, 128);
            this.bindCom.Name = "bindCom";
            this.bindCom.Size = new System.Drawing.Size(120, 23);
            this.bindCom.TabIndex = 9;
            this.bindCom.Text = "绑定端口";
            this.bindCom.UseVisualStyleBackColor = true;
            this.bindCom.Click += new System.EventHandler(this.bindCom_Click);
            // 
            // txtCom
            // 
            this.txtCom.Location = new System.Drawing.Point(12, 128);
            this.txtCom.Multiline = true;
            this.txtCom.Name = "txtCom";
            this.txtCom.Size = new System.Drawing.Size(120, 23);
            this.txtCom.TabIndex = 10;
            // 
            // battleKey
            // 
            this.battleKey.Location = new System.Drawing.Point(138, 157);
            this.battleKey.Name = "battleKey";
            this.battleKey.Size = new System.Drawing.Size(120, 23);
            this.battleKey.TabIndex = 11;
            this.battleKey.Text = "战斗中快捷键";
            this.battleKey.UseVisualStyleBackColor = true;
            this.battleKey.Click += new System.EventHandler(this.battleKey_Click);
            // 
            // hotKeyType
            // 
            this.hotKeyType.FormattingEnabled = true;
            this.hotKeyType.Items.AddRange(new object[] {
            "按住触发",
            "按下触发"});
            this.hotKeyType.Location = new System.Drawing.Point(264, 160);
            this.hotKeyType.Name = "hotKeyType";
            this.hotKeyType.Size = new System.Drawing.Size(120, 20);
            this.hotKeyType.TabIndex = 13;
            // 
            // txtBattleKey
            // 
            this.txtBattleKey.Location = new System.Drawing.Point(12, 157);
            this.txtBattleKey.Multiline = true;
            this.txtBattleKey.Name = "txtBattleKey";
            this.txtBattleKey.ReadOnly = true;
            this.txtBattleKey.Size = new System.Drawing.Size(120, 23);
            this.txtBattleKey.TabIndex = 14;
            // 
            // confirm
            // 
            this.confirm.Location = new System.Drawing.Point(391, 160);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(50, 23);
            this.confirm.TabIndex = 15;
            this.confirm.Text = "确认";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.confirm_Click);
            // 
            // pauseRunning
            // 
            this.pauseRunning.Location = new System.Drawing.Point(138, 215);
            this.pauseRunning.Name = "pauseRunning";
            this.pauseRunning.Size = new System.Drawing.Size(120, 23);
            this.pauseRunning.TabIndex = 16;
            this.pauseRunning.Text = "暂停";
            this.pauseRunning.UseVisualStyleBackColor = true;
            this.pauseRunning.Click += new System.EventHandler(this.pauseRunning_Click);
            // 
            // continueRunning
            // 
            this.continueRunning.Location = new System.Drawing.Point(12, 215);
            this.continueRunning.Name = "continueRunning";
            this.continueRunning.Size = new System.Drawing.Size(120, 23);
            this.continueRunning.TabIndex = 17;
            this.continueRunning.Text = "继续";
            this.continueRunning.UseVisualStyleBackColor = true;
            this.continueRunning.Click += new System.EventHandler(this.continueRunning_Click);
            // 
            // btnLogInfo
            // 
            this.btnLogInfo.Location = new System.Drawing.Point(264, 186);
            this.btnLogInfo.Name = "btnLogInfo";
            this.btnLogInfo.Size = new System.Drawing.Size(75, 23);
            this.btnLogInfo.TabIndex = 18;
            this.btnLogInfo.Text = "显示日志";
            this.btnLogInfo.UseVisualStyleBackColor = true;
            this.btnLogInfo.Click += new System.EventHandler(this.btnLogInfo_Click);
            // 
            // txtLogInfo
            // 
            this.txtLogInfo.Location = new System.Drawing.Point(264, 14);
            this.txtLogInfo.Name = "txtLogInfo";
            this.txtLogInfo.Size = new System.Drawing.Size(205, 137);
            this.txtLogInfo.TabIndex = 19;
            this.txtLogInfo.Text = "";
            this.txtLogInfo.Visible = false;
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 256);
            this.Controls.Add(this.txtLogInfo);
            this.Controls.Add(this.btnLogInfo);
            this.Controls.Add(this.continueRunning);
            this.Controls.Add(this.pauseRunning);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.txtBattleKey);
            this.Controls.Add(this.hotKeyType);
            this.Controls.Add(this.battleKey);
            this.Controls.Add(this.txtCom);
            this.Controls.Add(this.bindCom);
            this.Controls.Add(this.stopRunning);
            this.Controls.Add(this.stratRunning);
            this.Controls.Add(this.txtColorCoordinate);
            this.Controls.Add(this.txtWindowHandle);
            this.Controls.Add(this.txtBindKey);
            this.Controls.Add(this.btnBindKey);
            this.Controls.Add(this.txtColor);
            this.Controls.Add(this.btnGetColor);
            this.Controls.Add(this.btnSaveBaseInfo);
            this.Name = "ControlForm";
            this.Text = "菜菜的作弊器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveBaseInfo;
        private System.Windows.Forms.Button btnGetColor;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Button btnBindKey;
        private System.Windows.Forms.TextBox txtBindKey;
        private System.Windows.Forms.TextBox txtWindowHandle;
        private System.Windows.Forms.TextBox txtColorCoordinate;
        private System.Windows.Forms.Button stratRunning;
        private System.Windows.Forms.Button stopRunning;
        private System.Windows.Forms.Button bindCom;
        private System.Windows.Forms.TextBox txtCom;
        private System.Windows.Forms.Button battleKey;        
        private System.Windows.Forms.ComboBox hotKeyType;
        private System.Windows.Forms.TextBox txtBattleKey;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.Button pauseRunning;
        private System.Windows.Forms.Button continueRunning;
        private System.Windows.Forms.Button btnLogInfo;
        private System.Windows.Forms.RichTextBox txtLogInfo;
    }
}