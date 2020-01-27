namespace Alturos.PanTilt.TestUI.CustomControl
{
    partial class CommandSequenceControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonStartTest = new System.Windows.Forms.Button();
            this.dataGridViewResult = new System.Windows.Forms.DataGridView();
            this.ColumnCreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnRepeat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSuccessful = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.buttonAbort = new System.Windows.Forms.Button();
            this.comboBoxSequence = new System.Windows.Forms.ComboBox();
            this.textBoxRepeat = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStartTest
            // 
            this.buttonStartTest.Location = new System.Drawing.Point(256, 3);
            this.buttonStartTest.Name = "buttonStartTest";
            this.buttonStartTest.Size = new System.Drawing.Size(54, 23);
            this.buttonStartTest.TabIndex = 0;
            this.buttonStartTest.Text = "Start";
            this.buttonStartTest.UseVisualStyleBackColor = true;
            this.buttonStartTest.Click += new System.EventHandler(this.buttonStartTest_Click);
            // 
            // dataGridViewResult
            // 
            this.dataGridViewResult.AllowUserToAddRows = false;
            this.dataGridViewResult.AllowUserToDeleteRows = false;
            this.dataGridViewResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCreateDate,
            this.ColumnName,
            this.ColumnRepeat,
            this.ColumnSuccessful,
            this.ColumnDescription});
            this.dataGridViewResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewResult.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewResult.Name = "dataGridViewResult";
            this.dataGridViewResult.ReadOnly = true;
            this.dataGridViewResult.Size = new System.Drawing.Size(600, 315);
            this.dataGridViewResult.TabIndex = 4;
            this.dataGridViewResult.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridViewResult_RowPrePaint);
            // 
            // ColumnCreateDate
            // 
            this.ColumnCreateDate.DataPropertyName = "CreateDate";
            dataGridViewCellStyle2.Format = "HH:mm:ss.fff";
            dataGridViewCellStyle2.NullValue = null;
            this.ColumnCreateDate.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnCreateDate.HeaderText = "CreateDate";
            this.ColumnCreateDate.Name = "ColumnCreateDate";
            this.ColumnCreateDate.ReadOnly = true;
            // 
            // ColumnName
            // 
            this.ColumnName.DataPropertyName = "Name";
            this.ColumnName.HeaderText = "Name";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.ReadOnly = true;
            this.ColumnName.Width = 150;
            // 
            // ColumnRepeat
            // 
            this.ColumnRepeat.DataPropertyName = "Repeat";
            this.ColumnRepeat.HeaderText = "Repeat";
            this.ColumnRepeat.Name = "ColumnRepeat";
            this.ColumnRepeat.ReadOnly = true;
            this.ColumnRepeat.Width = 50;
            // 
            // ColumnSuccessful
            // 
            this.ColumnSuccessful.DataPropertyName = "Successful";
            this.ColumnSuccessful.HeaderText = "Successful";
            this.ColumnSuccessful.Name = "ColumnSuccessful";
            this.ColumnSuccessful.ReadOnly = true;
            this.ColumnSuccessful.Width = 70;
            // 
            // ColumnDescription
            // 
            this.ColumnDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnDescription.DataPropertyName = "Description";
            this.ColumnDescription.HeaderText = "Description";
            this.ColumnDescription.Name = "ColumnDescription";
            this.ColumnDescription.ReadOnly = true;
            this.ColumnDescription.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxRepeat);
            this.splitContainer1.Panel1.Controls.Add(this.buttonAbort);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxSequence);
            this.splitContainer1.Panel1.Controls.Add(this.buttonStartTest);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewResult);
            this.splitContainer1.Size = new System.Drawing.Size(600, 350);
            this.splitContainer1.SplitterDistance = 31;
            this.splitContainer1.TabIndex = 5;
            // 
            // buttonAbort
            // 
            this.buttonAbort.Location = new System.Drawing.Point(316, 3);
            this.buttonAbort.Name = "buttonAbort";
            this.buttonAbort.Size = new System.Drawing.Size(54, 23);
            this.buttonAbort.TabIndex = 2;
            this.buttonAbort.Text = "Abort";
            this.buttonAbort.UseVisualStyleBackColor = true;
            this.buttonAbort.Click += new System.EventHandler(this.buttonAbort_Click);
            // 
            // comboBoxSequence
            // 
            this.comboBoxSequence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSequence.FormattingEnabled = true;
            this.comboBoxSequence.Location = new System.Drawing.Point(3, 5);
            this.comboBoxSequence.Name = "comboBoxSequence";
            this.comboBoxSequence.Size = new System.Drawing.Size(154, 21);
            this.comboBoxSequence.TabIndex = 1;
            // 
            // textBoxRepeat
            // 
            this.textBoxRepeat.Location = new System.Drawing.Point(214, 5);
            this.textBoxRepeat.Name = "textBoxRepeat";
            this.textBoxRepeat.Size = new System.Drawing.Size(36, 20);
            this.textBoxRepeat.TabIndex = 3;
            this.textBoxRepeat.Text = "10";
            this.textBoxRepeat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(163, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Repeat:";
            // 
            // CommandSequenceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "CommandSequenceControl";
            this.Size = new System.Drawing.Size(600, 350);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonStartTest;
        private System.Windows.Forms.DataGridView dataGridViewResult;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox comboBoxSequence;
        private System.Windows.Forms.Button buttonAbort;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCreateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRepeat;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnSuccessful;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxRepeat;
    }
}
