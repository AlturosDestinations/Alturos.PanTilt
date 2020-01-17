namespace Alturos.PanTilt.TestUI.CustomControl
{
    partial class FeedbackAnalyzeControl
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.chartAnalyze = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buttonStart = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelInfo = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageChart = new System.Windows.Forms.TabPage();
            this.tabPageData = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColumnTimestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPanPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTiltPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPanSpeedCommand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPanSpeedCalculated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPanSpeedDifference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTiltSpeedCommand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTiltSpeedCalculated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTiltSpeedDifference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.chartAnalyze)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageChart.SuspendLayout();
            this.tabPageData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // chartAnalyze
            // 
            chartArea3.Name = "ChartArea1";
            chartArea4.Name = "ChartArea2";
            this.chartAnalyze.ChartAreas.Add(chartArea3);
            this.chartAnalyze.ChartAreas.Add(chartArea4);
            this.chartAnalyze.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chartAnalyze.Legends.Add(legend2);
            this.chartAnalyze.Location = new System.Drawing.Point(3, 3);
            this.chartAnalyze.Name = "chartAnalyze";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series5.Legend = "Legend1";
            series5.Name = "Feedback Pan";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series6.Legend = "Legend1";
            series6.Name = "Command Pan";
            series7.ChartArea = "ChartArea2";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series7.Legend = "Legend1";
            series7.Name = "Feedback Tilt";
            series8.ChartArea = "ChartArea2";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series8.Legend = "Legend1";
            series8.Name = "Command Tilt";
            this.chartAnalyze.Series.Add(series5);
            this.chartAnalyze.Series.Add(series6);
            this.chartAnalyze.Series.Add(series7);
            this.chartAnalyze.Series.Add(series8);
            this.chartAnalyze.Size = new System.Drawing.Size(586, 279);
            this.chartAnalyze.TabIndex = 0;
            this.chartAnalyze.Text = "chart1";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(3, 3);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this.labelInfo);
            this.splitContainer1.Panel1.Controls.Add(this.buttonStart);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(600, 350);
            this.splitContainer1.SplitterDistance = 35;
            this.splitContainer1.TabIndex = 2;
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(84, 8);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(84, 13);
            this.labelInfo.TabIndex = 2;
            this.labelInfo.Text = "change by code";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageChart);
            this.tabControl1.Controls.Add(this.tabPageData);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(600, 311);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPageChart
            // 
            this.tabPageChart.Controls.Add(this.chartAnalyze);
            this.tabPageChart.Location = new System.Drawing.Point(4, 22);
            this.tabPageChart.Name = "tabPageChart";
            this.tabPageChart.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageChart.Size = new System.Drawing.Size(592, 285);
            this.tabPageChart.TabIndex = 0;
            this.tabPageChart.Text = "Chart";
            this.tabPageChart.UseVisualStyleBackColor = true;
            // 
            // tabPageData
            // 
            this.tabPageData.Controls.Add(this.dataGridView1);
            this.tabPageData.Location = new System.Drawing.Point(4, 22);
            this.tabPageData.Name = "tabPageData";
            this.tabPageData.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageData.Size = new System.Drawing.Size(592, 285);
            this.tabPageData.TabIndex = 1;
            this.tabPageData.Text = "Data";
            this.tabPageData.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnTimestamp,
            this.ColumnPanPosition,
            this.ColumnTiltPosition,
            this.ColumnPanSpeedCommand,
            this.ColumnPanSpeedCalculated,
            this.ColumnPanSpeedDifference,
            this.ColumnTiltSpeedCommand,
            this.ColumnTiltSpeedCalculated,
            this.ColumnTiltSpeedDifference});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(586, 279);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridView1_RowPrePaint);
            // 
            // ColumnTimestamp
            // 
            this.ColumnTimestamp.DataPropertyName = "Timestamp";
            dataGridViewCellStyle8.Format = "HH:mm:ss.fff";
            this.ColumnTimestamp.DefaultCellStyle = dataGridViewCellStyle8;
            this.ColumnTimestamp.HeaderText = "Timestamp";
            this.ColumnTimestamp.Name = "ColumnTimestamp";
            this.ColumnTimestamp.ReadOnly = true;
            this.ColumnTimestamp.Width = 80;
            // 
            // ColumnPanPosition
            // 
            this.ColumnPanPosition.DataPropertyName = "PanPosition";
            this.ColumnPanPosition.HeaderText = "Pan Position";
            this.ColumnPanPosition.Name = "ColumnPanPosition";
            this.ColumnPanPosition.ReadOnly = true;
            this.ColumnPanPosition.Width = 80;
            // 
            // ColumnTiltPosition
            // 
            this.ColumnTiltPosition.DataPropertyName = "TiltPosition";
            this.ColumnTiltPosition.HeaderText = "Tilt Position";
            this.ColumnTiltPosition.Name = "ColumnTiltPosition";
            this.ColumnTiltPosition.ReadOnly = true;
            this.ColumnTiltPosition.Width = 80;
            // 
            // ColumnPanSpeedCommand
            // 
            this.ColumnPanSpeedCommand.DataPropertyName = "PanSpeedCommand";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "0.000";
            this.ColumnPanSpeedCommand.DefaultCellStyle = dataGridViewCellStyle9;
            this.ColumnPanSpeedCommand.HeaderText = "Pan Speed Command";
            this.ColumnPanSpeedCommand.Name = "ColumnPanSpeedCommand";
            this.ColumnPanSpeedCommand.ReadOnly = true;
            this.ColumnPanSpeedCommand.Width = 90;
            // 
            // ColumnPanSpeedCalculated
            // 
            this.ColumnPanSpeedCalculated.DataPropertyName = "PanSpeedCalculated";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "0.000";
            this.ColumnPanSpeedCalculated.DefaultCellStyle = dataGridViewCellStyle10;
            this.ColumnPanSpeedCalculated.HeaderText = "Pan Speed Calculated";
            this.ColumnPanSpeedCalculated.Name = "ColumnPanSpeedCalculated";
            this.ColumnPanSpeedCalculated.ReadOnly = true;
            this.ColumnPanSpeedCalculated.Width = 80;
            // 
            // ColumnPanSpeedDifference
            // 
            this.ColumnPanSpeedDifference.DataPropertyName = "PanSpeedDifference";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "0.000";
            this.ColumnPanSpeedDifference.DefaultCellStyle = dataGridViewCellStyle11;
            this.ColumnPanSpeedDifference.HeaderText = "Pan Speed Difference";
            this.ColumnPanSpeedDifference.Name = "ColumnPanSpeedDifference";
            this.ColumnPanSpeedDifference.ReadOnly = true;
            this.ColumnPanSpeedDifference.Width = 70;
            // 
            // ColumnTiltSpeedCommand
            // 
            this.ColumnTiltSpeedCommand.DataPropertyName = "TiltSpeedCommand";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "0.000";
            this.ColumnTiltSpeedCommand.DefaultCellStyle = dataGridViewCellStyle12;
            this.ColumnTiltSpeedCommand.HeaderText = "Tilt Speed Command";
            this.ColumnTiltSpeedCommand.Name = "ColumnTiltSpeedCommand";
            this.ColumnTiltSpeedCommand.ReadOnly = true;
            this.ColumnTiltSpeedCommand.Width = 80;
            // 
            // ColumnTiltSpeedCalculated
            // 
            this.ColumnTiltSpeedCalculated.DataPropertyName = "TiltSpeedCalculated";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Format = "0.000";
            this.ColumnTiltSpeedCalculated.DefaultCellStyle = dataGridViewCellStyle13;
            this.ColumnTiltSpeedCalculated.HeaderText = "Tilt Speed Calculated";
            this.ColumnTiltSpeedCalculated.Name = "ColumnTiltSpeedCalculated";
            this.ColumnTiltSpeedCalculated.ReadOnly = true;
            this.ColumnTiltSpeedCalculated.Width = 80;
            // 
            // ColumnTiltSpeedDifference
            // 
            this.ColumnTiltSpeedDifference.DataPropertyName = "TiltSpeedDifference";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.Format = "0.000";
            this.ColumnTiltSpeedDifference.DefaultCellStyle = dataGridViewCellStyle14;
            this.ColumnTiltSpeedDifference.HeaderText = "Tilt Speed Difference";
            this.ColumnTiltSpeedDifference.Name = "ColumnTiltSpeedDifference";
            this.ColumnTiltSpeedDifference.ReadOnly = true;
            this.ColumnTiltSpeedDifference.Width = 70;
            // 
            // FeedbackAnalyzeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "FeedbackAnalyzeControl";
            this.Size = new System.Drawing.Size(600, 350);
            ((System.ComponentModel.ISupportInitialize)(this.chartAnalyze)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageChart.ResumeLayout(false);
            this.tabPageData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartAnalyze;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageChart;
        private System.Windows.Forms.TabPage tabPageData;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTimestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPanPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTiltPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPanSpeedCommand;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPanSpeedCalculated;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPanSpeedDifference;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTiltSpeedCommand;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTiltSpeedCalculated;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTiltSpeedDifference;
    }
}
