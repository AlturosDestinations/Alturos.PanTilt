namespace Alturos.PanTilt.TestUI.Dialog
{
    partial class ConfigDialog
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
            this.textBoxPanTilt = new System.Windows.Forms.TextBox();
            this.labelPTIP = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCameraIpAddress = new System.Windows.Forms.TextBox();
            this.buttonContinue = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxComType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxPort = new System.Windows.Forms.ComboBox();
            this.labelCOMPort = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxCameraActive = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxCameraJpegUrl = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxPanTilt
            // 
            this.textBoxPanTilt.Location = new System.Drawing.Point(150, 46);
            this.textBoxPanTilt.Name = "textBoxPanTilt";
            this.textBoxPanTilt.Size = new System.Drawing.Size(129, 20);
            this.textBoxPanTilt.TabIndex = 2;
            this.textBoxPanTilt.Text = "192.168.184.35";
            this.textBoxPanTilt.Visible = false;
            // 
            // labelPTIP
            // 
            this.labelPTIP.AutoSize = true;
            this.labelPTIP.Location = new System.Drawing.Point(87, 49);
            this.labelPTIP.Name = "labelPTIP";
            this.labelPTIP.Size = new System.Drawing.Size(57, 13);
            this.labelPTIP.TabIndex = 1;
            this.labelPTIP.Text = "IpAddress:";
            this.labelPTIP.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(88, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "IpAddress:";
            // 
            // textBoxCameraIpAddress
            // 
            this.textBoxCameraIpAddress.Location = new System.Drawing.Point(150, 71);
            this.textBoxCameraIpAddress.Name = "textBoxCameraIpAddress";
            this.textBoxCameraIpAddress.Size = new System.Drawing.Size(129, 20);
            this.textBoxCameraIpAddress.TabIndex = 1;
            this.textBoxCameraIpAddress.Text = "192.168.184.30";
            // 
            // buttonContinue
            // 
            this.buttonContinue.Location = new System.Drawing.Point(235, 215);
            this.buttonContinue.Name = "buttonContinue";
            this.buttonContinue.Size = new System.Drawing.Size(75, 23);
            this.buttonContinue.TabIndex = 3;
            this.buttonContinue.Text = "Continue";
            this.buttonContinue.UseVisualStyleBackColor = true;
            this.buttonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(89, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Jpeg Url:";
            // 
            // comboBoxComType
            // 
            this.comboBoxComType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxComType.FormattingEnabled = true;
            this.comboBoxComType.Location = new System.Drawing.Point(150, 19);
            this.comboBoxComType.Name = "comboBoxComType";
            this.comboBoxComType.Size = new System.Drawing.Size(129, 21);
            this.comboBoxComType.TabIndex = 7;
            this.comboBoxComType.SelectedIndexChanged += new System.EventHandler(this.comboBox_ComType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Communication Type:";
            // 
            // comboBoxPort
            // 
            this.comboBoxPort.FormattingEnabled = true;
            this.comboBoxPort.Location = new System.Drawing.Point(150, 45);
            this.comboBoxPort.Name = "comboBoxPort";
            this.comboBoxPort.Size = new System.Drawing.Size(129, 21);
            this.comboBoxPort.TabIndex = 9;
            this.comboBoxPort.Visible = false;
            // 
            // labelCOMPort
            // 
            this.labelCOMPort.AutoSize = true;
            this.labelCOMPort.Location = new System.Drawing.Point(88, 49);
            this.labelCOMPort.Name = "labelCOMPort";
            this.labelCOMPort.Size = new System.Drawing.Size(56, 13);
            this.labelCOMPort.TabIndex = 8;
            this.labelCOMPort.Text = "COM Port:";
            this.labelCOMPort.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxCameraJpegUrl);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.checkBoxCameraActive);
            this.groupBox1.Controls.Add(this.textBoxCameraIpAddress);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 109);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Camera";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Active:";
            // 
            // checkBoxCameraActive
            // 
            this.checkBoxCameraActive.AutoSize = true;
            this.checkBoxCameraActive.Location = new System.Drawing.Point(151, 21);
            this.checkBoxCameraActive.Name = "checkBoxCameraActive";
            this.checkBoxCameraActive.Size = new System.Drawing.Size(15, 14);
            this.checkBoxCameraActive.TabIndex = 6;
            this.checkBoxCameraActive.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxComType);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBoxPanTilt);
            this.groupBox2.Controls.Add(this.labelPTIP);
            this.groupBox2.Controls.Add(this.labelCOMPort);
            this.groupBox2.Controls.Add(this.comboBoxPort);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(298, 82);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PanTilt Head";
            // 
            // textBoxCameraMjpegUrl
            // 
            this.textBoxCameraJpegUrl.Location = new System.Drawing.Point(150, 44);
            this.textBoxCameraJpegUrl.Name = "textBoxCameraMjpegUrl";
            this.textBoxCameraJpegUrl.Size = new System.Drawing.Size(129, 20);
            this.textBoxCameraJpegUrl.TabIndex = 8;
            this.textBoxCameraJpegUrl.Text = "/jpg/image.jpg";
            // 
            // ConfigDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 249);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonContinue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigDialog";
            this.Text = "Configuration";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPanTilt;
        private System.Windows.Forms.Label labelPTIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxCameraIpAddress;
        private System.Windows.Forms.Button buttonContinue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxComType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxPort;
        private System.Windows.Forms.Label labelCOMPort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxCameraActive;
        private System.Windows.Forms.TextBox textBoxCameraJpegUrl;
    }
}