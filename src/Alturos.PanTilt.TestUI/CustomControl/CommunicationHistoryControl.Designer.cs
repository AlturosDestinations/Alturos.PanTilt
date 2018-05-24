namespace Alturos.PanTilt.TestUI.CustomControl
{
    partial class CommunicationHistoryControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewSend = new System.Windows.Forms.DataGridView();
            this.ColumnSendTimestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSendData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSendType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewReceive = new System.Windows.Forms.DataGridView();
            this.ColumnReceiveTimestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnReceiveData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnReceiveType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainerCommunication = new System.Windows.Forms.SplitContainer();
            this.groupBoxReceive = new System.Windows.Forms.GroupBox();
            this.groupBoxSend = new System.Windows.Forms.GroupBox();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.checkBoxRefresh = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReceive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCommunication)).BeginInit();
            this.splitContainerCommunication.Panel1.SuspendLayout();
            this.splitContainerCommunication.Panel2.SuspendLayout();
            this.splitContainerCommunication.SuspendLayout();
            this.groupBoxReceive.SuspendLayout();
            this.groupBoxSend.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewSend
            // 
            this.dataGridViewSend.AllowUserToAddRows = false;
            this.dataGridViewSend.AllowUserToDeleteRows = false;
            this.dataGridViewSend.AllowUserToResizeColumns = false;
            this.dataGridViewSend.AllowUserToResizeRows = false;
            this.dataGridViewSend.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSend.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSendTimestamp,
            this.ColumnSendData,
            this.ColumnSendType});
            this.dataGridViewSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSend.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewSend.Name = "dataGridViewSend";
            this.dataGridViewSend.ReadOnly = true;
            this.dataGridViewSend.RowHeadersVisible = false;
            this.dataGridViewSend.Size = new System.Drawing.Size(511, 396);
            this.dataGridViewSend.TabIndex = 9;
            // 
            // ColumnSendTimestamp
            // 
            this.ColumnSendTimestamp.DataPropertyName = "Timestamp";
            dataGridViewCellStyle1.Format = "dd.MM.yyyy HH:mm:ss.fff";
            this.ColumnSendTimestamp.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnSendTimestamp.HeaderText = "Timestamp";
            this.ColumnSendTimestamp.Name = "ColumnSendTimestamp";
            this.ColumnSendTimestamp.ReadOnly = true;
            this.ColumnSendTimestamp.Width = 140;
            // 
            // ColumnSendData
            // 
            this.ColumnSendData.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnSendData.DataPropertyName = "DataReadable";
            this.ColumnSendData.HeaderText = "Data";
            this.ColumnSendData.Name = "ColumnSendData";
            this.ColumnSendData.ReadOnly = true;
            // 
            // ColumnSendType
            // 
            this.ColumnSendType.DataPropertyName = "Type";
            this.ColumnSendType.HeaderText = "Type";
            this.ColumnSendType.Name = "ColumnSendType";
            this.ColumnSendType.ReadOnly = true;
            this.ColumnSendType.Width = 200;
            // 
            // dataGridViewReceive
            // 
            this.dataGridViewReceive.AllowUserToAddRows = false;
            this.dataGridViewReceive.AllowUserToDeleteRows = false;
            this.dataGridViewReceive.AllowUserToResizeColumns = false;
            this.dataGridViewReceive.AllowUserToResizeRows = false;
            this.dataGridViewReceive.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReceive.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnReceiveTimestamp,
            this.ColumnReceiveData,
            this.ColumnReceiveType});
            this.dataGridViewReceive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewReceive.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewReceive.Name = "dataGridViewReceive";
            this.dataGridViewReceive.ReadOnly = true;
            this.dataGridViewReceive.RowHeadersVisible = false;
            this.dataGridViewReceive.Size = new System.Drawing.Size(511, 396);
            this.dataGridViewReceive.TabIndex = 8;
            // 
            // ColumnReceiveTimestamp
            // 
            this.ColumnReceiveTimestamp.DataPropertyName = "Timestamp";
            dataGridViewCellStyle2.Format = "dd.MM.yyyy HH:mm:ss.fff";
            this.ColumnReceiveTimestamp.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnReceiveTimestamp.HeaderText = "Timestamp";
            this.ColumnReceiveTimestamp.Name = "ColumnReceiveTimestamp";
            this.ColumnReceiveTimestamp.ReadOnly = true;
            this.ColumnReceiveTimestamp.Width = 140;
            // 
            // ColumnReceiveData
            // 
            this.ColumnReceiveData.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnReceiveData.DataPropertyName = "DataReadable";
            this.ColumnReceiveData.HeaderText = "Data";
            this.ColumnReceiveData.Name = "ColumnReceiveData";
            this.ColumnReceiveData.ReadOnly = true;
            // 
            // ColumnReceiveType
            // 
            this.ColumnReceiveType.DataPropertyName = "Type";
            this.ColumnReceiveType.HeaderText = "Type";
            this.ColumnReceiveType.Name = "ColumnReceiveType";
            this.ColumnReceiveType.ReadOnly = true;
            // 
            // splitContainerCommunication
            // 
            this.splitContainerCommunication.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerCommunication.Location = new System.Drawing.Point(0, 0);
            this.splitContainerCommunication.Name = "splitContainerCommunication";
            // 
            // splitContainerCommunication.Panel1
            // 
            this.splitContainerCommunication.Panel1.Controls.Add(this.groupBoxReceive);
            // 
            // splitContainerCommunication.Panel2
            // 
            this.splitContainerCommunication.Panel2.Controls.Add(this.groupBoxSend);
            this.splitContainerCommunication.Size = new System.Drawing.Size(1038, 415);
            this.splitContainerCommunication.SplitterDistance = 517;
            this.splitContainerCommunication.TabIndex = 12;
            // 
            // groupBoxReceive
            // 
            this.groupBoxReceive.Controls.Add(this.dataGridViewReceive);
            this.groupBoxReceive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxReceive.Location = new System.Drawing.Point(0, 0);
            this.groupBoxReceive.Name = "groupBoxReceive";
            this.groupBoxReceive.Size = new System.Drawing.Size(517, 415);
            this.groupBoxReceive.TabIndex = 0;
            this.groupBoxReceive.TabStop = false;
            this.groupBoxReceive.Text = "Receive";
            // 
            // groupBoxSend
            // 
            this.groupBoxSend.Controls.Add(this.dataGridViewSend);
            this.groupBoxSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSend.Location = new System.Drawing.Point(0, 0);
            this.groupBoxSend.Name = "groupBoxSend";
            this.groupBoxSend.Size = new System.Drawing.Size(517, 415);
            this.groupBoxSend.TabIndex = 1;
            this.groupBoxSend.TabStop = false;
            this.groupBoxSend.Text = "Send";
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.IsSplitterFixed = true;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.checkBoxRefresh);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainerCommunication);
            this.splitContainerMain.Size = new System.Drawing.Size(1038, 444);
            this.splitContainerMain.SplitterDistance = 25;
            this.splitContainerMain.TabIndex = 13;
            // 
            // checkBoxRefresh
            // 
            this.checkBoxRefresh.AutoSize = true;
            this.checkBoxRefresh.Checked = true;
            this.checkBoxRefresh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRefresh.Location = new System.Drawing.Point(3, 3);
            this.checkBoxRefresh.Name = "checkBoxRefresh";
            this.checkBoxRefresh.Size = new System.Drawing.Size(113, 17);
            this.checkBoxRefresh.TabIndex = 0;
            this.checkBoxRefresh.Text = "Automatic Refresh";
            this.checkBoxRefresh.UseVisualStyleBackColor = true;
            this.checkBoxRefresh.CheckedChanged += new System.EventHandler(this.checkBoxRefresh_CheckedChanged);
            // 
            // CommunicationHistoryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerMain);
            this.Name = "CommunicationHistoryControl";
            this.Size = new System.Drawing.Size(1038, 444);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReceive)).EndInit();
            this.splitContainerCommunication.Panel1.ResumeLayout(false);
            this.splitContainerCommunication.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCommunication)).EndInit();
            this.splitContainerCommunication.ResumeLayout(false);
            this.groupBoxReceive.ResumeLayout(false);
            this.groupBoxSend.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel1.PerformLayout();
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewSend;
        private System.Windows.Forms.DataGridView dataGridViewReceive;
        private System.Windows.Forms.SplitContainer splitContainerCommunication;
        private System.Windows.Forms.GroupBox groupBoxReceive;
        private System.Windows.Forms.GroupBox groupBoxSend;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReceiveTimestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReceiveData;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReceiveType;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.CheckBox checkBoxRefresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSendTimestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSendData;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSendType;
    }
}
