using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;

namespace Erwine.Leonard.T.Examples.WpfExamples.UserControls
{
    public class FileOperationViewModel : DependencyObject
    {
        private Collection<OperationWorker> _workers = new Collection<OperationWorker>();

        public static readonly DependencyProperty PercentageCompleteProperty =
            DependencyProperty.Register("PercentageComplete", typeof(int), typeof(FileOperationViewModel), new PropertyMetadata(0));

        public static readonly DependencyProperty TotalFilesProperty =
            DependencyProperty.Register("TotalFiles", typeof(int), typeof(FileOperationViewModel), new PropertyMetadata(0));

        public static readonly DependencyProperty CurrentIndexProperty =
            DependencyProperty.Register("CurrentIndex", typeof(int), typeof(FileOperationViewModel), new PropertyMetadata(0));

        public static readonly DependencyProperty FilesCompletedProperty =
            DependencyProperty.Register("FilesCompleted", typeof(int), typeof(FileOperationViewModel), new PropertyMetadata(0));

        public static readonly DependencyProperty TotalMegabytesProperty =
            DependencyProperty.Register("TotalMegabytes", typeof(long), typeof(FileOperationViewModel), new PropertyMetadata(0L));

        public static readonly DependencyProperty MegabytesCopiedProperty =
            DependencyProperty.Register("MegabytesCopied", typeof(long), typeof(FileOperationViewModel), new PropertyMetadata(0L));

        public static readonly DependencyProperty StatusMesageProperty =
            DependencyProperty.Register("StatusMesage", typeof(string), typeof(FileOperationViewModel), new PropertyMetadata(""));

        public int PercentageComplete
        {
            get { return (int)(this.GetValue(FileOperationViewModel.PercentageCompleteProperty)); }
            set { this.SetValue(FileOperationViewModel.PercentageCompleteProperty, value); }
        }

        public int TotalFiles
        {
            get { return (int)(this.GetValue(FileOperationViewModel.TotalFilesProperty)); }
            set { this.SetValue(FileOperationViewModel.TotalFilesProperty, value); }
        }

        public int CurrentIndex
        {
            get { return (int)(this.GetValue(FileOperationViewModel.CurrentIndexProperty)); }
            set { this.SetValue(FileOperationViewModel.CurrentIndexProperty, value); }
        }
        
        public int FilesCompleted
        {
            get { return (int)(this.GetValue(FileOperationViewModel.FilesCompletedProperty)); }
            set { this.SetValue(FileOperationViewModel.FilesCompletedProperty, value); }
        }

        public long TotalMegabytes
        {
            get { return (long)(this.GetValue(FileOperationViewModel.TotalMegabytesProperty)); }
            set { this.SetValue(FileOperationViewModel.TotalMegabytesProperty, value); }
        }

        public long MegabytesCopied
        {
            get { return (long)(this.GetValue(FileOperationViewModel.MegabytesCopiedProperty)); }
            set { this.SetValue(FileOperationViewModel.MegabytesCopiedProperty, value); }
        }

        public string StatusMesage
        {
            get { return (string)(this.GetValue(FileOperationViewModel.StatusMesageProperty)); }
            set { this.SetValue(FileOperationViewModel.StatusMesageProperty, (value == null) ? "" : value); }
        }
        
        private object _syncRoot = new object();

        public void CopyFile(FileInfo source, FileInfo destination)
        {
            this.AddOperation(new CopyFileOperation(source, destination));

            foreach (OperationWorker worker in this._workers)
                worker.Start();
        }

        internal void CopyFolder(DirectoryInfo source, DirectoryInfo destination, bool recursive)
        {
            this._CopyFolder(source, destination, recursive);
            
            foreach (OperationWorker worker in this._workers)
                worker.Start();
        }

        private void _CopyFolder(DirectoryInfo source, DirectoryInfo destination, bool recursive)
        {
            if (!source.Exists)
                return;

            foreach (FileInfo fi in source.GetFiles())
                this.AddOperation(new CopyFileOperation(fi, new FileInfo(Path.Combine(destination.FullName, fi.Name))));

            if (!recursive)
                return;

            foreach (DirectoryInfo di in destination.GetDirectories())
                this.CopyFolder(di, new DirectoryInfo(Path.Combine(destination.FullName, di.Name)), true);
        }

        private void AddOperation(FileOperationItem fileOperationItem)
        {
            OperationWorker newWorker;

            lock (this._workers)
            {
                newWorker = fileOperationItem.EnqueueOrCreateWorker(this._workers);
                if (newWorker != null) 
                    this._workers.Add(newWorker);
            }

            if (newWorker == null)
                return;

            newWorker.CountCompleteChanged += newWorker_CountCompleteChanged;
            newWorker.MegabytesProcessedChanged += newWorker_MegabytesProcessedChanged;
            newWorker.PercentageCompleteChanged += newWorker_PercentageCompleteChanged;
            newWorker.TotalCountChanged += newWorker_TotalCountChanged;
            newWorker.TotalMegaBytesChanged += newWorker_TotalMegaBytesChanged;
            newWorker.WorkerFinished += newWorker_WorkerFinished;
        }

        void newWorker_WorkerFinished(object sender, EventArgs e)
        {
            lock (this._workers)
            {
                OperationWorker newWorker = this._workers.FirstOrDefault(w => w == null || !Object.ReferenceEquals(w, sender));
                this._workers.Remove(newWorker);
                newWorker.CountCompleteChanged -= newWorker_CountCompleteChanged;
                newWorker.MegabytesProcessedChanged -= newWorker_MegabytesProcessedChanged;
                newWorker.PercentageCompleteChanged -= newWorker_PercentageCompleteChanged;
                newWorker.TotalCountChanged -= newWorker_TotalCountChanged;
                newWorker.TotalMegaBytesChanged -= newWorker_TotalMegaBytesChanged;
                newWorker.WorkerFinished -= newWorker_WorkerFinished;
            }
        }

        void newWorker_TotalMegaBytesChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void newWorker_TotalCountChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void newWorker_PercentageCompleteChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void newWorker_MegabytesProcessedChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void newWorker_CountCompleteChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        internal void Abort()
        {
            throw new NotImplementedException();
        }
    }
}
