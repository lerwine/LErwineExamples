using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace ExampleGDIClassLibrary
{
    public class ImageFileInfo
    {
        public class ImageData
        {
            public bool HasAlpha { get; private set; }
            public bool IsIndexed { get; private set; }
            public ColorSpaceValue ColorSpace { get; private set; }
            public ImageType FileType { get; private set; }

            public static ImageData Create(ImageFileInfo fileInfo)
            {
                if (!fileInfo.FileInfo.Exists || fileInfo.ExtensionType != ImageType.Other)
                    return new ImageData { ColorSpace = ColorSpaceValue.Other, FileType = ImageType.Other, HasAlpha = false, IsIndexed = false };

                ImageData result;
                using (Image image = Image.FromFile(fileInfo.FileInfo.FullName))
                {
                    result = ImageData.Create(image);
                }

                return result;
            }

            public static ImageData Create(Image image)
            {
                ImageData result = new ImageData
                {
                    HasAlpha = (image.Flags & (int)(ImageFlags.HasAlpha)) != 0,
                    IsIndexed = image.PixelFormat.HasFlag(PixelFormat.Indexed)
                };

                if ((image.Flags & (int)(ImageFlags.ColorSpaceCmyk)) != 0)
                    result.ColorSpace = ColorSpaceValue.Cmyk;
                else if ((image.Flags & (int)(ImageFlags.ColorSpaceYcbcr)) != 0)
                    result.ColorSpace = ColorSpaceValue.Ycbcr;
                else if ((image.Flags & (int)(ImageFlags.ColorSpaceYcck)) != 0)
                    result.ColorSpace = ColorSpaceValue.Ycck;
                else if ((image.Flags & (int)(ImageFlags.ColorSpaceRgb)) != 0)
                    result.ColorSpace = ColorSpaceValue.Rgb;
                else if ((image.Flags & (int)(ImageFlags.ColorSpaceGray)) != 0)
                    result.ColorSpace = ColorSpaceValue.Gray;
                else
                    result.ColorSpace = ColorSpaceValue.Other;

                if (image.RawFormat.Equals(ImageFormat.Bmp))
                    result.FileType = ImageType.Bmp;
                else if (image.RawFormat.Equals(ImageFormat.Gif))
                    result.FileType = ImageType.Gif;
                else if (image.RawFormat.Equals(ImageFormat.Jpeg))
                    result.FileType = ImageType.Jpeg;
                else if (image.RawFormat.Equals(ImageFormat.Png))
                    result.FileType = ImageType.Png;
                else if (image.RawFormat.Equals(ImageFormat.Tiff))
                    result.FileType = ImageType.Tiff;
                else
                    result.FileType = ImageType.Other;

                return result;
            }
        }

        private ImageData _innerImageData = null;

        private ImageData InnerImageData
        {
            get
            {
                if (this._innerImageData == null)
                    this._innerImageData = ImageData.Create(this);

                return this._innerImageData;
            }
        }

        public bool HasAlpha { get { return this.InnerImageData.HasAlpha; } }

        public bool IsIndexed { get { return this.InnerImageData.IsIndexed; } }

        public ColorSpaceValue ColorSpace { get { return this.InnerImageData.ColorSpace; } }

        public ImageType FileType { get { return this.InnerImageData.FileType; } }

        private FileInfo _fileInfo;

        public FileInfo FileInfo
        {
            get { return _fileInfo; }
            set { _fileInfo = value; }
        }

        private ImageType _extensionType;

        public ImageType ExtensionType
        {
            get { return _extensionType; }
            set { _extensionType = value; }
        }

        public ImageFileInfo(FileInfo fileInfo)
        {
            if (fileInfo == null)
                throw new ArgumentNullException("fileInfo");
            
            this._fileInfo = fileInfo;

            switch (fileInfo.Extension.ToLower())
            {
                case "gif":
                    this.ExtensionType = ImageType.Gif;
                    break;
                case "jpg":
                case "jpeg":
                    this.ExtensionType = ImageType.Jpeg;
                    break;
                case "png":
                    this.ExtensionType = ImageType.Png;
                    break;
                case "tif":
                case "tiff":
                    this.ExtensionType = ImageType.Tiff;
                    break;
                case "bmp":
                    this.ExtensionType = ImageType.Bmp;
                    break;
                default:
                    this.ExtensionType = ImageType.Other;
                    break;
            }
        }

        public ImageFileInfo Export(ImageType imageType, FileInfo fileInfo)
        {
            if (fileInfo == null)
                throw new ArgumentNullException("fileInfo");

            string []expectedExtensions;
            ImageFormat imageFormat;
            switch (imageType)
            {
                case ImageType.Bmp:
                    expectedExtensions = new string[] { "bmp" };
                    imageFormat = ImageFormat.Bmp;
                    break;
                case ImageType.Gif:
                    expectedExtensions = new string[] { "gif" };
                    imageFormat = ImageFormat.Gif;
                    break;
                case ImageType.Jpeg:
                    expectedExtensions = new string[] { "jpg", "jpeg" };
                    imageFormat = ImageFormat.Jpeg;
                    break;
                case ImageType.Png:
                    expectedExtensions = new string[] { "png" };
                    imageFormat = ImageFormat.Png;
                    break;
                case ImageType.Tiff:
                    expectedExtensions = new string[] { "tif", "tiff" };
                    imageFormat = ImageFormat.Tiff;
                    break;
                default:
                    throw new ArgumentException("Unsupported image export type.", "imageType");
            }

            if (!expectedExtensions.Any(e => String.Compare(e, fileInfo.Extension, true) != 0))
                throw new ArgumentException("Unexpected file extension name.", "fileInfo");

            if (this.FileInfo.Exists)
            {
                using (Image image = Image.FromFile(this.FileInfo.FullName))
                {
                    image.Save(fileInfo.FullName, imageFormat);
                }
            }

            return new ImageFileInfo(fileInfo);
        }

        public void Refresh()
        {
            this._innerImageData = null;
            this._fileInfo.Refresh();
        }
    }
}
