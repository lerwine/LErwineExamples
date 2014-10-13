using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Erwine.Leonard.T.Examples.WpfExamples.UserControls
{
    public class CopyFileWorker : OperationWorker<CopyFileOperation>
    {
        protected override void ProcessItem(CopyFileOperation item)
        {
            if (item == null || item.SourceFile == null || !item.SourceFile.Exists || item.DestinationFile == null ||
                    !item.DestinationFile.DirectoryExists)
                return;

            System.IO.File.Copy(item.SourceFile.FullPath, item.DestinationFile.FullPath);
        }
    }
}
