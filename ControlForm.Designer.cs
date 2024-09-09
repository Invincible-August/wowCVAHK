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
            this.SuspendLayout();
            // 
            // btnSaveBaseInfo
            // 
            this.btnSaveBaseInfo.Location = new System.Drawing.Point(118, 12);
            this.btnSaveBaseInfo.Name = "btnSaveBaseInfo";
            this.btnSaveBaseInfo.Size = new System.Drawing.Size(100, 23);
            this.btnSaveBaseInfo.TabIndex = 0;
            this.btnSaveBaseInfo.Text = "绑定窗口信息";
            this.btnSaveBaseInfo.UseVisualStyleBackColor = true;
            this.btnSaveBaseInfo.Click += new System.EventHandler(this.btnSaveBaseInfo_Click);
            // 
            // btnGetColor
            // 
            this.btnGetColor.Location = new System.Drawing.Point(118, 41);
            this.btnGetColor.Name = "btnGetColor";
            this.btnGetColor.Size = new System.Drawing.Size(100, 23);
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
            this.txtColor.Size = new System.Drawing.Size(100, 23);
            this.txtColor.TabIndex = 2;
            // 
            // btnBindKey
            // 
            this.btnBindKey.Location = new System.Drawing.Point(118, 99);
            this.btnBindKey.Name = "btnBindKey";
            this.btnBindKey.Size = new System.Drawing.Size(100, 23);
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
            this.txtBindKey.Size = new System.Drawing.Size(100, 23);
            this.txtBindKey.TabIndex = 4;
            // 
            // txtWindowHandle
            // 
            this.txtWindowHandle.Location = new System.Drawing.Point(12, 12);
            this.txtWindowHandle.Multiline = true;
            this.txtWindowHandle.Name = "txtWindowHandle";
            this.txtWindowHandle.Size = new System.Drawing.Size(100, 23);
            this.txtWindowHandle.TabIndex = 5;
            // 
            // txtColorCoordinate
            // 
            this.txtColorCoordinate.Location = new System.Drawing.Point(12, 41);
            this.txtColorCoordinate.Multiline = true;
            this.txtColorCoordinate.Name = "txtColorCoordinate";
            this.txtColorCoordinate.Size = new System.Drawing.Size(100, 23);
            this.txtColorCoordinate.TabIndex = 6;
            // 
            // stratRunning
            // 
            this.stratRunning.Location = new System.Drawing.Point(12, 128);
            this.stratRunning.Name = "stratRunning";
            this.stratRunning.Size = new System.Drawing.Size(100, 23);
            this.stratRunning.TabIndex = 7;
            this.stratRunning.Text = "运行";
            this.stratRunning.UseVisualStyleBackColor = true;
            this.stratRunning.Click += new System.EventHandler(this.stratRunning_Click);
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 435);
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
    }
}