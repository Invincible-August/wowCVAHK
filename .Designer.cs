namespace wowCVAHK
{
    partial class mainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtRGB = new System.Windows.Forms.TextBox();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.saveInfo = new System.Windows.Forms.Button();
            this.getColor = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.running = new System.Windows.Forms.Button();
            this.ImportScripts = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtRGB
            // 
            this.txtRGB.BackColor = System.Drawing.SystemColors.Window;
            this.txtRGB.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtRGB.Location = new System.Drawing.Point(12, 41);
            this.txtRGB.Name = "txtRGB";
            this.txtRGB.ReadOnly = true;
            this.txtRGB.Size = new System.Drawing.Size(100, 21);
            this.txtRGB.TabIndex = 1;
            this.txtRGB.TextChanged += new System.EventHandler(this.txtRGB_TextChanged);
            // 
            // txtKey
            // 
            this.txtKey.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtKey.Location = new System.Drawing.Point(12, 68);
            this.txtKey.Name = "txtKey";
            this.txtKey.ReadOnly = true;
            this.txtKey.Size = new System.Drawing.Size(100, 21);
            this.txtKey.TabIndex = 2;
            // 
            // saveInfo
            // 
            this.saveInfo.Location = new System.Drawing.Point(12, 12);
            this.saveInfo.Name = "saveInfo";
            this.saveInfo.Size = new System.Drawing.Size(100, 23);
            this.saveInfo.TabIndex = 3;
            this.saveInfo.Text = "绑定启动信息";
            this.saveInfo.UseVisualStyleBackColor = true;
            this.saveInfo.Click += new System.EventHandler(this.saveInfo_Click);
            // 
            // getColor
            // 
            this.getColor.Location = new System.Drawing.Point(118, 41);
            this.getColor.Name = "getColor";
            this.getColor.Size = new System.Drawing.Size(75, 23);
            this.getColor.TabIndex = 4;
            this.getColor.Text = "获取色块";
            this.getColor.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(118, 70);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "绑定定按键";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // running
            // 
            this.running.Location = new System.Drawing.Point(61, 99);
            this.running.Name = "running";
            this.running.Size = new System.Drawing.Size(75, 23);
            this.running.TabIndex = 6;
            this.running.Text = "启动";
            this.running.UseVisualStyleBackColor = true;
            this.running.Click += new System.EventHandler(this.running_Click);
            // 
            // ImportScripts
            // 
            this.ImportScripts.Location = new System.Drawing.Point(118, 12);
            this.ImportScripts.Name = "ImportScripts";
            this.ImportScripts.Size = new System.Drawing.Size(75, 23);
            this.ImportScripts.TabIndex = 7;
            this.ImportScripts.Text = "导入脚本";
            this.ImportScripts.UseVisualStyleBackColor = true;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(216, 132);
            this.Controls.Add(this.ImportScripts);
            this.Controls.Add(this.running);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.getColor);
            this.Controls.Add(this.saveInfo);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.txtRGB);
            this.Name = "mainForm";
            this.Text = "菜菜的作弊器";
            this.TransparencyKey = System.Drawing.Color.White;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //this.Load += new System.EventHandler(this.mainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtRGB;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Button saveInfo;
        private System.Windows.Forms.Button getColor;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button running;
        private System.Windows.Forms.Button ImportScripts;
    }
}

