using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ImageFile
{
    public class thumbnail : Control
    {
        #region public fields
        //public Label lb = new Label();
        public Boolean IsSelected = false;
        public Image Image;
        public string Path;
        #endregion

        #region private fields
        private Boolean _IsItNowSelected = false;
        private PictureBox _PicBox = new PictureBox();
        #endregion

        #region constructor
        public thumbnail()
        {
            Graphics gph = this.CreateGraphics();
            Pen pen = new Pen(Color.Black);

            gph.DrawRectangle(pen, 5, 5, this.Width - 5, this.Height - 5);
            this._PicBox.Left = 10;
            this._PicBox.Top = 10;
            this._PicBox.Width = this.Width - 20;
            this._PicBox.Height = this.Height - 20;
            this._PicBox.BackColor = Color.Red;
            this.Controls.Add(this._PicBox);
            //-----------------------------
            //this.lb.Left = 5;
            //this.lb.Top = 10;
            //this.lb.AutoSize = true;
            //this.lb.BackColor = Color.White;
            //this.Controls.Add(this.lb);
        }
        #endregion

        #region public
        public void SetPath(string value)
        {
            this.Path = value;
            this.LoadPictureFromFile();
        }
        #endregion

        #region override
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            /*
            this.IsSelected = !this.IsSelected;
            this._IsItNowSelected = true;
            this._UpdateSelection();
            */
            this.Focus();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this._PicBox.Left = 10;
            this._PicBox.Top = 10;
            this._PicBox.Width = this.Width - 20;
            this._PicBox.Height = this.Height - 20;
            if (this.Image != null)
            {
                this._PicBox.SizeMode = PictureBoxSizeMode.CenterImage;
                this._PicBox.Image = ResizeToCell(this.Image, this._PicBox.Width, this._PicBox.Height);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this.Focused)
            {
                ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.Red, ButtonBorderStyle.Solid);
            }
            /*
            Pen myPen = new System.Drawing.Pen(Color.Red);
   
            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(5, 5, this.Width - 10, this.Height - 10));
            this._UpdateSelection();
            */
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            this.Invalidate();
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            this.Invalidate();
        }
        #endregion

        #region private
        private void LoadPictureFromFile()
        {
            try
            {
                this.Image = Image.FromFile(this.Path);
            }
            finally
            {
                if (this.Image != null)
                {
                    if ((this.Image.Width > 0) && (this.Image.Height > 0) && (this._PicBox.Width > 0) && (this._PicBox.Height > 0))
                    {
                        this._PicBox.SizeMode = PictureBoxSizeMode.CenterImage;
                        this._PicBox.Image = ResizeToCell(this.Image, this._PicBox.Width, this._PicBox.Height);
                    }
                }
            }
        }

        private void _UpdateSelection()
        {
            Graphics gph = this.CreateGraphics();
            Pen myPen = new System.Drawing.Pen(Color.Black);

            if (this.IsSelected)
            {
                myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            }
            else
            {
                myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                myPen.Color = this.Parent.BackColor;
            }
            if (this._IsItNowSelected)
            {
                gph.DrawRectangle(myPen, new Rectangle(1, 1, this.Width - 2, this.Height - 2));
                this._IsItNowSelected = false;
            }
        }
        #endregion

        #region public static
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
            return 0;
        }

        public byte[] GetDataBytes()
        {
            Image teImage;


            return default(byte[]);
        }
        #endregion
    }
}
