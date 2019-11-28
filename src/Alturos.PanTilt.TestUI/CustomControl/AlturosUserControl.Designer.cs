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
            this.textBoxVersion = new System.Windows.Forms.TextBox();
            this.buttonStartUpdate = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelUpdateStatus = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
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
            // textBoxVersion
            // 
            this.textBoxVersion.Location = new System.Drawing.Point(66, 26);
            this.textBoxVersion.Name = "textBoxVersion";
            this.textBoxVersion.Size = new System.Drawing.Size(57, 20);
            this.textBoxVersion.TabIndex = 1;
            this.textBoxVersion.Text = "16038";
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
            this.groupBox1.Controls.Add(this.textBoxVersion);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.buttonStartUpdate);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(293, 125);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Firmware Update";
            // 
            // AlturosUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "AlturosUserControl";
            this.Size = new System.Drawing.Size(780, 440);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.TextBox textBoxVersion;
        private System.Windows.Forms.Button buttonStartUpdate;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelUpdateStatus;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
