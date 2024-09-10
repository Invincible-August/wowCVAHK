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
            this.stratRunning.Location = new System.Drawing.Point(12, 157);
            this.stratRunning.Name = "stratRunning";
            this.stratRunning.Size = new System.Drawing.Size(120, 23);
            this.stratRunning.TabIndex = 7;
            this.stratRunning.Text = "运行";
            this.stratRunning.UseVisualStyleBackColor = true;
            this.stratRunning.Click += new System.EventHandler(this.stratRunning_Click);
            // 
            // stopRunning
            // 
            this.stopRunning.Location = new System.Drawing.Point(138, 157);
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
            this.txtCom.TextChanged += new System.EventHandler(this.txtCom_TextChanged);
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 435);
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
    }
}