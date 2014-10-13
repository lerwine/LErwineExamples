using System;
using System.IO;
using System.Windows;

namespace Erwine.Leonard.T.Examples.WpfExamples.ViewModel.FolderBrowser
{
    public abstract class FileSystemItem : DependencyObject
    {
        #region IsFolder Property Members

        public const string PropertyName_IsFolder = "IsFolder";

        public static readonly DependencyPropertyKey IsFolderPropertyKey =
            DependencyProperty.RegisterReadOnly(FileSystemItem.PropertyName_IsFolder, typeof(bool), typeof(FileSystemItem),
                new PropertyMetadata(true));

        public static readonly DependencyProperty IsFolderProperty =
          FileSystemItem.IsFolderPropertyKey.DependencyProperty;

        public bool IsFolder
        {
            get { return (bool)(this.GetValue(FileSystemItem.IsFolderProperty)); }
            private set { this.SetValue(FileSystemItem.IsFolderPropertyKey, value); }
        }

        #endregion

        #region FullPath Property Members

        public const string PropertyName_FullPath = "FullPath";

        public static readonly DependencyPropertyKey FullPathPropertyKey =
            DependencyProperty.RegisterReadOnly(FileSystemItem.PropertyName_FullPath, typeof(string), typeof(FileSystemItem),
                new PropertyMetadata(""));

        public static readonly DependencyProperty FullPathProperty =
          FileSystemItem.FullPathPropertyKey.DependencyProperty;

        public string FullPath
        {
            get { return this.GetValue(FileSystemItem.FullPathProperty) as string; }
            private set { this.SetValue(FileSystemItem.FullPathPropertyKey, value); }
        }

        #endregion

        #region Name Property Members

        public const string PropertyName_Name = "Name";

        public static readonly DependencyPropertyKey NamePropertyKey =
            DependencyProperty.RegisterReadOnly(FileSystemItem.PropertyName_Name, typeof(string), typeof(FileSystemItem),
                new PropertyMetadata(""));

        public static readonly DependencyProperty NameProperty =
          FileSystemItem.NamePropertyKey.DependencyProperty;

        public string Name
        {
            get { return this.GetValue(FileSystemItem.NameProperty) as string; }
            private set { this.SetValue(FileSystemItem.NamePropertyKey, value); }
        }

        #endregion

        #region Extension Property Members

        public const string PropertyName_Extension = "Extension";

        public static readonly DependencyPropertyKey ExtensionPropertyKey =
            DependencyProperty.RegisterReadOnly(FileSystemItem.PropertyName_Extension, typeof(string), typeof(FileSystemItem),
                new PropertyMetadata(""));

        public static readonly DependencyProperty ExtensionProperty =
          FileSystemItem.ExtensionPropertyKey.DependencyProperty;

        public string Extension
        {
            get { return this.GetValue(FileSystemItem.ExtensionProperty) as string; }
            private set { this.SetValue(FileSystemItem.ExtensionPropertyKey, value); }
        }

        #endregion

        #region CreationTime Property Members

        public const string PropertyName_CreationTime = "CreationTime";

        public static readonly DependencyPropertyKey CreationTimePropertyKey =
            DependencyProperty.RegisterReadOnly(FileSystemItem.PropertyName_CreationTime, typeof(DateTime), typeof(FileSystemItem),
                new PropertyMetadata(DateTime.MinValue));

        public static readonly DependencyProperty CreationTimeProperty =
          FileSystemItem.CreationTimePropertyKey.DependencyProperty;

        public DateTime CreationTime
        {
            get { return (DateTime)(this.GetValue(FileSystemItem.CreationTimeProperty)); }
            private set { this.SetValue(FileSystemItem.CreationTimePropertyKey, value); }
        }

        #endregion

        #region LastWriteTime Property Members

        public const string PropertyName_LastWriteTime = "LastWriteTime";

        public static readonly DependencyPropertyKey LastWriteTimePropertyKey =
            DependencyProperty.RegisterReadOnly(FileSystemItem.PropertyName_LastWriteTime, typeof(DateTime), typeof(FileSystemItem),
                new PropertyMetadata(DateTime.Now));

        public static readonly DependencyProperty LastWriteTimeProperty =
          FileSystemItem.LastWriteTimePropertyKey.DependencyProperty;

        public DateTime LastWriteTime
        {
            get { return (DateTime)(this.GetValue(FileSystemItem.LastWriteTimeProperty)); }
            private set { this.SetValue(FileSystemItem.LastWriteTimePropertyKey, value); }
        }

        #endregion

        protected FileSystemItem(bool isFolder)
        {
            this.IsFolder = isFolder;
        }

        protected FileSystemItem(bool isFolder, FileSystemInfo fileSystemInfo)
            : this(isFolder)
        {
            this.Name = fileSystemInfo.Name;
            this.FullPath = fileSystemInfo.FullName;
            this.Extension = fileSystemInfo.Extension;
            this.CreationTime = fileSystemInfo.CreationTime;
            this.LastWriteTime = fileSystemInfo.LastWriteTime;
        }
    }
}
