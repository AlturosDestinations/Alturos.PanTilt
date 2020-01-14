namespace Alturos.PanTilt.TestUI.CustomControl
{
    partial class EneoUserControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonSmoothingHigh = new System.Windows.Forms.Button();
            this.buttonSmoothingNormal = new System.Windows.Forms.Button();
            this.buttonSmoothingLow = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.buttonEnableLimits = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.buttonLimitLeft = new System.Windows.Forms.Button();
            this.buttonLimitUp = new System.Windows.Forms.Button();
            this.buttonSetLimitDown = new System.Windows.Forms.Button();
            this.buttonSetLimitRight = new System.Windows.Forms.Button();
            this.buttonDisableLimits = new System.Windows.Forms.Button();
            this.textBoxAcceleration = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxGain = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSetSmoothing = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonSetSmoothing);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxGain);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxAcceleration);
            this.groupBox1.Controls.Add(this.buttonSmoothingHigh);
            this.groupBox1.Controls.Add(this.buttonSmoothingNormal);
            this.groupBox1.Controls.Add(this.buttonSmoothingLow);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 111);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Smoothing (Is only used for absolute movement)";
            // 
            // buttonSmoothingHigh
            // 
            this.buttonSmoothingHigh.Location = new System.Drawing.Point(147, 19);
            this.buttonSmoothingHigh.Name = "buttonSmoothingHigh";
            this.buttonSmoothingHigh.Size = new System.Drawing.Size(63, 23);
            this.buttonSmoothingHigh.TabIndex = 21;
            this.buttonSmoothingHigh.Text = "High";
            this.buttonSmoothingHigh.UseVisualStyleBackColor = true;
            this.buttonSmoothingHigh.Click += new System.EventHandler(this.buttonSmoothingHigh_Click);
            // 
            // buttonSmoothingNormal
            // 
            this.buttonSmoothingNormal.Location = new System.Drawing.Point(78, 19);
            this.buttonSmoothingNormal.Name = "buttonSmoothingNormal";
            this.buttonSmoothingNormal.Size = new System.Drawing.Size(63, 23);
            this.buttonSmoothingNormal.TabIndex = 20;
            this.buttonSmoothingNormal.Text = "Normal";
            this.buttonSmoothingNormal.UseVisualStyleBackColor = true;
            this.buttonSmoothingNormal.Click += new System.EventHandler(this.buttonSmoothingNormal_Click);
            // 
            // buttonSmoothingLow
            // 
            this.buttonSmoothingLow.Location = new System.Drawing.Point(9, 19);
            this.buttonSmoothingLow.Name = "buttonSmoothingLow";
            this.buttonSmoothingLow.Size = new System.Drawing.Size(63, 23);
            this.buttonSmoothingLow.TabIndex = 19;
            this.buttonSmoothingLow.Text = "Low";
            this.buttonSmoothingLow.UseVisualStyleBackColor = true;
            this.buttonSmoothingLow.Click += new System.EventHandler(this.buttonSmoothingLow_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.buttonEnableLimits);
            this.groupBox7.Controls.Add(this.groupBox5);
            this.groupBox7.Controls.Add(this.buttonDisableLimits);
            this.groupBox7.Location = new System.Drawing.Point(3, 120);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(341, 193);
            this.groupBox7.TabIndex = 25;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Limit";
            // 
            // buttonEnableLimits
            // 
            this.buttonEnableLimits.Location = new System.Drawing.Point(6, 20);
            this.buttonEnableLimits.Name = "buttonEnableLimits";
            this.buttonEnableLimits.Size = new System.Drawing.Size(70, 23);
            this.buttonEnableLimits.TabIndex = 4;
            this.buttonEnableLimits.Text = "Enable";
            this.buttonEnableLimits.UseVisualStyleBackColor = true;
            this.buttonEnableLimits.Click += new System.EventHandler(this.buttonEnableLimits_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.buttonLimitLeft);
            this.groupBox5.Controls.Add(this.buttonLimitUp);
            this.groupBox5.Controls.Add(this.buttonSetLimitDown);
            this.groupBox5.Controls.Add(this.buttonSetLimitRight);
            this.groupBox5.Location = new System.Drawing.Point(6, 48);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(319, 138);
            this.groupBox5.TabIndex = 17;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Set current position";
            // 
            // buttonLimitLeft
            // 
            this.buttonLimitLeft.Location = new System.Drawing.Point(79, 60);
            this.buttonLimitLeft.Name = "buttonLimitLeft";
            this.buttonLimitLeft.Size = new System.Drawing.Size(71, 23);
            this.buttonLimitLeft.TabIndex = 1;
            this.buttonLimitLeft.Text = "Limit Left";
            this.buttonLimitLeft.UseVisualStyleBackColor = true;
            this.buttonLimitLeft.Click += new System.EventHandler(this.buttonLimitLeft_Click);
            // 
            // buttonLimitUp
            // 
            this.buttonLimitUp.Location = new System.Drawing.Point(117, 31);
            this.buttonLimitUp.Name = "buttonLimitUp";
            this.buttonLimitUp.Size = new System.Drawing.Size(80, 23);
            this.buttonLimitUp.TabIndex = 0;
            this.buttonLimitUp.Text = "Limit Up";
            this.buttonLimitUp.UseVisualStyleBackColor = true;
            this.buttonLimitUp.Click += new System.EventHandler(this.buttonLimitUp_Click);
            // 
            // buttonSetLimitDown
            // 
            this.buttonSetLimitDown.Location = new System.Drawing.Point(117, 89);
            this.buttonSetLimitDown.Name = "buttonSetLimitDown";
            this.buttonSetLimitDown.Size = new System.Drawing.Size(80, 23);
            this.buttonSetLimitDown.TabIndex = 2;
            this.buttonSetLimitDown.Text = "Limit Down";
            this.buttonSetLimitDown.UseVisualStyleBackColor = true;
            this.buttonSetLimitDown.Click += new System.EventHandler(this.buttonSetLimitDown_Click);
            // 
            // buttonSetLimitRight
            // 
            this.buttonSetLimitRight.Location = new System.Drawing.Point(168, 60);
            this.buttonSetLimitRight.Name = "buttonSetLimitRight";
            this.buttonSetLimitRight.Size = new System.Drawing.Size(71, 23);
            this.buttonSetLimitRight.TabIndex = 3;
            this.buttonSetLimitRight.Text = "Limit Right";
            this.buttonSetLimitRight.UseVisualStyleBackColor = true;
            this.buttonSetLimitRight.Click += new System.EventHandler(this.buttonSetLimitRight_Click);
            // 
            // buttonDisableLimits
            // 
            this.buttonDisableLimits.Location = new System.Drawing.Point(82, 20);
            this.buttonDisableLimits.Name = "buttonDisableLimits";
            this.buttonDisableLimits.Size = new System.Drawing.Size(70, 23);
            this.buttonDisableLimits.TabIndex = 5;
            this.buttonDisableLimits.Text = "Disable";
            this.buttonDisableLimits.UseVisualStyleBackColor = true;
            this.buttonDisableLimits.Click += new System.EventHandler(this.buttonDisableLimits_Click);
            // 
            // textBoxAcceleration
            // 
            this.textBoxAcceleration.Location = new System.Drawing.Point(91, 50);
            this.textBoxAcceleration.Name = "textBoxAcceleration";
            this.textBoxAcceleration.Size = new System.Drawing.Size(50, 20);
            this.textBoxAcceleration.TabIndex = 22;
            this.textBoxAcceleration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Acceleration:";
            // 
            // textBoxGain
            // 
            this.textBoxGain.Location = new System.Drawing.Point(91, 76);
            this.textBoxGain.Name = "textBoxGain";
            this.textBoxGain.Size = new System.Drawing.Size(50, 20);
            this.textBoxGain.TabIndex = 24;
            this.textBoxGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Gain:";
            // 
            // buttonSetSmoothing
            // 
            this.buttonSetSmoothing.Location = new System.Drawing.Point(147, 48);
            this.buttonSetSmoothing.Name = "buttonSetSmoothing";
            this.buttonSetSmoothing.Size = new System.Drawing.Size(51, 48);
            this.buttonSetSmoothing.TabIndex = 26;
            this.buttonSetSmoothing.Text = "Set";
            this.buttonSetSmoothing.UseVisualStyleBackColor = true;
            this.buttonSetSmoothing.Click += new System.EventHandler(this.buttonSetSmoothing_Click);
            // 
            // EneoUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox1);
            this.Name = "EneoUserControl";
            this.Size = new System.Drawing.Size(493, 320);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonSmoothingHigh;
        private System.Windows.Forms.Button buttonSmoothingNormal;
        private System.Windows.Forms.Button buttonSmoothingLow;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button buttonEnableLimits;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button buttonLimitLeft;
        private System.Windows.Forms.Button buttonLimitUp;
        private System.Windows.Forms.Button buttonSetLimitDown;
        private System.Windows.Forms.Button buttonSetLimitRight;
        private System.Windows.Forms.Button buttonDisableLimits;
        private System.Windows.Forms.Button buttonSetSmoothing;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxGain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAcceleration;
    }
}
