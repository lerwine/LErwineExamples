using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Erwine.Leonard.T.Examples.WpfExamples.ViewModel.FolderBrowser
{
    public class FolderItem : FileSystemItem
    {
        #region FolderCount Property Members

        public const string PropertyName_FolderCount = "FolderCount";

        public static readonly DependencyPropertyKey FolderCountPropertyKey =
            DependencyProperty.RegisterReadOnly(FolderItem.PropertyName_FolderCount, typeof(int), typeof(FolderItem),
                new PropertyMetadata(0));

        public static readonly DependencyProperty FolderCountProperty =
          FolderItem.FolderCountPropertyKey.DependencyProperty;

        public int FolderCount
        {
            get { return (int)(this.GetValue(FolderItem.FolderCountProperty)); }
            private set { this.SetValue(FolderItem.FolderCountPropertyKey, value); }
        }

        #endregion

        #region FileCount Property Members

        public const string PropertyName_FileCount = "FileCount";

        public static readonly DependencyPropertyKey FileCountPropertyKey =
            DependencyProperty.RegisterReadOnly(FolderItem.PropertyName_FileCount, typeof(int), typeof(FolderItem),
                new PropertyMetadata(0));

        public static readonly DependencyProperty FileCountProperty =
          FolderItem.FileCountPropertyKey.DependencyProperty;

        public int FileCount
        {
            get { return (int)(this.GetValue(FolderItem.FileCountProperty)); }
            private set { this.SetValue(FolderItem.FileCountPropertyKey, value); }
        }

        #endregion

        #region ChildFolders Property Members

        private bool _childFoldersProcessed = false;

        public const string PropertyName_InnerChildFolders = "InnerChildFolders";
        public const string PropertyName_ChildFolders = "ChildFolders";

        private static readonly DependencyPropertyKey InnerChildFoldersPropertyKey =
            DependencyProperty.RegisterReadOnly(FolderItem.PropertyName_InnerChildFolders, typeof(ObservableCollection<FolderItem>), typeof(FolderItem),
                new PropertyMetadata(new ObservableCollection<FolderItem>()));

        private static readonly DependencyPropertyKey ChildFoldersPropertyKey =
            DependencyProperty.RegisterReadOnly(FolderItem.PropertyName_ChildFolders, typeof(ReadOnlyObservableCollection<FolderItem>), typeof(FolderItem),
                new PropertyMetadata(null));

        protected static readonly DependencyProperty InnerChildFoldersProperty =
            FolderItem.InnerChildFoldersPropertyKey.DependencyProperty;

        public static readonly DependencyProperty ChildFoldersProperty =
            FolderItem.ChildFoldersPropertyKey.DependencyProperty;

        protected ObservableCollection<FolderItem> InnerChildFolders
        {
            get { return (ObservableCollection<FolderItem>)(this.GetValue(FolderItem.InnerChildFoldersProperty)); }
            private set { this.SetValue(FolderItem.InnerChildFoldersPropertyKey, value); }
        }

        public ReadOnlyObservableCollection<FolderItem> ChildFolders
        {
            get
            {
                ReadOnlyObservableCollection<FolderItem> value = (ReadOnlyObservableCollection<FolderItem>)(this.GetValue(FolderItem.ChildFoldersProperty));

                if (value == null)
                {
                    foreach (DirectoryInfo di in this._directories)
                    {
                        FolderItem item = new FolderItem(di);
                        this.InnerChildFolders.Add(item);
                    }
                    this._directories = null;
                    value = new ReadOnlyObservableCollection<FolderItem>(this.InnerChildFolders);
                    this.SetValue(FolderItem.ChildFoldersPropertyKey, value);
                }

                return value;
            }
            private set { this.SetValue(FolderItem.ChildFoldersPropertyKey, value); }
        }

        #endregion

        #region Files Property Members

        public const string PropertyName_InnerFiles = "InnerFiles";
        public const string PropertyName_Files = "Files";

        private static readonly DependencyPropertyKey InnerFilesPropertyKey =
            DependencyProperty.RegisterReadOnly(FolderItem.PropertyName_InnerFiles, typeof(ObservableCollection<FileItem>), typeof(FolderItem),
                new PropertyMetadata(new ObservableCollection<FileItem>()));

        private static readonly DependencyPropertyKey FilesPropertyKey =
            DependencyProperty.RegisterReadOnly(FolderItem.PropertyName_Files, typeof(ReadOnlyObservableCollection<FileItem>), typeof(FolderItem),
                new PropertyMetadata(null));

        protected static readonly DependencyProperty InnerFilesProperty =
            FolderItem.InnerFilesPropertyKey.DependencyProperty;

        public static readonly DependencyProperty FilesProperty =
            FolderItem.FilesPropertyKey.DependencyProperty;

        protected ObservableCollection<FileItem> InnerFiles
        {
            get { return (ObservableCollection<FileItem>)(this.GetValue(FolderItem.InnerFilesProperty)); }
            private set { this.SetValue(FolderItem.InnerFilesPropertyKey, value); }
        }

        public ReadOnlyObservableCollection<FileItem> Files
        {
            get
            {
                ReadOnlyObservableCollection<FileItem> value = (ReadOnlyObservableCollection<FileItem>)(this.GetValue(FolderItem.FilesProperty));

                if (value == null)
                {
                    foreach (FileInfo fi in this._files)
                    {
                        FileItem item = new FileItem(fi);
                        this.InnerFiles.Add(item);
                    }
                    this._files = null;
                    value = new ReadOnlyObservableCollection<FileItem>(this.InnerFiles);
                    this.SetValue(FolderItem.FilesPropertyKey, value);
                }

                return value;
            }
            private set { this.SetValue(FolderItem.FilesPropertyKey, value); }
        }

        #endregion

        #region AllItems Property Members

        public const string PropertyName_InnerAllItems = "InnerAllItems";
        public const string PropertyName_AllItems = "AllItems";

        private static readonly DependencyPropertyKey InnerAllItemsPropertyKey =
            DependencyProperty.RegisterReadOnly(FolderItem.PropertyName_InnerAllItems, typeof(ObservableCollection<FileSystemItem>), typeof(FolderItem),
                new PropertyMetadata(new ObservableCollection<FileSystemItem>()));

        private static readonly DependencyPropertyKey AllItemsPropertyKey =
            DependencyProperty.RegisterReadOnly(FolderItem.PropertyName_AllItems, typeof(ReadOnlyObservableCollection<FileSystemItem>), typeof(FolderItem),
                new PropertyMetadata(null));

        protected static readonly DependencyProperty InnerAllItemsProperty =
            FolderItem.InnerAllItemsPropertyKey.DependencyProperty;

        public static readonly DependencyProperty AllItemsProperty =
            FolderItem.AllItemsPropertyKey.DependencyProperty;

        protected ObservableCollection<FileSystemItem> InnerAllItems
        {
            get { return (ObservableCollection<FileSystemItem>)(this.GetValue(FolderItem.InnerAllItemsProperty)); }
            private set { this.SetValue(FolderItem.InnerAllItemsPropertyKey, value); }
        }

        public ReadOnlyObservableCollection<FileSystemItem> AllItems
        {
            get
            {
                ReadOnlyObservableCollection<FileSystemItem> value = (ReadOnlyObservableCollection<FileSystemItem>)(this.GetValue(FolderItem.AllItemsProperty));

                if (value == null)
                {
                    foreach (FolderItem item in this.ChildFolders)
                        this.InnerAllItems.Add(item);
                    foreach (FileItem item in this.Files)
                        this.InnerAllItems.Add(item);
                    value = new ReadOnlyObservableCollection<FileSystemItem>(this.InnerAllItems);
                    this.SetValue(FolderItem.AllItemsPropertyKey, value);
                }

                return value;
            }
            private set { this.SetValue(FolderItem.AllItemsPropertyKey, value); }
        }

        #endregion

        public FolderItem() : base(true) { }

        private DirectoryInfo[] _directories;
        private FileInfo[] _files;

        public FolderItem(DirectoryInfo directory)
            : base(true, directory)
        {
            this._directories = directory.GetDirectories();
            this.FolderCount = this._directories.Length;

            this._files = directory.GetFiles();
            this.FileCount = this.InnerFiles.Count;
        }
    }
}
