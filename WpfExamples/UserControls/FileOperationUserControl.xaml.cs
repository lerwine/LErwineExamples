using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Erwine.Leonard.T.Examples.WpfExamples.UserControls
{
    /// <summary>
    /// Interaction logic for FileMoverUserControl.xaml
    /// </summary>
    public partial class FileOperationUserControl : UserControl
    {
        public void CopyFile(FileInfo source, FileInfo destination)
        {
            (this.FindResource("ViewModelObject") as FileOperationViewModel).CopyFile(source, destination);
        }

        public void CopyFolder(DirectoryInfo source, DirectoryInfo destination, bool recursive)
        {
            (this.FindResource("ViewModelObject") as FileOperationViewModel).CopyFolder(source, destination, recursive);
        }

        public void Abort()
        {
            (this.FindResource("ViewModelObject") as FileOperationViewModel).Abort();
        }

        public FileOperationUserControl()
        {
            this.InitializeComponent();
        }
    }
}
