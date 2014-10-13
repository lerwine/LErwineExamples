using ExampleGDIClassLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExampleGDIUnitTestProject.ExampleImages
{
    public class ImageData
    {
        public static readonly ImageData[] AllImages = new ImageData[]
        {
            new ImageData("Image01.BMP", ImageType.Bmp, 16, 16, PixelFormat.Format24bppRgb, false, 0),
            new ImageData("Image02.bmp", ImageType.Bmp, 16, 16, PixelFormat.Format24bppRgb, false, 0),
            new ImageData("Image03.bmp", ImageType.Bmp, 196, 190, PixelFormat.Format24bppRgb, false, 0),
            new ImageData("Image04.bmp", ImageType.Bmp, 32, 32, PixelFormat.Format32bppRgb, false, 0),
            new ImageData("Image05.gif", ImageType.Gif, 200, 200, PixelFormat.Format8bppIndexed, false, 40),
            new ImageData("Image06.gif", ImageType.Gif, 32, 22, PixelFormat.Format8bppIndexed, false, 2),
            new ImageData("Image07.JPEG", ImageType.Jpeg, 127, 26, PixelFormat.Format24bppRgb, false, 0),
            new ImageData("Image08.jpg", ImageType.Jpeg, 134, 71, PixelFormat.Format24bppRgb, false, 0),
            new ImageData("Image09.jpeg", ImageType.Jpeg, 148, 185, PixelFormat.Format24bppRgb, false, 0),
            new ImageData("Image10.JPG", ImageType.Jpeg, 196, 190, PixelFormat.Format24bppRgb, false, 0),
            new ImageData("Image11.png", ImageType.Png, 32, 32, PixelFormat.Format32bppArgb, true, 0),
            new ImageData("Image12.PNG", ImageType.Png, 196, 190, PixelFormat.Format24bppRgb, false, 0),
            new ImageData("Image13.TIF", ImageType.Png, 200, 200, PixelFormat.Format16bppGrayScale, false, 0),
            new ImageData("Image14.tif", ImageType.Png, 512, 512, PixelFormat.Format24bppRgb, false, 0),
            new ImageData("Image15.tiff", ImageType.Png, 480, 362, PixelFormat.Format24bppRgb, false, 0),
            new ImageData("Image16.gif", ImageType.Gif, 16, 16, PixelFormat.Format8bppIndexed, true, 0)
        };

        public string FileName { get; private set; }
        public ImageType Type { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public PixelFormat Format { get; private set; }
        public bool UsesAlpha { get; private set; }
        public int Frames { get; private set; }
        public string FilePath { get; private set; }

        public ImageData(string fileName, ImageType imageType, int width, int height, PixelFormat pixelFormat, bool usesAlpha, int frames)
        {
            this.FileName = fileName;
            this.Type = imageType;
            this.Width = width;
            this.Height = height;
            this.Format = pixelFormat;
            this.UsesAlpha = usesAlpha;
            this.Frames = frames;
            this.FilePath = Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "ExampleImages"), fileName);
            
        }

    }
}
