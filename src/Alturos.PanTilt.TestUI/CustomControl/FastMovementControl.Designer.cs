namespace Alturos.PanTilt.TestUI.CustomControl
{
    partial class FastMovementControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelError = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownTime = new System.Windows.Forms.NumericUpDown();
            this.buttonStartFast = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridViewFastMovement = new System.Windows.Forms.DataGridView();
            this.ColumnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTimestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnRequest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTilt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFastMovement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(955, 535);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelError);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 508);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(949, 24);
            this.panel1.TabIndex = 2;
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelError.ForeColor = System.Drawing.Color.Red;
            this.labelError.Location = new System.Drawing.Point(4, 6);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(98, 13);
            this.labelError.TabIndex = 1;
            this.labelError.Text = "change by code";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.numericUpDownTime);
            this.panel2.Controls.Add(this.buttonStartFast);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(949, 29);
            this.panel2.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(326, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 37;
            this.label7.Text = "ms";
            // 
            // numericUpDownTime
            // 
            this.numericUpDownTime.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownTime.Location = new System.Drawing.Point(272, 4);
            this.numericUpDownTime.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numericUpDownTime.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownTime.Name = "numericUpDownTime";
            this.numericUpDownTime.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownTime.TabIndex = 36;
            this.numericUpDownTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownTime.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // buttonStartFast
            // 
            this.buttonStartFast.Location = new System.Drawing.Point(3, 3);
            this.buttonStartFast.Name = "buttonStartFast";
            this.buttonStartFast.Size = new System.Drawing.Size(120, 23);
            this.buttonStartFast.TabIndex = 35;
            this.buttonStartFast.Text = "Start Fast-Movement";
            this.buttonStartFast.UseVisualStyleBackColor = true;
            this.buttonStartFast.Click += new System.EventHandler(this.buttonStartFast_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(137, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Time between commands";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 38);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewFastMovement);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Size = new System.Drawing.Size(949, 464);
            this.splitContainer1.SplitterDistance = 316;
            this.splitContainer1.TabIndex = 6;
            // 
            // dataGridViewFastMovement
            // 
            this.dataGridViewFastMovement.AllowUserToAddRows = false;
            this.dataGridViewFastMovement.AllowUserToDeleteRows = false;
            this.dataGridViewFastMovement.AllowUserToResizeColumns = false;
            this.dataGridViewFastMovement.AllowUserToResizeRows = false;
            this.dataGridViewFastMovement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFastMovement.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnType,
            this.ColumnTimestamp,
            this.ColumnRequest,
            this.ColumnPan,
            this.ColumnTilt});
            this.dataGridViewFastMovement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewFastMovement.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewFastMovement.Name = "dataGridViewFastMovement";
            this.dataGridViewFastMovement.ReadOnly = true;
            this.dataGridViewFastMovement.RowHeadersVisible = false;
            this.dataGridViewFastMovement.Size = new System.Drawing.Size(316, 464);
            this.dataGridViewFastMovement.TabIndex = 0;
            this.dataGridViewFastMovement.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridViewFastMovement_RowPostPaint);
            // 
            // ColumnType
            // 
            this.ColumnType.DataPropertyName = "Type";
            this.ColumnType.HeaderText = "Type";
            this.ColumnType.Name = "ColumnType";
            this.ColumnType.ReadOnly = true;
            this.ColumnType.Width = 80;
            // 
            // ColumnTimestamp
            // 
            this.ColumnTimestamp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnTimestamp.DataPropertyName = "Timestamp";
            dataGridViewCellStyle3.Format = "HH:mm:ss.fff";
            this.ColumnTimestamp.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnTimestamp.HeaderText = "Timestamp";
            this.ColumnTimestamp.Name = "ColumnTimestamp";
            this.ColumnTimestamp.ReadOnly = true;
            // 
            // ColumnRequest
            // 
            this.ColumnRequest.DataPropertyName = "Request";
            this.ColumnRequest.HeaderText = "Request";
            this.ColumnRequest.Name = "ColumnRequest";
            this.ColumnRequest.ReadOnly = true;
            this.ColumnRequest.Width = 55;
            // 
            // ColumnPan
            // 
            this.ColumnPan.DataPropertyName = "Pan";
            this.ColumnPan.HeaderText = "Pan";
            this.ColumnPan.Name = "ColumnPan";
            this.ColumnPan.ReadOnly = true;
            this.ColumnPan.Width = 50;
            // 
            // ColumnTilt
            // 
            this.ColumnTilt.DataPropertyName = "Tilt";
            this.ColumnTilt.HeaderText = "Tilt";
            this.ColumnTilt.Name = "ColumnTilt";
            this.ColumnTilt.ReadOnly = true;
            this.ColumnTilt.Width = 50;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(629, 464);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // FastMovementControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FastMovementControl";
            this.Size = new System.Drawing.Size(955, 535);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTime)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFastMovement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonStartFast;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridViewFastMovement;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTimestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRequest;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPan;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTilt;
    }
}
