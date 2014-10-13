using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace Erwine.Leonard.T.Examples.WpfExamples.UserControls
{
    public class FileDetails : DependencyObject
    {
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(FileDetails), new PropertyMetadata(""));

        public string Name
        {
            get { return (string)(this.GetValue(FileDetails.NameProperty)); }
            set { this.SetValue(FileDetails.NameProperty, (value == null) ? "" : value); }
        }
        public static readonly DependencyProperty DirectoryNameProperty =
            DependencyProperty.Register("DirectoryName", typeof(string), typeof(FileDetails), new PropertyMetadata(""));

        public string DirectoryName
        {
            get { return (string)(this.GetValue(FileDetails.DirectoryNameProperty)); }
            set { this.SetValue(FileDetails.DirectoryNameProperty, (value == null) ? "" : value); }
        }
        public static readonly DependencyProperty ExtensionProperty =
            DependencyProperty.Register("Extension", typeof(string), typeof(FileDetails), new PropertyMetadata(""));

        public string Extension
        {
            get { return (string)(this.GetValue(FileDetails.ExtensionProperty)); }
            set { this.SetValue(FileDetails.ExtensionProperty, (value == null) ? "" : value); }
        }

        public static readonly DependencyProperty BaseNameProperty =
            DependencyProperty.Register("BaseName", typeof(string), typeof(FileDetails), new PropertyMetadata(""));

        public string BaseName
        {
            get { return (string)(this.GetValue(FileDetails.BaseNameProperty)); }
            set { this.SetValue(FileDetails.BaseNameProperty, (value == null) ? "" : value); }
        }

        public static readonly DependencyProperty FullPathProperty =
            DependencyProperty.Register("FullPath", typeof(string), typeof(FileDetails), new PropertyMetadata(""));

        public string FullPath
        {
            get { return (string)(this.GetValue(FileDetails.FullPathProperty)); }
            set { this.SetValue(FileDetails.FullPathProperty, (value == null) ? "" : value); }
        }

        public static readonly DependencyProperty SizeMBProperty =
            DependencyProperty.Register("SizeMB", typeof(double), typeof(FileDetails), new PropertyMetadata(0.0));

        public double SizeMB
        {
            get { return (double)(this.GetValue(FileDetails.SizeMBProperty)); }
            set { this.SetValue(FileDetails.SizeMBProperty, value); }
        }

        public static readonly DependencyProperty SizeBytesProperty =
            DependencyProperty.Register("SizeBytes", typeof(long), typeof(FileDetails), new PropertyMetadata(0L));

        public long SizeBytes
        {
            get { return (long)(this.GetValue(FileDetails.SizeBytesProperty)); }
            set { this.SetValue(FileDetails.SizeBytesProperty, value); }
        }

        public static readonly DependencyProperty CreationTimeProperty =
            DependencyProperty.Register("CreationTime", typeof(DateTime?), typeof(FileDetails), new PropertyMetadata(null));

        public DateTime? CreationTime
        {
            get { return (DateTime?)(this.GetValue(FileDetails.CreationTimeProperty)); }
            set { this.SetValue(FileDetails.CreationTimeProperty, value); }
        }

        public static readonly DependencyProperty LastWriteTimeProperty =
            DependencyProperty.Register("LastWriteTime", typeof(DateTime?), typeof(FileDetails), new PropertyMetadata(null));

        public DateTime? LastWriteTime
        {
            get { return (DateTime?)(this.GetValue(FileDetails.LastWriteTimeProperty)); }
            set { this.SetValue(FileDetails.LastWriteTimeProperty, value); }
        }

        public static readonly DependencyProperty ExistsProperty =
            DependencyProperty.Register("Exists", typeof(bool), typeof(FileDetails), new PropertyMetadata(false));

        public bool Exists
        {
            get { return (bool)(this.GetValue(FileDetails.ExistsProperty)); }
            set { this.SetValue(FileDetails.ExistsProperty, value); }
        }

        public static readonly DependencyProperty DirectoryExistsProperty =
            DependencyProperty.Register("DirectoryExists", typeof(bool), typeof(FileDetails), new PropertyMetadata(false));

        public bool DirectoryExists
        {
            get { return (bool)(this.GetValue(FileDetails.DirectoryExistsProperty)); }
            set { this.SetValue(FileDetails.DirectoryExistsProperty, value); }
        }

        public FileDetails() : base() { }

        public FileDetails(FileInfo fileInfo)
            : this()
        {
            if (fileInfo == null)
                return;

            this.Name = fileInfo.Name;
            this.DirectoryName = fileInfo.DirectoryName;
            this.FullPath = fileInfo.FullName;
            this.SizeBytes = fileInfo.Length;
            this.SizeMB = (Convert.ToDouble(this.SizeBytes) / 1024.0) / 1024.0;
            this.BaseName = Path.GetFileNameWithoutExtension(fileInfo.Name);
            this.Extension = fileInfo.Extension;
            this.Exists = fileInfo.Exists;
            this.DirectoryExists = fileInfo.Directory.Exists;
            this.CreationTime = fileInfo.CreationTime;
            this.LastWriteTime = fileInfo.LastWriteTime;
        }
    }
}
