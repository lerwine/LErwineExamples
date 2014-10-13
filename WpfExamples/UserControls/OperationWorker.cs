using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;

namespace Erwine.Leonard.T.Examples.WpfExamples.UserControls
{
    public abstract class OperationWorker : DependencyObject
    {
        public event EventHandler TotalCountChanged;
        public event EventHandler CountCompleteChanged;
        public event EventHandler PercentageCompleteChanged;
        public event EventHandler TotalMegaBytesChanged;
        public event EventHandler MegabytesProcessedChanged;
        public event EventHandler WorkerFinished;

        public static readonly DependencyProperty TotalCountProperty = DependencyProperty.Register("TotalCount", typeof(int), typeof(OperationWorker), 
            new PropertyMetadata(0, (DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as OperationWorker).OnTotalCountChanged()));

        public static readonly DependencyProperty CountCompleteProperty = DependencyProperty.Register("CountComplete", typeof(int), typeof(OperationWorker),
            new PropertyMetadata(0, (DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as OperationWorker).OnCountCompleteChanged()));

        public static readonly DependencyProperty PercentageCompleteProperty = DependencyProperty.Register("PercentageComplete", typeof(int), typeof(OperationWorker),
            new PropertyMetadata(0, (DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as OperationWorker).OnPercentageCompleteChanged()));

        public static readonly DependencyProperty TotalMegaBytesProperty = DependencyProperty.Register("TotalMegaBytes", typeof(double), typeof(OperationWorker), 
            new PropertyMetadata(0.0, (DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as OperationWorker).OnTotalMegaBytesChanged()));

        public static readonly DependencyProperty MegabytesProcessedProperty = DependencyProperty.Register("MegabytesProcessed", typeof(double), typeof(OperationWorker),
            new PropertyMetadata(0.0, (DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as OperationWorker).OnMegabytesProcessedChanged()));

        public int TotalCount
        {
            get { return (int)(this.GetValue(OperationWorker.TotalCountProperty)); }
            protected set { this.SetValue(OperationWorker.TotalCountProperty, value); }
        }

        public int CountComplete
        {
            get { return (int)(this.GetValue(OperationWorker.CountCompleteProperty)); }
            private set { this.SetValue(OperationWorker.CountCompleteProperty, value); }
        }

        public int PercentageComplete
        {
            get { return (int)(this.GetValue(OperationWorker.PercentageCompleteProperty)); }
            private set { this.SetValue(OperationWorker.PercentageCompleteProperty, value); }
        }

        public double TotalMegaBytes
        {
            get { return (double)(this.GetValue(OperationWorker.TotalMegaBytesProperty)); }
            set { this.SetValue(OperationWorker.TotalMegaBytesProperty, value); }
        }

        public double MegabytesProcessed
        {
            get { return (double)(this.GetValue(OperationWorker.MegabytesProcessedProperty)); }
            set { this.SetValue(OperationWorker.MegabytesProcessedProperty, value); }
        }

        protected virtual void OnTotalCountChanged()
        {
            if (this.TotalCountChanged != null)
                this.TotalCountChanged(this, EventArgs.Empty);
        }

        protected virtual void OnCountCompleteChanged()
        {
            if (this.CountCompleteChanged != null)
                this.CountCompleteChanged(this, EventArgs.Empty);
        }

        protected virtual void OnPercentageCompleteChanged()
        {
            if (this.PercentageCompleteChanged != null)
                this.PercentageCompleteChanged(this, EventArgs.Empty);
        }

        protected virtual void OnTotalMegaBytesChanged()
        {
            if (this.TotalMegaBytesChanged != null)
                this.TotalMegaBytesChanged(this, EventArgs.Empty);
        }

        protected virtual void OnMegabytesProcessedChanged()
        {
            if (this.MegabytesProcessedChanged != null)
                this.MegabytesProcessedChanged(this, EventArgs.Empty);
        }

        private object _syncRoot = new object();
        private Thread _currentThread = null;
        private Exception _unhandledError = null;

        public void Stop()
        {
            lock (this._syncRoot)
            {
                if (this._currentThread != null)
                    this._currentThread.Interrupt();
            };
        }

