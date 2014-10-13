using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Erwine.Leonard.T.Examples.WpfExamples.ViewModel.FolderBrowser
{
    public class FolderBrowserVM : DependencyObject
    {
        #region SelectedFolder Property Members

        public event DependencyPropertyChangedEventHandler SelectedFolderPropertyChanged;

        public const string PropertyName_SelectedFolder = "SelectedFolder";

        public static readonly DependencyProperty SelectedFolderProperty =
            DependencyProperty.Register(FolderBrowserVM.PropertyName_SelectedFolder, typeof(FolderItem), typeof(FolderBrowserVM),
                new PropertyMetadata(null,
                    (DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as FolderBrowserVM).OnSelectedFolderPropertyChanged(e)));

        public FolderItem SelectedFolder
        {
            get { return (FolderItem)(this.GetValue(FolderBrowserVM.SelectedFolderProperty)); }
            set { this.SetValue(FolderBrowserVM.SelectedFolderProperty, value); }
        }

        protected virtual void OnSelectedFolderPropertyChanged(DependencyPropertyChangedEventArgs args)
        {
            this.OnSelectedFolderPropertyChanged((FolderItem)(args.OldValue), (FolderItem)(args.NewValue));

            if (this.SelectedFolderPropertyChanged != null)
                SelectedFolderPropertyChanged(this, args);
        }

        protected virtual void OnSelectedFolderPropertyChanged(FolderItem oldValue, FolderItem newValue)
        {
            this.SpecifiedPath = newValue.FullPath;
#warning Need to update folder contents
        }

        #endregion
   
        #region Folders Property Members

        public const string PropertyName_InnerFolders = "InnerFolders";
        public const string PropertyName_Folders = "Folders";

        private static readonly DependencyPropertyKey InnerFoldersPropertyKey =
            DependencyProperty.RegisterReadOnly(FolderBrowserVM.PropertyName_InnerFolders, typeof(ObservableCollection<FolderItem>), typeof(FolderBrowserVM),
                new PropertyMetadata(new ObservableCollection<FolderItem>()));

        private static readonly DependencyPropertyKey FoldersPropertyKey =
            DependencyProperty.RegisterReadOnly(FolderBrowserVM.PropertyName_Folders, typeof(ReadOnlyObservableCollection<FolderItem>), typeof(FolderBrowserVM),
                new PropertyMetadata(null));

        protected static readonly DependencyProperty InnerFoldersProperty =
            FolderBrowserVM.InnerFoldersPropertyKey.DependencyProperty;

        public static readonly DependencyProperty FoldersProperty =
            FolderBrowserVM.FoldersPropertyKey.DependencyProperty;

        protected ObservableCollection<FolderItem> InnerFolders
        {
            get { return (ObservableCollection<FolderItem>)(this.GetValue(FolderBrowserVM.InnerFoldersProperty)); }
            private set { this.SetValue(FolderBrowserVM.InnerFoldersPropertyKey, value); }
        }

        public ReadOnlyObservableCollection<FolderItem> Folders
        {
            get
            {
                ReadOnlyObservableCollection<FolderItem> value = (ReadOnlyObservableCollection<FolderItem>)(this.GetValue(FolderBrowserVM.FoldersProperty));

                if (value == null)
                {
                    // TODO: Need a way to delay retrieving folder contents if the drive is not ready, or a seprate class
                    foreach (FolderItem f in DriveInfo.GetDrives().Where(d => d.IsReady).Select(d => new FolderItem(d.RootDirectory)))
                        this.InnerFolders.Add(f);
                    value = new ReadOnlyObservableCollection<FolderItem>(this.InnerFolders);
                    this.SetValue(FolderBrowserVM.FoldersPropertyKey, value);
                }

                return value;
            }
            private set { this.SetValue(FolderBrowserVM.FoldersPropertyKey, value); }
        }

        #endregion

        #region FolderContents Property Members

        public const string PropertyName_InnerFolderContents = "InnerFolderContents";
        public const string PropertyName_FolderContents = "FolderContents";

        private static readonly DependencyPropertyKey InnerFolderContentsPropertyKey =
            DependencyProperty.RegisterReadOnly(FolderBrowserVM.PropertyName_InnerFolderContents, typeof(ObservableCollection<FileSystemItem>), typeof(FolderBrowserVM),
                new PropertyMetadata(new ObservableCollection<FileSystemItem>()));

        private static readonly DependencyPropertyKey FolderContentsPropertyKey =
            DependencyProperty.RegisterReadOnly(FolderBrowserVM.PropertyName_FolderContents, typeof(ReadOnlyObservableCollection<FileSystemItem>), typeof(FolderBrowserVM),
                new PropertyMetadata(null));

        protected static readonly DependencyProperty InnerFolderContentsProperty =
            FolderBrowserVM.InnerFolderContentsPropertyKey.DependencyProperty;

        public static readonly DependencyProperty FolderContentsProperty =
            FolderBrowserVM.FolderContentsPropertyKey.DependencyProperty;

        protected ObservableCollection<FileSystemItem> InnerFolderContents
        {
            get { return (ObservableCollection<FileSystemItem>)(this.GetValue(FolderBrowserVM.InnerFolderContentsProperty)); }
            private set { this.SetValue(FolderBrowserVM.InnerFolderContentsPropertyKey, value); }
        }

        public ReadOnlyObservableCollection<FileSystemItem> FolderContents
        {
            get
            {
                ReadOnlyObservableCollection<FileSystemItem> value = (ReadOnlyObservableCollection<FileSystemItem>)(this.GetValue(FolderBrowserVM.FolderContentsProperty));

                if (value == null)
                {
                    value = new ReadOnlyObservableCollection<FileSystemItem>(this.InnerFolderContents);
                    this.SetValue(FolderBrowserVM.FolderContentsPropertyKey, value);
                }

                return value;
            }
            private set { this.SetValue(FolderBrowserVM.FolderContentsPropertyKey, value); }
        }

        #endregion

        #region SpecifiedPath Property Members

        public const string PropertyName_SpecifiedPath = "SpecifiedPath";

        public static readonly DependencyProperty SpecifiedPathProperty =
            DependencyProperty.Register(FolderBrowserVM.PropertyName_SpecifiedPath, typeof(string), typeof(FolderBrowserVM),
                new PropertyMetadata("", (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as FolderBrowserVM).OnSpecifiedPathPropertyChanged(e.OldValue as string, e.NewValue as string)));

        public string SpecifiedPath
        {
            get { return this.GetValue(FolderBrowserVM.SpecifiedPathProperty) as string; }
            set { this.SetValue(FolderBrowserVM.SpecifiedPathProperty, value); }
        }

        protected virtual void OnSpecifiedPathPropertyChanged(string oldValue, string newValue)
        {
            if (String.IsNullOrWhiteSpace(newValue))
            {
                this.SpecifiedPathErrorMessage = "Path not specified";
                this.OkClickCommand.IsEnabled = false;
                return;
            }

            string message;
            try
            {
                message = (Directory.Exists(newValue)) ? "" : "Specified path does not exist.";
            }
            catch (Exception exc)
            {
                message = String.Format("Error validating specified path: {0}", exc.Message);
            }

            this.SpecifiedPathErrorMessage = message;

            if (String.IsNullOrWhiteSpace(message))
            {
                this.OkClickCommand.IsEnabled = true;
                this.SelectedFolder = this.EnsureFolder(new DirectoryInfo(newValue));
            }
        }

        private FolderItem EnsureFolder(DirectoryInfo directoryInfo)
        {
            if (directoryInfo.Parent != null)
            {
                FolderItem parent = this.EnsureFolder(directoryInfo.Parent);
                return parent.ChildFolders.First(f => f.Name == directoryInfo.Name);
            }

            FolderItem root = this.Folders.FirstOrDefault(f => f.FullPath == directoryInfo.FullName);
            if (root != null)
                return root;

            root = new FolderItem(directoryInfo);
            this.InnerFolders.Add(root);
            return root;
        }

        #endregion

        #region SpecifiedPathErrorMessage Property Members

        public const string PropertyName_SpecifiedPathErrorMessage = "SpecifiedPathErrorMessage";

        public static readonly DependencyProperty SpecifiedPathErrorMessageProperty =
            DependencyProperty.Register(FolderBrowserVM.PropertyName_SpecifiedPathErrorMessage, typeof(string), typeof(FolderBrowserVM),
                new PropertyMetadata(""));

        public string SpecifiedPathErrorMessage
        {
            get { return this.GetValue(FolderBrowserVM.SpecifiedPathErrorMessageProperty) as string; }
            set { this.SetValue(FolderBrowserVM.SpecifiedPathErrorMessageProperty, value); }
        }

        #endregion

        #region ShowFolderTree Property Members

        public const string PropertyName_ShowFolderTree = "ShowFolderTree";

        public static readonly DependencyProperty ShowFolderTreeProperty =
            DependencyProperty.Register(FolderBrowserVM.PropertyName_ShowFolderTree, typeof(bool), typeof(FolderBrowserVM),
                new PropertyMetadata(true, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as FolderBrowserVM).OnShowFolderTreePropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool ShowFolderTree
        {
            get { return (bool)(this.GetValue(FolderBrowserVM.ShowFolderTreeProperty)); }
            set { this.SetValue(FolderBrowserVM.ShowFolderTreeProperty, value); }
        }

        protected virtual void OnShowFolderTreePropertyChanged(bool oldValue, bool newValue)
        {
            // TODO: Implement OnShowFolderTreePropertyChanged Logic
        }

        #endregion

        #region ShowFolderContents Property Members

        public const string PropertyName_ShowFolderContents = "ShowFolderContents";

        public static readonly DependencyProperty ShowFolderContentsProperty =
            DependencyProperty.Register(FolderBrowserVM.PropertyName_ShowFolderContents, typeof(bool), typeof(FolderBrowserVM),
                new PropertyMetadata(true, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as FolderBrowserVM).OnShowFolderContentsPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool ShowFolderContents
        {
            get { return (bool)(this.GetValue(FolderBrowserVM.ShowFolderContentsProperty)); }
            set { this.SetValue(FolderBrowserVM.ShowFolderContentsProperty, value); }
        }

        protected virtual void OnShowFolderContentsPropertyChanged(bool oldValue, bool newValue)
        {
            // TODO: Implement OnShowFolderContentsPropertyChanged Logic
        }

        #endregion

        #region ShowNewFolderButton Property Members

        public const string PropertyName_ShowNewFolderButton = "ShowNewFolderButton";

        public static readonly DependencyProperty ShowNewFolderButtonProperty =
            DependencyProperty.Register(FolderBrowserVM.PropertyName_ShowNewFolderButton, typeof(bool), typeof(FolderBrowserVM),
                new PropertyMetadata(false));

        public bool ShowNewFolderButton
        {
            get { return (bool)(this.GetValue(FolderBrowserVM.ShowNewFolderButtonProperty)); }
            set { this.SetValue(FolderBrowserVM.ShowNewFolderButtonProperty, value); }
        }

        #endregion

        #region OkClick Command Property Members

        public event EventHandler OkClick;

        private Events.RelayCommand _okClickCommand = null;

        public Events.RelayCommand OkClickCommand
        {
            get
            {
                if (this._okClickCommand == null)
                {
                    this._okClickCommand = new Events.RelayCommand(this.OnOkClick);
                    this._okClickCommand.IsEnabled = false;
                }

                return this._okClickCommand;
            }
        }

        protected virtual void OnOkClick(object parameter)
        {
            if (this.OkClick != null)
                this.OkClick(this, EventArgs.Empty);
        }

        #endregion

        #region CancelClick Command Property Members

        public event EventHandler CancelClick;

        private Events.RelayCommand _cancelClickCommand = null;

        public Events.RelayCommand CancelClickCommand
        {
            get
            {
                if (this._cancelClickCommand == null)
                    this._cancelClickCommand = new Events.RelayCommand(this.OnCancelClick);

                return this._cancelClickCommand;
            }
        }

        protected virtual void OnCancelClick(object parameter)
        {
            if (this.CancelClick != null)
                this.CancelClick(this, EventArgs.Empty);
        }

        #endregion
    }
}
