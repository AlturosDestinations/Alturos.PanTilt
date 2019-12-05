namespace Alturos.PanTilt.TestUI.CustomControl
{
    partial class ContiniousMovementControl
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelError = new System.Windows.Forms.Label();
            this.labelPanTiltPosition = new System.Windows.Forms.Label();
            this.panelAbsoluteMove = new System.Windows.Forms.Panel();
            this.checkBoxReverse = new System.Windows.Forms.CheckBox();
            this.numericUpDownPTDetailLevel = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownRadius = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxAutostart = new System.Windows.Forms.CheckBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.panelRelativeMove = new System.Windows.Forms.Panel();
            this.numericUpDownSeconds = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownDegrees = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxRelativeLoop = new System.Windows.Forms.CheckBox();
            this.buttonStartRelative = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonStop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelAbsoluteMove.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPTDetailLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRadius)).BeginInit();
            this.panelRelativeMove.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDegrees)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 108);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(700, 310);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panelAbsoluteMove, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelRelativeMove, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(706, 451);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelError);
            this.panel1.Controls.Add(this.labelPanTiltPosition);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 424);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(700, 24);
            this.panel1.TabIndex = 2;
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelError.ForeColor = System.Drawing.Color.Red;
            this.labelError.Location = new System.Drawing.Point(199, 4);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(98, 13);
            this.labelError.TabIndex = 1;
            this.labelError.Text = "change by code";
            // 
            // labelPanTiltPosition
            // 
            this.labelPanTiltPosition.AutoSize = true;
            this.labelPanTiltPosition.Location = new System.Drawing.Point(3, 4);
            this.labelPanTiltPosition.Name = "labelPanTiltPosition";
            this.labelPanTiltPosition.Size = new System.Drawing.Size(84, 13);
            this.labelPanTiltPosition.TabIndex = 0;
            this.labelPanTiltPosition.Text = "change by code";
            // 
            // panelAbsoluteMove
            // 
            this.panelAbsoluteMove.Controls.Add(this.checkBoxReverse);
            this.panelAbsoluteMove.Controls.Add(this.numericUpDownPTDetailLevel);
            this.panelAbsoluteMove.Controls.Add(this.label3);
            this.panelAbsoluteMove.Controls.Add(this.numericUpDownRadius);
            this.panelAbsoluteMove.Controls.Add(this.label2);
            this.panelAbsoluteMove.Controls.Add(this.checkBoxAutostart);
            this.panelAbsoluteMove.Controls.Add(this.buttonStart);
            this.panelAbsoluteMove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAbsoluteMove.Location = new System.Drawing.Point(3, 3);
            this.panelAbsoluteMove.Name = "panelAbsoluteMove";
            this.panelAbsoluteMove.Size = new System.Drawing.Size(700, 29);
            this.panelAbsoluteMove.TabIndex = 3;
            // 
            // checkBoxReverse
            // 
            this.checkBoxReverse.AutoSize = true;
            this.checkBoxReverse.Location = new System.Drawing.Point(631, 9);
            this.checkBoxReverse.Name = "checkBoxReverse";
            this.checkBoxReverse.Size = new System.Drawing.Size(66, 17);
            this.checkBoxReverse.TabIndex = 6;
            this.checkBoxReverse.Text = "Reverse";
            this.checkBoxReverse.UseVisualStyleBackColor = true;
            // 
            // numericUpDownPTDetailLevel
            // 
            this.numericUpDownPTDetailLevel.Location = new System.Drawing.Point(512, 6);
            this.numericUpDownPTDetailLevel.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDownPTDetailLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPTDetailLevel.Name = "numericUpDownPTDetailLevel";
            this.numericUpDownPTDetailLevel.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownPTDetailLevel.TabIndex = 5;
            this.numericUpDownPTDetailLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownPTDetailLevel.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(443, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Detail Level";
            // 
            // numericUpDownRadius
            // 
            this.numericUpDownRadius.Location = new System.Drawing.Point(330, 5);
            this.numericUpDownRadius.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.numericUpDownRadius.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownRadius.Name = "numericUpDownRadius";
            this.numericUpDownRadius.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownRadius.TabIndex = 3;
            this.numericUpDownRadius.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownRadius.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(255, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Circle Radius";
            // 
            // checkBoxAutostart
            // 
            this.checkBoxAutostart.AutoSize = true;
            this.checkBoxAutostart.Location = new System.Drawing.Point(140, 8);
            this.checkBoxAutostart.Name = "checkBoxAutostart";
            this.checkBoxAutostart.Size = new System.Drawing.Size(96, 17);
            this.checkBoxAutostart.TabIndex = 1;
            this.checkBoxAutostart.Text = "Use Test Loop";
            this.checkBoxAutostart.UseVisualStyleBackColor = true;
            this.checkBoxAutostart.CheckedChanged += new System.EventHandler(this.checkBoxAutostart_CheckedChanged);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(3, 4);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(120, 23);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start Absolute";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // panelRelativeMove
            // 
            this.panelRelativeMove.Controls.Add(this.numericUpDownSeconds);
            this.panelRelativeMove.Controls.Add(this.label4);
            this.panelRelativeMove.Controls.Add(this.numericUpDownDegrees);
            this.panelRelativeMove.Controls.Add(this.label5);
            this.panelRelativeMove.Controls.Add(this.checkBoxRelativeLoop);
            this.panelRelativeMove.Controls.Add(this.buttonStartRelative);
            this.panelRelativeMove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRelativeMove.Location = new System.Drawing.Point(3, 38);
            this.panelRelativeMove.Name = "panelRelativeMove";
            this.panelRelativeMove.Size = new System.Drawing.Size(700, 29);
            this.panelRelativeMove.TabIndex = 4;
            // 
            // numericUpDownSeconds
            // 
            this.numericUpDownSeconds.DecimalPlaces = 1;
            this.numericUpDownSeconds.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownSeconds.Location = new System.Drawing.Point(512, 4);
            this.numericUpDownSeconds.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDownSeconds.Name = "numericUpDownSeconds";
            this.numericUpDownSeconds.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownSeconds.TabIndex = 34;
            this.numericUpDownSeconds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownSeconds.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(394, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Seconds per Direction";
            // 
            // numericUpDownDegrees
            // 
            this.numericUpDownDegrees.Location = new System.Drawing.Point(330, 4);
            this.numericUpDownDegrees.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numericUpDownDegrees.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownDegrees.Name = "numericUpDownDegrees";
            this.numericUpDownDegrees.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownDegrees.TabIndex = 30;
            this.numericUpDownDegrees.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownDegrees.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(242, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Degree per sec";
            // 
            // checkBoxRelativeLoop
            // 
            this.checkBoxRelativeLoop.AutoSize = true;
            this.checkBoxRelativeLoop.Location = new System.Drawing.Point(140, 7);
            this.checkBoxRelativeLoop.Name = "checkBoxRelativeLoop";
            this.checkBoxRelativeLoop.Size = new System.Drawing.Size(96, 17);
            this.checkBoxRelativeLoop.TabIndex = 24;
            this.checkBoxRelativeLoop.Text = "Use Test Loop";
            this.checkBoxRelativeLoop.UseVisualStyleBackColor = true;
            this.checkBoxRelativeLoop.CheckedChanged += new System.EventHandler(this.checkBoxRelativeLoop_CheckedChanged);
            // 
            // buttonStartRelative
            // 
            this.buttonStartRelative.Location = new System.Drawing.Point(3, 3);
            this.buttonStartRelative.Name = "buttonStartRelative";
            this.buttonStartRelative.Size = new System.Drawing.Size(120, 23);
            this.buttonStartRelative.TabIndex = 18;
            this.buttonStartRelative.Text = "Start Relative";
            this.buttonStartRelative.UseVisualStyleBackColor = true;
            this.buttonStartRelative.Click += new System.EventHandler(this.buttonStartRelative_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.buttonStop);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 73);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(700, 29);
            this.panel3.TabIndex = 6;
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(2, 3);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(121, 23);
            this.buttonStop.TabIndex = 35;
            this.buttonStop.Text = "Stop Movement";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // ContiniousMovementControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ContiniousMovementControl";
            this.Size = new System.Drawing.Size(706, 451);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelAbsoluteMove.ResumeLayout(false);
            this.panelAbsoluteMove.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPTDetailLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRadius)).EndInit();
            this.panelRelativeMove.ResumeLayout(false);
            this.panelRelativeMove.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDegrees)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelPanTiltPosition;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.Panel panelAbsoluteMove;
        private System.Windows.Forms.NumericUpDown numericUpDownPTDetailLevel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownRadius;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxAutostart;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Panel panelRelativeMove;
        private System.Windows.Forms.Button buttonStartRelative;
        private System.Windows.Forms.CheckBox checkBoxRelativeLoop;
        private System.Windows.Forms.NumericUpDown numericUpDownDegrees;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownSeconds;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox checkBoxReverse;
    }
}
