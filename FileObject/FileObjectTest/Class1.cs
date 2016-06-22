using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace FileObjectTest
{
    public class Class1
    {
        private static int thWidth, thHeight;
        private Image _img;
        private string _fileName;
        private bool _isThumbnailSet;

        public string FileName
        {
            set
            {
                _fileName = value;
                _img = Image.FromFile(_fileName);
            }
            get
            {
                return _fileName;
            }
        }
        
        public Image Img
        {
            get
            {
                return _img;
            }
            set
            {
                _img = value;
            }
        }

        public Class1()
        {
            _fileName = "";
            _img = null;
            _isThumbnailSet = false;
            thHeight = default(int);
            thWidth = default(int);
        }

        public Class1(string filename)
        {
            _fileName = filename;
            _img = Image.FromFile(_fileName);
            _isThumbnailSet = false;
            thHeight = default(int);
            thWidth = default(int);
        }

        private Image BytesToImage(byte[] imgBytes)
        {
            MemoryStream ms = new MemoryStream(imgBytes, 0, imgBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        private byte[]  ImageToBytes(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, format);
            byte[] imageBytes = ms.ToArray();
            //string base64String = Convert.ToBase64String(imageBytes);
            return imageBytes;
        }

        public static Bitmap ResizeToCell(Image image, int width, int height)
        {
            int nw = 0, nh = 0;

            ///Zooming to the desired size (the size of pictureBox obj)
            ///if the picture dimensions are bigger than obj
            ///or if the picture dimensions are smaller than obj
            nw = (image.Width < width) ? image.Width : width;
            nh = (image.Height < height) ? image.Height : height;

            if (image.Width < image.Height)
            {
                return ResizeImageProportionaly(image, -1, nh);
            }
            else
            {
                return ResizeImageProportionaly(image, nw, -1);
            }
        }

        public static Bitmap ResizeImageProportionaly(Image image, int width = -1, int height = -1)
        {
            double raport = 0;

            if ((width < 0) && (height < 0))
            {
                return null;
            }

            if (width < 0)
            {
                raport = image.Width * height / image.Height;
                width = (int)Math.Round(raport);

            }
            else if (height < 0)
            {
                raport = image.Height * width / image.Width;
                height = (int)Math.Round(raport);
            }
            else
            {
                return null;
            }

            return ResizeImage(image, width, height);
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        
        public int GetDataLength()
        {
            if (_isThumbnailSet)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public byte[] GetDataBytes()
        {
            if (_isThumbnailSet)
            {
                return default(byte[]);
            }
            else
            {
                return default(byte[]);
            }
        }

        public void SetThumbnailResolution(int width, int height)
        {
            if (!_isThumbnailSet)
            {
                thWidth = width;
                thHeight = height;
                _isThumbnailSet = true;
            }

        }

        public bool IsThumbnailSet()
        {
            return _isThumbnailSet;
        }
    }
}
