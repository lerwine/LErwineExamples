using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace Erwine.Leonard.T.Examples.WpfExamples.UserControls
{
    public abstract class FileOperationItem : DependencyObject
    {
        public static readonly DependencyProperty SourceFileProperty =
            DependencyProperty.Register("SourceFile", typeof(FileDetails), typeof(FileOperationItem), new PropertyMetadata(null));

        public FileDetails SourceFile
        {
            get { return (FileDetails)(this.GetValue(FileOperationItem.SourceFileProperty)); }
            set { this.SetValue(FileOperationItem.SourceFileProperty, value); }
        }

        public static readonly DependencyProperty ErrorMessageProperty =
            DependencyProperty.Register("ErrorMessage", typeof(string), typeof(FileOperationItem), new PropertyMetadata(""));

        public string ErrorMessage
        {
            get { return (string)(this.GetValue(FileOperationItem.ErrorMessageProperty)); }
            set { this.SetValue(FileOperationItem.ErrorMessageProperty, (value == null) ? "" : value); }
        }

        public static readonly DependencyProperty ErrorDetailProperty =
            DependencyProperty.Register("ErrorDetail", typeof(string), typeof(FileOperationItem), new PropertyMetadata(""));

        public string ErrorDetail
        {
            get { return (string)(this.GetValue(FileOperationItem.ErrorDetailProperty)); }
            set { this.SetValue(FileOperationItem.ErrorDetailProperty, (value == null) ? "" : value); }
        }

        public abstract OperationWorker CreateOperationWorker();

        public abstract Type GetWorkerType();

        public abstract OperationWorker EnqueueOrCreateWorker(IEnumerable<OperationWorker> collection);

        protected FileOperationItem() : base() { }

        protected FileOperationItem(FileInfo sourceFile)
            : this()
        {
            this.SourceFile = new FileDetails(sourceFile);
        }
    }

    public abstract class FileOperationItem<TOperationWorker, TInstance> : FileOperationItem
        where TInstance : FileOperationItem<TOperationWorker, TInstance>
        where TOperationWorker : OperationWorker<TInstance>, new()
    {
        public override Type GetWorkerType() { return typeof(TOperationWorker); }

        protected FileOperationItem() : base() { }

        protected FileOperationItem(FileInfo sourceFile) : base(sourceFile) { }

        public override OperationWorker CreateOperationWorker()
        {
            return this.OnCreateOperationWorker();
        }

        protected virtual TOperationWorker OnCreateOperationWorker()
        {
            return new TOperationWorker();
        }

        public override OperationWorker EnqueueOrCreateWorker(IEnumerable<OperationWorker> collection)
        {
            TOperationWorker result;
            TOperationWorker worker = collection.OfType<TOperationWorker>().FirstOrDefault();

            if (worker == null)
            {
                worker = this.OnCreateOperationWorker();
                result = worker;
            }
            else
                result = null;

            worker.Enqueue(this as TInstance);

            return result;
        }
    }
}
