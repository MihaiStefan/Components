using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileObj
{
    public class ImageFileObject<T>: FileObject<T>
    {
        private uint _imgWidth;
        private uint _imgHeight;

        public uint ImgWidth
        {
            get
            {
                return _imgWidth;
            }
            set
            {
                if ((_imgWidth == default(uint)) && (value != default(int)))
                {
                    _imgWidth = value;
                }
                else
                {
                }
            }
        }

        public uint ImgHeight
        {
            get
            {
                return _imgHeight;
            }
            set
            {
                if ((_imgHeight == default(uint)) && (value != default(int)))
                {
                    _imgHeight = value;
                }
                else
                {
                }
            }
        }

        public ImageFileObject():base()
        {
            _imgHeight = default(uint);
            _imgWidth = default(uint);
        }

        protected override void _CheckClassMethods()
        {
 	        base._CheckClassMethods();
            Type tempType = typeof(T);
            if (!_HasMethod(tempType, "SetThumbnailResolution"))
            {
                throw new System.InvalidOperationException("You added a invalid class!");
            }

        }
    }
}
