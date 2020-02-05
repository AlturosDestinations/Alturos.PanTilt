namespace Alturos.PanTilt.TestUI.CustomControl
{
    partial class AlturosUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelVersion = new System.Windows.Forms.Label();
            this.textBoxFirmwareVersion = new System.Windows.Forms.TextBox();
            this.buttonStartUpdate = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelUpdateStatus = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonTemperature = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxTiltInitialError = new System.Windows.Forms.TextBox();
            this.textBoxPanInitialError = new System.Windows.Forms.TextBox();
            this.buttonTiltInitialError = new System.Windows.Forms.Button();
            this.buttonPanInitialError = new System.Windows.Forms.Button();
            this.textBoxTiltHalSensor = new System.Windows.Forms.TextBox();
            this.buttonTiltHalSensor = new System.Windows.Forms.Button();
            this.buttonPanHalSensor = new System.Windows.Forms.Button();
            this.textBoxPanHalSensor = new System.Windows.Forms.TextBox();
            this.buttonHumidity = new System.Windows.Forms.Button();
            this.textBoxHumidity = new System.Windows.Forms.TextBox();
            this.textBoxTemperature = new System.Windows.Forms.TextBox();
            this.buttonGetConfig = new System.Windows.Forms.Button();
            this.buttonSetConfig = new System.Windows.Forms.Button();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxIpAddressPart1 = new System.Windows.Forms.TextBox();
            this.textBoxIpAddressPart2 = new System.Windows.Forms.TextBox();
            this.textBoxIpAddressPart3 = new System.Windows.Forms.TextBox();
            this.textBoxIpAddressPart4 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(15, 29);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(45, 13);
            this.labelVersion.TabIndex = 0;
            this.labelVersion.Text = "Version:";
            // 
            // textBoxFirmwareVersion
            // 
            this.textBoxFirmwareVersion.Location = new System.Drawing.Point(66, 26);
            this.textBoxFirmwareVersion.Name = "textBoxFirmwareVersion";
            this.textBoxFirmwareVersion.Size = new System.Drawing.Size(57, 20);
            this.textBoxFirmwareVersion.TabIndex = 1;
            // 
            // buttonStartUpdate
            // 
            this.buttonStartUpdate.Location = new System.Drawing.Point(129, 24);
            this.buttonStartUpdate.Name = "buttonStartUpdate";
            this.buttonStartUpdate.Size = new System.Drawing.Size(125, 23);
            this.buttonStartUpdate.TabIndex = 2;
            this.buttonStartUpdate.Text = "Update";
            this.buttonStartUpdate.UseVisualStyleBackColor = true;
            this.buttonStartUpdate.Click += new System.EventHandler(this.buttonStartUpdate_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(18, 53);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(236, 30);
            this.progressBar1.TabIndex = 3;
            // 
            // labelUpdateStatus
            // 
            this.labelUpdateStatus.AutoSize = true;
            this.labelUpdateStatus.Location = new System.Drawing.Point(15, 86);
            this.labelUpdateStatus.Name = "labelUpdateStatus";
            this.labelUpdateStatus.Size = new System.Drawing.Size(84, 13);
            this.labelUpdateStatus.TabIndex = 4;
            this.labelUpdateStatus.Text = "change by code";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelVersion);
            this.groupBox1.Controls.Add(this.labelUpdateStatus);
            this.groupBox1.Controls.Add(this.textBoxFirmwareVersion);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.buttonStartUpdate);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(293, 125);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Firmware Update";
            // 
            // buttonTemperature
            // 
            this.buttonTemperature.Location = new System.Drawing.Point(112, 17);
            this.buttonTemperature.Name = "buttonTemperature";
            this.buttonTemperature.Size = new System.Drawing.Size(129, 23);
            this.buttonTemperature.TabIndex = 6;
            this.buttonTemperature.Text = "Temperature";
            this.buttonTemperature.UseVisualStyleBackColor = true;
            this.buttonTemperature.Click += new System.EventHandler(this.buttonTemperature_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxTiltInitialError);
            this.groupBox2.Controls.Add(this.textBoxPanInitialError);
            this.groupBox2.Controls.Add(this.buttonTiltInitialError);
            this.groupBox2.Controls.Add(this.buttonPanInitialError);
            this.groupBox2.Controls.Add(this.textBoxTiltHalSensor);
            this.groupBox2.Controls.Add(this.buttonTiltHalSensor);
            this.groupBox2.Controls.Add(this.buttonPanHalSensor);
            this.groupBox2.Controls.Add(this.textBoxPanHalSensor);
            this.groupBox2.Controls.Add(this.buttonHumidity);
            this.groupBox2.Controls.Add(this.textBoxHumidity);
            this.groupBox2.Controls.Add(this.textBoxTemperature);
            this.groupBox2.Controls.Add(this.buttonTemperature);
            this.groupBox2.Location = new System.Drawing.Point(3, 134);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(293, 196);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Statstic Data";
            // 
            // textBoxTiltInitialError
            // 
            this.textBoxTiltInitialError.Location = new System.Drawing.Point(6, 164);
            this.textBoxTiltInitialError.Name = "textBoxTiltInitialError";
            this.textBoxTiltInitialError.Size = new System.Drawing.Size(100, 20);
            this.textBoxTiltInitialError.TabIndex = 16;
            // 
            // textBoxPanInitialError
            // 
            this.textBoxPanInitialError.Location = new System.Drawing.Point(6, 135);
            this.textBoxPanInitialError.Name = "textBoxPanInitialError";
            this.textBoxPanInitialError.Size = new System.Drawing.Size(100, 20);
            this.textBoxPanInitialError.TabIndex = 15;
            // 
            // buttonTiltInitialError
            // 
            this.buttonTiltInitialError.Location = new System.Drawing.Point(112, 162);
            this.buttonTiltInitialError.Name = "buttonTiltInitialError";
            this.buttonTiltInitialError.Size = new System.Drawing.Size(129, 23);
            this.buttonTiltInitialError.TabIndex = 14;
            this.buttonTiltInitialError.Text = "Tilt Initial Error";
            this.buttonTiltInitialError.UseVisualStyleBackColor = true;
            this.buttonTiltInitialError.Click += new System.EventHandler(this.buttonTiltInitialError_Click);
            // 
            // buttonPanInitialError
            // 
            this.buttonPanInitialError.Location = new System.Drawing.Point(112, 133);
            this.buttonPanInitialError.Name = "buttonPanInitialError";
            this.buttonPanInitialError.Size = new System.Drawing.Size(129, 23);
            this.buttonPanInitialError.TabIndex = 13;
            this.buttonPanInitialError.Text = "Pan Initial Error";
            this.buttonPanInitialError.UseVisualStyleBackColor = true;
            this.buttonPanInitialError.Click += new System.EventHandler(this.buttonPanInitialError_Click);
            // 
            // textBoxTiltHalSensor
            // 
            this.textBoxTiltHalSensor.Location = new System.Drawing.Point(6, 106);
            this.textBoxTiltHalSensor.Name = "textBoxTiltHalSensor";
            this.textBoxTiltHalSensor.Size = new System.Drawing.Size(100, 20);
            this.textBoxTiltHalSensor.TabIndex = 12;
            // 
            // buttonTiltHalSensor
            // 
            this.buttonTiltHalSensor.Location = new System.Drawing.Point(112, 104);
            this.buttonTiltHalSensor.Name = "buttonTiltHalSensor";
            this.buttonTiltHalSensor.Size = new System.Drawing.Size(129, 23);
            this.buttonTiltHalSensor.TabIndex = 11;
            this.buttonTiltHalSensor.Text = "Tilt HalSensor";
            this.buttonTiltHalSensor.UseVisualStyleBackColor = true;
            this.buttonTiltHalSensor.Click += new System.EventHandler(this.buttonTiltHalSensor_Click);
            // 
            // buttonPanHalSensor
            // 
            this.buttonPanHalSensor.Location = new System.Drawing.Point(112, 75);
            this.buttonPanHalSensor.Name = "buttonPanHalSensor";
            this.buttonPanHalSensor.Size = new System.Drawing.Size(129, 23);
            this.buttonPanHalSensor.TabIndex = 10;
            this.buttonPanHalSensor.Text = "Pan HalSensor";
            this.buttonPanHalSensor.UseVisualStyleBackColor = true;
            this.buttonPanHalSensor.Click += new System.EventHandler(this.buttonPanHalSensor_Click);
            // 
            // textBoxPanHalSensor
            // 
            this.textBoxPanHalSensor.Location = new System.Drawing.Point(6, 77);
            this.textBoxPanHalSensor.Name = "textBoxPanHalSensor";
            this.textBoxPanHalSensor.Size = new System.Drawing.Size(100, 20);
            this.textBoxPanHalSensor.TabIndex = 9;
            // 
            // buttonHumidity
            // 
            this.buttonHumidity.Location = new System.Drawing.Point(112, 46);
            this.buttonHumidity.Name = "buttonHumidity";
            this.buttonHumidity.Size = new System.Drawing.Size(129, 23);
            this.buttonHumidity.TabIndex = 8;
            this.buttonHumidity.Text = "Humidity";
            this.buttonHumidity.UseVisualStyleBackColor = true;
            this.buttonHumidity.Click += new System.EventHandler(this.buttonHumidity_Click);
            // 
            // textBoxHumidity
            // 
            this.textBoxHumidity.Location = new System.Drawing.Point(6, 48);
            this.textBoxHumidity.Name = "textBoxHumidity";
            this.textBoxHumidity.Size = new System.Drawing.Size(100, 20);
            this.textBoxHumidity.TabIndex = 7;
            // 
            // textBoxTemperature
            // 
            this.textBoxTemperature.Location = new System.Drawing.Point(6, 19);
            this.textBoxTemperature.Name = "textBoxTemperature";
            this.textBoxTemperature.Size = new System.Drawing.Size(100, 20);
            this.textBoxTemperature.TabIndex = 0;
            // 
            // buttonGetConfig
            // 
            this.buttonGetConfig.Location = new System.Drawing.Point(6, 19);
            this.buttonGetConfig.Name = "buttonGetConfig";
            this.buttonGetConfig.Size = new System.Drawing.Size(75, 23);
            this.buttonGetConfig.TabIndex = 8;
            this.buttonGetConfig.Text = "Get";
            this.buttonGetConfig.UseVisualStyleBackColor = true;
            this.buttonGetConfig.Click += new System.EventHandler(this.buttonGetConfig_Click);
            // 
            // buttonSetConfig
            // 
            this.buttonSetConfig.Location = new System.Drawing.Point(87, 19);
            this.buttonSetConfig.Name = "buttonSetConfig";
            this.buttonSetConfig.Size = new System.Drawing.Size(75, 23);
            this.buttonSetConfig.TabIndex = 9;
            this.buttonSetConfig.Text = "Set";
            this.buttonSetConfig.UseVisualStyleBackColor = true;
            this.buttonSetConfig.Click += new System.EventHandler(this.buttonSetConfig_Click);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.HelpVisible = false;
            this.propertyGrid1.Location = new System.Drawing.Point(6, 48);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(390, 331);
            this.propertyGrid1.TabIndex = 10;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.buttonGetConfig);
            this.groupBox3.Controls.Add(this.propertyGrid1);
            this.groupBox3.Controls.Add(this.buttonSetConfig);
            this.groupBox3.Location = new System.Drawing.Point(302, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(402, 434);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Config";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxIpAddressPart4);
            this.groupBox4.Controls.Add(this.textBoxIpAddressPart3);
            this.groupBox4.Controls.Add(this.textBoxIpAddressPart2);
            this.groupBox4.Controls.Add(this.textBoxIpAddressPart1);
            this.groupBox4.Location = new System.Drawing.Point(6, 385);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(214, 42);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "IpAddress";
            // 
            // textBoxIpAddressPart1
            // 
            this.textBoxIpAddressPart1.Location = new System.Drawing.Point(6, 16);
            this.textBoxIpAddressPart1.Name = "textBoxIpAddressPart1";
            this.textBoxIpAddressPart1.Size = new System.Drawing.Size(44, 20);
            this.textBoxIpAddressPart1.TabIndex = 0;
            // 
            // textBoxIpAddressPart2
            // 
            this.textBoxIpAddressPart2.Location = new System.Drawing.Point(56, 16);
            this.textBoxIpAddressPart2.Name = "textBoxIpAddressPart2";
            this.textBoxIpAddressPart2.Size = new System.Drawing.Size(44, 20);
            this.textBoxIpAddressPart2.TabIndex = 1;
            // 
            // textBoxIpAddressPart3
            // 
            this.textBoxIpAddressPart3.Location = new System.Drawing.Point(106, 16);
            this.textBoxIpAddressPart3.Name = "textBoxIpAddressPart3";
            this.textBoxIpAddressPart3.Size = new System.Drawing.Size(44, 20);
            this.textBoxIpAddressPart3.TabIndex = 2;
            // 
            // textBoxIpAddressPart4
            // 
            this.textBoxIpAddressPart4.Location = new System.Drawing.Point(156, 16);
            this.textBoxIpAddressPart4.Name = "textBoxIpAddressPart4";
            this.textBoxIpAddressPart4.Size = new System.Drawing.Size(44, 20);
            this.textBoxIpAddressPart4.TabIndex = 3;
            // 
            // AlturosUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "AlturosUserControl";
            this.Size = new System.Drawing.Size(780, 440);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.TextBox textBoxFirmwareVersion;
        private System.Windows.Forms.Button buttonStartUpdate;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelUpdateStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonTemperature;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxTemperature;
        private System.Windows.Forms.Button buttonHumidity;
        private System.Windows.Forms.TextBox textBoxHumidity;
        private System.Windows.Forms.TextBox textBoxTiltHalSensor;
        private System.Windows.Forms.Button buttonTiltHalSensor;
        private System.Windows.Forms.Button buttonPanHalSensor;
        private System.Windows.Forms.TextBox textBoxPanHalSensor;
        private System.Windows.Forms.TextBox textBoxTiltInitialError;
        private System.Windows.Forms.TextBox textBoxPanInitialError;
        private System.Windows.Forms.Button buttonTiltInitialError;
        private System.Windows.Forms.Button buttonPanInitialError;
        private System.Windows.Forms.Button buttonGetConfig;
        private System.Windows.Forms.Button buttonSetConfig;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBoxIpAddressPart4;
        private System.Windows.Forms.TextBox textBoxIpAddressPart3;
        private System.Windows.Forms.TextBox textBoxIpAddressPart2;
        private System.Windows.Forms.TextBox textBoxIpAddressPart1;
    }
}
