using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace ImageFile
{
    public delegate void TProcessLog(string Message);

    public partial class ImgPannel : ScrollableControl
    {
        public TProcessLog ProcessLog = new TProcessLog(DummyProcessLog);

        string[] extensionsFor = { ".bmp", ".jpeg", ".jpg", ".png"};

        private byte _IsMouseDown;
        private int _MouseX, _OldMX;
        private int _MouseY, _OldMY;

        private string _Path;
        private int _ThumbnailNo;
        private int _MaxNoOfThumbnailOnWidth;
        private double _xRateDistance;
        private double _yRateDistance;
        private int _ObjectWidth;
        private int _ObjectHeight;

        public string Path
        {
            get
            {
                return this._Path;
            }
            set
            {
                this._Path = value;
                ReadFolderForFiles();
            }
        }

        public int ObjectWidth
        {
            get
            {
                return this._ObjectWidth;
            }
            set
            {
                this._ObjectWidth = value;
                this.RefreshObjectsOnParrent();
            }
        }

        public int ObjectHeight
        {
            get
            {
                return this._ObjectWidth;
            }
            set
            {
                this._ObjectHeight = value;
                this.RefreshObjectsOnParrent();
            }
        }

        public ImgPannel()
        {
            this.MouseClick += Control_MouseClick;
            this.Path = "";
            this._ObjectWidth = 32;
            this._ObjectHeight = 32;
            
            this._IsMouseDown = 0;
            this._MouseX = -1;
            this._MouseY = -1;
            this._OldMX = -1;
            this._OldMY = -1;
            this._ThumbnailNo = 0;
            this._MaxNoOfThumbnailOnWidth = 0;
            this._xRateDistance = 1.2;
            this._yRateDistance = 1.2;

        }

        private bool FileNameHasOneOfExtensions(string fileName, string[] extensions)
        {
            foreach (string stri in extensions)
                if (fileName.EndsWith(stri))
                    return true;
            return false;
        }

        private bool VerifyFileName(string stri)
        {
            FileStream file;
            if (!FileNameHasOneOfExtensions(stri, extensionsFor))
                return false;
            try
            {
                file = File.Open(stri, FileMode.Open);
            }
            catch
            {
                return false;
            }
            if (file.Length <= 0)
            {
                file.Close();
                return false;
            }
            file.Close();
            return true;
        }

        public void ReadFolderForFiles()
        {
            if (this._Path != "")
            {
                string[] FileList = Directory.GetFiles(this._Path);
                List<string> FilteredList = new List<string>();
                FilteredList = FileList.Where(stri => VerifyFileName(stri)).ToList();

                this.ProcessLog("Whole list: " + string.Join("\t", (FileList)) + Environment.NewLine);
                this.ProcessLog("Filtered: " + string.Join("\t", (FilteredList)) + Environment.NewLine);
                foreach (string stri2 in FilteredList)
                {
                    if (CheckIfFileIsImage(stri2))
                    {
                        thumbnail tbn = new thumbnail();
                        this.AddThumbnail(tbn);
                        tbn.Name = this._ThumbnailNo.ToString();
                        tbn.SetPath(stri2);
                        this.ProcessLog("Thumb added:" + tbn.Name);
                    }
                }
            }
        }
    
        public event EventHandler<EventArgs> WasClicked;

        private void Control_MouseClick(object sender, MouseEventArgs e)
        {
            var wasClicked = WasClicked;
            if (wasClicked != null)
            {
                WasClicked(this, EventArgs.Empty);
            }
            //Selection processing
            this.ProcessLog(e.Button.ToString() + "-" + e.Location.ToString());
        }

        public static void DrawRectangleInt(Graphics graphic, Pen pen, int x, int y, int width, int height)
        {
            int xx = 0, yy = 0, ww = 0, hh = 0;
            if (width < 0)
            {
                xx = x + width;
                ww = -width;
            }
            else
            {
                xx = x;
                ww = width;
            }
            if (height < 0)
            {
                yy = y + height;
                hh = -height;
            }
            else
            {
                yy = y;
                hh = height;
            }
            graphic.DrawRectangle(pen, xx, yy, ww, hh);
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            e.Control.MouseClick += Control_MouseClick;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _IsMouseDown = 1;
            var relativePoint = this.PointToClient(Cursor.Position);
            _MouseX = relativePoint.X;
            _MouseY = relativePoint.Y;
            _OldMX = _MouseX;
            _OldMY = _MouseY;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _IsMouseDown = 2;
            Pen pen1 = new Pen(this.Parent.BackColor);
            Graphics gph = this.CreateGraphics();
            DrawRectangleInt(gph, pen1, _MouseX, _MouseY, _OldMX, _OldMY);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_IsMouseDown == 1)
            {
                Graphics gph = this.CreateGraphics();
                var relativePoint = this.PointToClient(Cursor.Position);
                int xx = relativePoint.X - _MouseX;
                int yy = relativePoint.Y - _MouseY;
                Pen pen1 = new Pen(this.Parent.BackColor);
                Pen pen2 = new Pen(Color.Red);
                DrawRectangleInt( gph, pen1, _MouseX, _MouseY, _OldMX, _OldMY);
                DrawRectangleInt( gph, pen2, _MouseX, _MouseY, xx, yy);
                _OldMX = xx;
                _OldMY = yy;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.RefreshObjectsOnParrent();
        }

        public void GeneratePatrare()
        {
            this.Controls.Clear();

            for (int i = 0; i < 30; i++)
            {
                thumbnail tbn = new thumbnail();
                tbn.Name = /*"tbn" +*/ i.ToString();
                this.ProcessLog("Thumb added:" + tbn.Name);
                this.AddThumbnail(tbn);
            }
        }

        private void RefreshObjectsOnParrent()
        {
            int locIndexer = 0;
            Point pt;

            this._MaxNoOfThumbnailOnWidth = (this.Width - SystemInformation.VerticalScrollBarWidth) / Convert.ToInt32(Math.Round(this._ObjectWidth * (this._xRateDistance)));
            foreach (Control obj in this.Controls)
            {
                if (obj is thumbnail)
                {
                    pt = GetPositionToThisIndexThumbnail(locIndexer);
                    obj.Left = pt.X;
                    obj.Top = pt.Y;
                    obj.Width = this._ObjectWidth;
                    obj.Height = this._ObjectHeight;
                }
                locIndexer++;
            }
        }

        private void AddThumbnail(thumbnail tbnl)
        {
            Point pt = GetPositionToNextThumbnail();

            tbnl.Left = pt.X;
            tbnl.Top = pt.Y;
            tbnl.Width = this._ObjectWidth;
            tbnl.Height = this._ObjectHeight;
            this.Controls.Add(tbnl);
            this.Refresh();

            this._ThumbnailNo++;
        }

        private Point GetPositionToNextThumbnail()
        {
            //Point pt = new Point();

            //pt.X = (this._ThumbnailNo % this._MaxNoOfThumbnailOnWidth) * Convert.ToInt32(Math.Round(this.ObjectWidth * (this._xRateDistance)));
            //pt.Y = Convert.ToInt32(Math.Round(this.ObjectHeight * (this._yRateDistance))) * (this._ThumbnailNo / this._MaxNoOfThumbnailOnWidth) + Convert.ToInt32(Math.Round(this.ObjectHeight * (this._yRateDistance - 1)));

            return GetPositionToThisIndexThumbnail(this._ThumbnailNo);
        }

        private Point GetPositionToThisIndexThumbnail(int index)
        {
            Point pt = new Point();

            pt.X = (index % this._MaxNoOfThumbnailOnWidth) * Convert.ToInt32(Math.Round(this._ObjectWidth * (this._xRateDistance)));
            pt.Y = Convert.ToInt32(Math.Round(this._ObjectHeight * (this._yRateDistance))) * (index / this._MaxNoOfThumbnailOnWidth) + Convert.ToInt32(Math.Round(this._ObjectHeight * (this._yRateDistance - 1)));
            this.ProcessLog("New coords:" + pt.ToString());

            return pt;
        }

        private bool CheckIfFileIsImage(string file)
        {
            this.ProcessLog(file);
            return true;
        }

        public static void DummyProcessLog(string Message)
        { }
    }
}