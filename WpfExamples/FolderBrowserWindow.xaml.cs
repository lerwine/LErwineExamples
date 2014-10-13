using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Erwine.Leonard.T.Examples.WpfExamples
{
    /// <summary>
    /// Interaction logic for FolderBrowserWindow.xaml
    /// </summary>
    public partial class FolderBrowserWindow : Window
    {
        #region SelectedPath Property Members

        public const string PropertyName_SelectedPath = "SelectedPath";

        public static readonly DependencyProperty SelectedPathProperty =
            DependencyProperty.Register(FolderBrowserWindow.PropertyName_SelectedPath, typeof(string), typeof(FolderBrowserWindow),
                new PropertyMetadata("", (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as FolderBrowserWindow).OnSelectedPathPropertyChanged(e.OldValue as string, e.NewValue as string)));

        public string SelectedPath
        {
            get { return this.GetValue(FolderBrowserWindow.SelectedPathProperty) as string; }
            set { this.SetValue(FolderBrowserWindow.SelectedPathProperty, value); }
        }

        protected virtual void OnSelectedPathPropertyChanged(string oldValue, string newValue)
        {
            // TODO: Implement OnSelectedPathPropertyChanged Logic
        }

        #endregion

        #region ShowNewFolderButton Property Members

        public const string PropertyName_ShowNewFolderButton = "ShowNewFolderButton";

        public static readonly DependencyProperty ShowNewFolderButtonProperty =
            DependencyProperty.Register(FolderBrowserWindow.PropertyName_ShowNewFolderButton, typeof(bool), typeof(FolderBrowserWindow),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as FolderBrowserWindow).OnShowNewFolderButtonPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool ShowNewFolderButton
        {
            get { return (bool)(this.GetValue(FolderBrowserWindow.ShowNewFolderButtonProperty)); }
            set { this.SetValue(FolderBrowserWindow.ShowNewFolderButtonProperty, value); }
        }

        protected virtual void OnShowNewFolderButtonPropertyChanged(bool oldValue, bool newValue)
        {
            // TODO: Implement OnShowNewFolderButtonPropertyChanged Logic
        }

        #endregion

        #region ShowFolderTree Property Members

        public const string PropertyName_ShowFolderTree = "ShowFolderTree";

        public static readonly DependencyProperty ShowFolderTreeProperty =
            DependencyProperty.Register(FolderBrowserWindow.PropertyName_ShowFolderTree, typeof(bool), typeof(FolderBrowserWindow),
                new PropertyMetadata(true, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as FolderBrowserWindow).OnShowFolderTreePropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool ShowFolderTree
        {
            get { return (bool)(this.GetValue(FolderBrowserWindow.ShowFolderTreeProperty)); }
            set { this.SetValue(FolderBrowserWindow.ShowFolderTreeProperty, value); }
        }

        protected virtual void OnShowFolderTreePropertyChanged(bool oldValue, bool newValue)
        {
            // TODO: Implement OnShowFolderTreePropertyChanged Logic
        }

        #endregion

        #region ShowFolderContents Property Members

        public const string PropertyName_ShowFolderContents = "ShowFolderContents";

        public static readonly DependencyProperty ShowFolderContentsProperty =
            DependencyProperty.Register(FolderBrowserWindow.PropertyName_ShowFolderContents, typeof(bool), typeof(FolderBrowserWindow),
                new PropertyMetadata(true, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as FolderBrowserWindow).OnShowFolderContentsPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool ShowFolderContents
        {
            get { return (bool)(this.GetValue(FolderBrowserWindow.ShowFolderContentsProperty)); }
            set { this.SetValue(FolderBrowserWindow.ShowFolderContentsProperty, value); }
        }

        protected virtual void OnShowFolderContentsPropertyChanged(bool oldValue, bool newValue)
        {
            // TODO: Implement OnShowFolderContentsPropertyChanged Logic
        }

        #endregion

        #region Description Property Members

        public const string PropertyName_Description = "Description";

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register(FolderBrowserWindow.PropertyName_Description, typeof(string), typeof(FolderBrowserWindow),
                new PropertyMetadata("", (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as FolderBrowserWindow).OnDescriptionPropertyChanged(e.OldValue as string, e.NewValue as string)));

        public string Description
        {
            get { return this.GetValue(FolderBrowserWindow.DescriptionProperty) as string; }
            set { this.SetValue(FolderBrowserWindow.DescriptionProperty, value); }
        }

        protected virtual void OnDescriptionPropertyChanged(string oldValue, string newValue)
        {
            // TODO: Implement OnDescriptionPropertyChanged Logic
        }

        #endregion

        public FolderBrowserWindow()
        {
            InitializeComponent();
        }
    }
}
