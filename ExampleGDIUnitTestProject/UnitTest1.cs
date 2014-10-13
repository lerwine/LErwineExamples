using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ExampleGDIClassLibrary;
using System.Collections.Generic;
using System.Reflection;

namespace ExampleGDIUnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string outputPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Output");
            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);
            foreach (ExampleImages.ImageData id in ExampleImages.ImageData.AllImages)
            {
                ImageFileInfo fi = new ImageFileInfo(new FileInfo(id.FilePath));
                if (fi.ExtensionType != ImageType.Gif)
                    fi.Export(ImageType.Gif, new FileInfo(Path.Combine(outputPath, Path.GetFileNameWithoutExtension(fi.FileInfo.Name) + ".gif")));
            }
        }
    }
}
