namespace Alturos.PanTilt.Calibration
{
    partial class CommunicationDialog
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
            this.comboBoxCommunicationType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.labelPanTiltControl = new System.Windows.Forms.Label();
            this.comboBoxPanTiltControl = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboBoxCommunicationType
            // 
            this.comboBoxCommunicationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCommunicationType.FormattingEnabled = true;
            this.comboBoxCommunicationType.Location = new System.Drawing.Point(124, 47);
            this.comboBoxCommunicationType.Name = "comboBoxCommunicationType";
            this.comboBoxCommunicationType.Size = new System.Drawing.Size(211, 21);
            this.comboBoxCommunicationType.TabIndex = 0;
            this.comboBoxCommunicationType.SelectedIndexChanged += new System.EventHandler(this.comboBoxConnectionType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "CommunicationType:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(31, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "change by code:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxValue
            // 
            this.textBoxValue.Location = new System.Drawing.Point(124, 74);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(211, 20);
            this.textBoxValue.TabIndex = 3;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(260, 100);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 4;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // labelPanTiltControl
            // 
            this.labelPanTiltControl.AutoSize = true;
            this.labelPanTiltControl.Location = new System.Drawing.Point(45, 23);
            this.labelPanTiltControl.Name = "labelPanTiltControl";
            this.labelPanTiltControl.Size = new System.Drawing.Size(73, 13);
            this.labelPanTiltControl.TabIndex = 11;
            this.labelPanTiltControl.Text = "Manufacturer:";
            // 
            // comboBoxPanTiltControl
            // 
            this.comboBoxPanTiltControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPanTiltControl.FormattingEnabled = true;
            this.comboBoxPanTiltControl.Location = new System.Drawing.Point(124, 20);
            this.comboBoxPanTiltControl.Name = "comboBoxPanTiltControl";
            this.comboBoxPanTiltControl.Size = new System.Drawing.Size(211, 21);
            this.comboBoxPanTiltControl.TabIndex = 12;
            // 
            // CommunicationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 146);
            this.Controls.Add(this.comboBoxPanTiltControl);
            this.Controls.Add(this.labelPanTiltControl);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.textBoxValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxCommunicationType);
            this.Name = "CommunicationDialog";
            this.Text = "Communication";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxCommunicationType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label labelPanTiltControl;
        private System.Windows.Forms.ComboBox comboBoxPanTiltControl;
    }
}