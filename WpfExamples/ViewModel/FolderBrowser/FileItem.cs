using System.IO;
using System.Windows;

namespace Erwine.Leonard.T.Examples.WpfExamples.ViewModel.FolderBrowser
{
    public class FileItem : FileSystemItem
    {
        #region Length Property Members

        public const string PropertyName_Length = "Length";

        public static readonly DependencyPropertyKey LengthPropertyKey =
            DependencyProperty.RegisterReadOnly(FileItem.PropertyName_Length, typeof(long), typeof(FileItem),
                new PropertyMetadata(0L));

        public static readonly DependencyProperty LengthProperty =
          FileItem.LengthPropertyKey.DependencyProperty;

        public long Length
        {
            get { return (long)(this.GetValue(FileItem.LengthProperty)); }
            private set { this.SetValue(FileItem.LengthPropertyKey, value); }
        }

        #endregion

        public FileItem() : base(false) { }

        public FileItem(FileInfo fileInfo)
            : base(false, fileInfo)
        {
            this.Length = fileInfo.Length;
        }
    }
}
