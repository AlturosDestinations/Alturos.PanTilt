namespace Alturos.PanTilt.Calibration
{
    partial class Main
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonSpeedLogicPan = new System.Windows.Forms.Button();
            this.buttonSpeedLogicTilt = new System.Windows.Forms.Button();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.textBoxDegreePerSecond = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxDriveMilliseconds = new System.Windows.Forms.TextBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.textBoxStartPosition = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageCheck = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxAxisType = new System.Windows.Forms.ComboBox();
            this.tabPageAnalysis = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageCheck.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabPageAnalysis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(747, 528);
            this.dataGridView1.TabIndex = 0;
            // 
            // buttonSpeedLogicPan
            // 
            this.buttonSpeedLogicPan.Location = new System.Drawing.Point(3, 3);
            this.buttonSpeedLogicPan.Name = "buttonSpeedLogicPan";
            this.buttonSpeedLogicPan.Size = new System.Drawing.Size(194, 23);
            this.buttonSpeedLogicPan.TabIndex = 1;
            this.buttonSpeedLogicPan.Text = "Start Pan analysis";
            this.buttonSpeedLogicPan.UseVisualStyleBackColor = true;
            this.buttonSpeedLogicPan.Click += new System.EventHandler(this.buttonSpeedLogicPan_Click);
            // 
            // buttonSpeedLogicTilt
            // 
            this.buttonSpeedLogicTilt.Location = new System.Drawing.Point(3, 32);
            this.buttonSpeedLogicTilt.Name = "buttonSpeedLogicTilt";
            this.buttonSpeedLogicTilt.Size = new System.Drawing.Size(194, 23);
            this.buttonSpeedLogicTilt.TabIndex = 3;
            this.buttonSpeedLogicTilt.Text = "Start Tilit analysis";
            this.buttonSpeedLogicTilt.UseVisualStyleBackColor = true;
            this.buttonSpeedLogicTilt.Click += new System.EventHandler(this.buttonSpeedLogicTilt_Click);
            // 
            // buttonCheck
            // 
            this.buttonCheck.Location = new System.Drawing.Point(111, 108);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(83, 23);
            this.buttonCheck.TabIndex = 4;
            this.buttonCheck.Text = "Check Position";
            this.buttonCheck.UseVisualStyleBackColor = true;
            this.buttonCheck.Click += new System.EventHandler(this.buttonCheck_Click);
            // 
            // textBoxDegreePerSecond
            // 
            this.textBoxDegreePerSecond.Location = new System.Drawing.Point(111, 30);
            this.textBoxDegreePerSecond.Name = "textBoxDegreePerSecond";
            this.textBoxDegreePerSecond.Size = new System.Drawing.Size(83, 20);
            this.textBoxDegreePerSecond.TabIndex = 5;
            this.textBoxDegreePerSecond.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Degree per second:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Drive milliseconds:";
            // 
            // textBoxDriveMilliseconds
            // 
            this.textBoxDriveMilliseconds.Location = new System.Drawing.Point(111, 56);
            this.textBoxDriveMilliseconds.Name = "textBoxDriveMilliseconds";
            this.textBoxDriveMilliseconds.Size = new System.Drawing.Size(83, 20);
            this.textBoxDriveMilliseconds.TabIndex = 8;
            this.textBoxDriveMilliseconds.Text = "1000";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.Size = new System.Drawing.Size(747, 528);
            this.dataGridView2.TabIndex = 11;
            this.dataGridView2.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridView2_RowPrePaint);
            // 
            // textBoxStartPosition
            // 
            this.textBoxStartPosition.Location = new System.Drawing.Point(111, 82);
            this.textBoxStartPosition.Name = "textBoxStartPosition";
            this.textBoxStartPosition.Size = new System.Drawing.Size(83, 20);
            this.textBoxStartPosition.TabIndex = 12;
            this.textBoxStartPosition.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Start position:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageCheck);
            this.tabControl1.Controls.Add(this.tabPageAnalysis);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(965, 560);
            this.tabControl1.TabIndex = 14;
            // 
            // tabPageCheck
            // 
            this.tabPageCheck.Controls.Add(this.splitContainer2);
            this.tabPageCheck.Location = new System.Drawing.Point(4, 22);
            this.tabPageCheck.Name = "tabPageCheck";
            this.tabPageCheck.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCheck.Size = new System.Drawing.Size(957, 534);
            this.tabPageCheck.TabIndex = 1;
            this.tabPageCheck.Text = "Quick Check";
            this.tabPageCheck.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.comboBoxAxisType);
            this.splitContainer2.Panel1.Controls.Add(this.textBoxDegreePerSecond);
            this.splitContainer2.Panel1.Controls.Add(this.label4);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.textBoxStartPosition);
            this.splitContainer2.Panel1.Controls.Add(this.textBoxDriveMilliseconds);
            this.splitContainer2.Panel1.Controls.Add(this.buttonCheck);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dataGridView2);
            this.splitContainer2.Size = new System.Drawing.Size(951, 528);
            this.splitContainer2.SplitterDistance = 200;
            this.splitContainer2.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "AxisType:";
            // 
            // comboBoxAxisType
            // 
            this.comboBoxAxisType.FormattingEnabled = true;
            this.comboBoxAxisType.Location = new System.Drawing.Point(111, 3);
            this.comboBoxAxisType.Name = "comboBoxAxisType";
            this.comboBoxAxisType.Size = new System.Drawing.Size(83, 21);
            this.comboBoxAxisType.TabIndex = 14;
            // 
            // tabPageAnalysis
            // 
            this.tabPageAnalysis.Controls.Add(this.splitContainer1);
            this.tabPageAnalysis.Location = new System.Drawing.Point(4, 22);
            this.tabPageAnalysis.Name = "tabPageAnalysis";
            this.tabPageAnalysis.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAnalysis.Size = new System.Drawing.Size(957, 534);
            this.tabPageAnalysis.TabIndex = 0;
            this.tabPageAnalysis.Text = "Axis Analysis";
            this.tabPageAnalysis.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.buttonSpeedLogicPan);
            this.splitContainer1.Panel1.Controls.Add(this.buttonSpeedLogicTilt);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(951, 528);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 4;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 560);
            this.Controls.Add(this.tabControl1);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alturos.PanTilt.Calibration";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageCheck.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabPageAnalysis.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonSpeedLogicPan;
        private System.Windows.Forms.Button buttonSpeedLogicTilt;
        private System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.TextBox textBoxDegreePerSecond;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDriveMilliseconds;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TextBox textBoxStartPosition;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageAnalysis;
        private System.Windows.Forms.TabPage tabPageCheck;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxAxisType;
    }
}