        public void Start()
        {
            Thread currentThread;

            lock (this._syncRoot)
            {
                if ((currentThread = this._currentThread) == null)
                    this._currentThread = new Thread(new ThreadStart(this._Start));
            };

            if (currentThread == null)
                this._currentThread.Start();
        }

        private void _Start()
        {
            try
            {
                int totalCount;
                while (this.ProcessNext())
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        this.PercentageComplete = Convert.ToInt32((this.MegabytesProcessed / this.TotalMegaBytes) * 100.0);
                    });
                    Thread.Sleep(0);
                }

                lock (this._syncRoot)
                {
                    this._currentThread = null;
                }

                totalCount = this.GetTotalCount();
                this.Dispatcher.Invoke(() =>
                {
                    this.TotalCount = totalCount;
                    this.CountComplete = totalCount;
                    this.PercentageComplete = 100;
                });
            }
            catch (ThreadInterruptedException)
            {
                lock (this._syncRoot)
                {
                    this._currentThread = null;
                }
            }
            catch (Exception exc)
            {
                lock (this._syncRoot)
                {
                    this._currentThread = null;
                }

                this._unhandledError = exc;
            }

            this.OnWorkerFinished();
        }

        protected virtual void OnWorkerFinished()
        {
            if (this.WorkerFinished != null)
                this.WorkerFinished(this, EventArgs.Empty);
        }

        private bool ProcessNext()
        {
            int totalCount = this.GetTotalCount();
            int index = 0;

            this.Dispatcher.Invoke(() =>
            {
                if (this.CountComplete > totalCount)
                    this.CountComplete = totalCount;
                index = this.CountComplete;
                if (this.TotalCount != totalCount)
                {
                    this.TotalCount = totalCount;
                    this.TotalMegaBytes = this.GetTotalMegaBytes();
                    this.MegabytesProcessed = this.GetSumMegabytes(index);
                }
            });

            if (index >= totalCount)
                return false;

            this.ProcessItemAt(index);

            this.Dispatcher.Invoke(() =>
            {
                this.CountComplete = index + 1;
                this.MegabytesProcessed += this.GetMegabytesAt(index);
            });

            return true;
        }

        protected abstract double GetMegabytesAt(int index);

        protected abstract double GetSumMegabytes(int index);

        protected abstract double GetTotalMegaBytes();

        protected abstract void ProcessItemAt(int index);

        protected abstract int GetTotalCount();
    }

    public abstract class OperationWorker<TOperationItem> : OperationWorker
        where TOperationItem : FileOperationItem
    {
        private ObservableCollection<TOperationItem> _innerItems = new ObservableCollection<TOperationItem>();
        private ReadOnlyObservableCollection<TOperationItem> _items = null;

        public Type GetItemType() { return typeof(TOperationItem); }

        public ReadOnlyObservableCollection<TOperationItem> Items
        {
            get
            {
                if (this._items == null)
                    this._items = new ReadOnlyObservableCollection<TOperationItem>(this._innerItems);

                return this._items;
            }
        }

        public void Enqueue(TOperationItem item)
        {
            this._innerItems.Add(item);
            this.Dispatcher.Invoke(() =>
            {
                this.TotalCount = this._innerItems.Count;
            });
        }

        protected override void ProcessItemAt(int index)
        {
            try
            {
                this.ProcessItem(this._innerItems[index]);
            }
            catch (Exception error)
            {
                this._innerItems[index].ErrorMessage = error.Message;
                this._innerItems[index].ErrorDetail = error.ToString();
            }
        }

        protected abstract void ProcessItem(TOperationItem item);

        protected override int GetTotalCount()
        {
            return this._innerItems.Count;
        }

        protected override double GetMegabytesAt(int index)
        {
            return (this._innerItems[index] == null || this._innerItems[index].SourceFile == null) ? 0.0 : this._innerItems[index].SourceFile.SizeMB;
        }

        protected override double GetSumMegabytes(int index)
        {
            return this._innerItems.Take(index).Where(i => i != null && i.SourceFile != null).Sum(i => i.SourceFile.SizeMB);
        }

        protected override double GetTotalMegaBytes()
        {
            return this._innerItems.Where(i => i != null && i.SourceFile != null).Sum(i => i.SourceFile.SizeMB);
        }
    }
}
