using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace Erwine.Leonard.T.Examples.WpfExamples.UserControls
{
    public class CopyFileOperation : FileOperationItem<CopyFileWorker, CopyFileOperation>
    {
        public static readonly DependencyProperty DestinationFileProperty =
            DependencyProperty.Register("DestinationFile", typeof(FileDetails), typeof(CopyFileOperation), new PropertyMetadata(null));

        public FileDetails DestinationFile
        {
            get { return (FileDetails)(this.GetValue(CopyFileOperation.DestinationFileProperty)); }
            set { this.SetValue(CopyFileOperation.DestinationFileProperty, value); }
        }

        public CopyFileOperation(FileInfo source, FileInfo destination)
            : base(source)
        {
            this.DestinationFile = new FileDetails(destination);
        }
    }
}
