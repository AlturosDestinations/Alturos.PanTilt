using Alturos.PanTilt.Eneo;
using Alturos.PanTilt.TestUI.Extension;
using Alturos.PanTilt.TestUI.Model;
using Alturos.PanTilt.Translator;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Alturos.PanTilt.TestUI.CustomControl
{
    public partial class CommunicationHistoryControl : UserControl
    {
        private int _limitSend = 1000;
        private int _limitReceive = 1000;
        private IFeedbackTranslator _feedbackTranslator;
        private List<DataPackage> _receivedData;
        private List<DataPackage> _sendData;
        private BindingSource _bindingSourceReceive = new BindingSource();
        private BindingSource _bindingSourceSend = new BindingSource();
        private object _syncLockSend = new object();
        private object _syncLockReceive = new object();
        private DateTime _lastRefresh;
        private ConcurrentQueue<DataPackage> _tempQueueSend = new ConcurrentQueue<DataPackage>();
        private ConcurrentQueue<DataPackage> _tempQueueReceive = new ConcurrentQueue<DataPackage>();
        
        public CommunicationHistoryControl()
        {
            this.InitializeComponent();

            this._receivedData = new List<DataPackage>();
            this._sendData = new List<DataPackage>();

            this._bindingSourceReceive.DataSource = this._receivedData;
            this.dataGridViewReceive.AutoGenerateColumns = false;
            this.dataGridViewReceive.DataSource = this._bindingSourceReceive;

            this._bindingSourceSend.DataSource = this._sendData;
            this.dataGridViewSend.AutoGenerateColumns = false;
            this.dataGridViewSend.DataSource = this._bindingSourceSend;
        }

        public void SetTranslator(IFeedbackTranslator feedbackTranslator)
        {
            this._feedbackTranslator = feedbackTranslator;
        }

        public void AddSendPackage(DataPackage item)
        {
            if (!this.checkBoxRefresh.Checked)
            {
                this.CleanupSendQueue();
                this._tempQueueSend.Enqueue(item);
                return;
            }

            lock (this._syncLockSend)
            {
                this.CleanupSend();
                this._sendData.Insert(0, item);
            }

            this.RefreshGrids();
        }

        public void AddReceivePackage(DataPackage item)
        {
            if (this._feedbackTranslator != null)
            {
                item.Type = this._feedbackTranslator.Translate(item.Data);
            }

            if (!this.checkBoxRefresh.Checked)
            {
                this.CleanupReceiveQueue();
                this._tempQueueReceive.Enqueue(item);
                return;
            }

            lock (this._syncLockReceive)
            {
                this.CleanupReceive();
                this._receivedData.Insert(0, item);
            }

            this.RefreshGrids();
        }

        #region Cleanup

        private void CleanupSendQueue()
        {
            while (this._tempQueueSend.Count > this._limitSend)
            {
                this._tempQueueSend.TryDequeue(out DataPackage temp);
            }
        }

        private void CleanupReceiveQueue()
        {
            while (this._tempQueueReceive.Count > this._limitReceive)
            {
                this._tempQueueReceive.TryDequeue(out DataPackage temp);
            }
        }

        private void CleanupSend()
        {
            var removeSize = 20;

            if (this._sendData.Count < this._limitSend)
            {
                return;
            }

            this._sendData.RemoveRange(this._limitSend - removeSize, removeSize);
        }

        private void CleanupReceive()
        {
            var removeSize = 20;

            if (this._receivedData.Count < this._limitReceive)
            {
                return;
            }

            this._receivedData.RemoveRange(this._limitReceive - removeSize, removeSize);
        }

        #endregion

        private void RefreshGrids()
        {
            if (!this.checkBoxRefresh.Checked)
            {
                return;
            }

            if (this._lastRefresh > DateTime.Now.AddMilliseconds(-500))
            {
                return;
            }

            this._lastRefresh = DateTime.Now;

            this.dataGridViewReceive.Invoke(o => this._bindingSourceReceive.ResetBindings(false));
            this.dataGridViewSend.Invoke(o => this._bindingSourceSend.ResetBindings(false));
        }

        private void MoveDataFromBuffer()
        {
            lock (this._syncLockSend)
            {
                this.CleanupSend();
                this.CleanupSendQueue();
                while (!this._tempQueueSend.IsEmpty)
                {
                    if (this._tempQueueSend.TryDequeue(out DataPackage item))
                    {
                        this._sendData.Insert(0, item);
                    }
                }
            }

            lock (this._syncLockReceive)
            {
                this.CleanupReceive();
                this.CleanupReceiveQueue();
                while (!this._tempQueueReceive.IsEmpty)
                {
                    if (this._tempQueueReceive.TryDequeue(out DataPackage item))
                    {
                        this._receivedData.Insert(0, item);
                    }
                }
            }

            this.RefreshGrids();
        }

        private void CheckBoxRefreshCheckedChanged(object sender, EventArgs e)
        {
            if (!this.checkBoxRefresh.Checked)
            {
                return;
            }

            this.MoveDataFromBuffer();
        }
    }
}
