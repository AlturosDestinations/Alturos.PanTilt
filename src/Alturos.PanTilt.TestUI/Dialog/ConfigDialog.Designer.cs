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
            this.labelPanTiltIpAddress = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCameraIpAddress = new System.Windows.Forms.TextBox();
            this.buttonContinue = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxComType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxPort = new System.Windows.Forms.ComboBox();
            this.labelComPort = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxCameraImageUrl = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxCameraActive = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxDiscoverd = new System.Windows.Forms.ComboBox();
            this.comboBoxPanTiltControl = new System.Windows.Forms.ComboBox();
            this.labelPanTiltControl = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxPanTilt
            // 
            this.textBoxPanTilt.Location = new System.Drawing.Point(136, 100);
            this.textBoxPanTilt.Name = "textBoxPanTilt";
            this.textBoxPanTilt.Size = new System.Drawing.Size(143, 20);
            this.textBoxPanTilt.TabIndex = 2;
            this.textBoxPanTilt.Text = "192.168.184.35";
            this.textBoxPanTilt.Visible = false;
            // 
            // labelPanTiltIpAddress
            // 
            this.labelPanTiltIpAddress.AutoSize = true;
            this.labelPanTiltIpAddress.Location = new System.Drawing.Point(73, 103);
            this.labelPanTiltIpAddress.Name = "labelPanTiltIpAddress";
            this.labelPanTiltIpAddress.Size = new System.Drawing.Size(57, 13);
            this.labelPanTiltIpAddress.TabIndex = 1;
            this.labelPanTiltIpAddress.Text = "IpAddress:";
            this.labelPanTiltIpAddress.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "IpAddress:";
            // 
            // textBoxCameraIpAddress
            // 
            this.textBoxCameraIpAddress.Location = new System.Drawing.Point(136, 71);
            this.textBoxCameraIpAddress.Name = "textBoxCameraIpAddress";
            this.textBoxCameraIpAddress.Size = new System.Drawing.Size(143, 20);
            this.textBoxCameraIpAddress.TabIndex = 1;
            this.textBoxCameraIpAddress.Text = "192.168.184.30";
            // 
            // buttonContinue
            // 
            this.buttonContinue.Location = new System.Drawing.Point(235, 291);
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
            this.label3.Location = new System.Drawing.Point(76, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Image Url:";
            // 
            // comboBoxComType
            // 
            this.comboBoxComType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxComType.FormattingEnabled = true;
            this.comboBoxComType.Location = new System.Drawing.Point(136, 73);
            this.comboBoxComType.Name = "comboBoxComType";
            this.comboBoxComType.Size = new System.Drawing.Size(143, 21);
            this.comboBoxComType.TabIndex = 7;
            this.comboBoxComType.SelectedIndexChanged += new System.EventHandler(this.comboBox_ComType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Communication Type:";
            // 
            // comboBoxPort
            // 
            this.comboBoxPort.FormattingEnabled = true;
            this.comboBoxPort.Location = new System.Drawing.Point(136, 99);
            this.comboBoxPort.Name = "comboBoxPort";
            this.comboBoxPort.Size = new System.Drawing.Size(143, 21);
            this.comboBoxPort.TabIndex = 9;
            this.comboBoxPort.Visible = false;
            // 
            // labelComPort
            // 
            this.labelComPort.AutoSize = true;
            this.labelComPort.Location = new System.Drawing.Point(74, 103);
            this.labelComPort.Name = "labelComPort";
            this.labelComPort.Size = new System.Drawing.Size(56, 13);
            this.labelComPort.TabIndex = 8;
            this.labelComPort.Text = "COM Port:";
            this.labelComPort.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxCameraImageUrl);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.checkBoxCameraActive);
            this.groupBox1.Controls.Add(this.textBoxCameraIpAddress);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 176);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 109);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Camera";
            // 
            // comboBoxCameraImageUrl
            // 
            this.comboBoxCameraImageUrl.FormattingEnabled = true;
            this.comboBoxCameraImageUrl.Location = new System.Drawing.Point(136, 44);
            this.comboBoxCameraImageUrl.Name = "comboBoxCameraImageUrl";
            this.comboBoxCameraImageUrl.Size = new System.Drawing.Size(143, 21);
            this.comboBoxCameraImageUrl.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(91, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Active:";
            // 
            // checkBoxCameraActive
            // 
            this.checkBoxCameraActive.AutoSize = true;
            this.checkBoxCameraActive.Location = new System.Drawing.Point(137, 21);
            this.checkBoxCameraActive.Name = "checkBoxCameraActive";
            this.checkBoxCameraActive.Size = new System.Drawing.Size(15, 14);
            this.checkBoxCameraActive.TabIndex = 6;
            this.checkBoxCameraActive.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.comboBoxDiscoverd);
            this.groupBox2.Controls.Add(this.comboBoxPanTiltControl);
            this.groupBox2.Controls.Add(this.labelPanTiltControl);
            this.groupBox2.Controls.Add(this.comboBoxComType);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBoxPanTilt);
            this.groupBox2.Controls.Add(this.labelPanTiltIpAddress);
            this.groupBox2.Controls.Add(this.labelComPort);
            this.groupBox2.Controls.Add(this.comboBoxPort);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(298, 158);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PanTilt Head";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(65, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Discovered:";
            // 
            // comboBoxDiscoverd
            // 
            this.comboBoxDiscoverd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDiscoverd.FormattingEnabled = true;
            this.comboBoxDiscoverd.Location = new System.Drawing.Point(136, 19);
            this.comboBoxDiscoverd.Name = "comboBoxDiscoverd";
            this.comboBoxDiscoverd.Size = new System.Drawing.Size(143, 21);
            this.comboBoxDiscoverd.TabIndex = 12;
            this.comboBoxDiscoverd.SelectedIndexChanged += new System.EventHandler(this.comboBoxDiscoverd_SelectedIndexChanged);
            // 
            // comboBoxPanTiltControl
            // 
            this.comboBoxPanTiltControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPanTiltControl.FormattingEnabled = true;
            this.comboBoxPanTiltControl.Location = new System.Drawing.Point(136, 46);
            this.comboBoxPanTiltControl.Name = "comboBoxPanTiltControl";
            this.comboBoxPanTiltControl.Size = new System.Drawing.Size(143, 21);
            this.comboBoxPanTiltControl.TabIndex = 11;
            // 
            // labelPanTiltControl
            // 
            this.labelPanTiltControl.AutoSize = true;
            this.labelPanTiltControl.Location = new System.Drawing.Point(57, 49);
            this.labelPanTiltControl.Name = "labelPanTiltControl";
            this.labelPanTiltControl.Size = new System.Drawing.Size(73, 13);
            this.labelPanTiltControl.TabIndex = 10;
            this.labelPanTiltControl.Text = "Manufacturer:";
            // 
            // ConfigDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 323);
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
        private System.Windows.Forms.Label labelPanTiltIpAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxCameraIpAddress;
        private System.Windows.Forms.Button buttonContinue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxComType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxPort;
        private System.Windows.Forms.Label labelComPort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxCameraActive;
        private System.Windows.Forms.ComboBox comboBoxCameraImageUrl;
        private System.Windows.Forms.ComboBox comboBoxPanTiltControl;
        private System.Windows.Forms.Label labelPanTiltControl;
        private System.Windows.Forms.ComboBox comboBoxDiscoverd;
        private System.Windows.Forms.Label label5;
    }
}