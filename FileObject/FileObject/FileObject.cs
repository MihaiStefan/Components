using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace FileObj
{
    public class FileObject<T>
    {
        private string _fileName;
        private Stream _stream;
        private uint _count;

        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
                _UpdateFileName();
                _count = _ReadFileHeader();
                //_UpdateFileContent();
            }
        }

        public uint Count
        {
            get { return _count; }
        }

        public FileObject()
        {
            _fileName = "";
            _count = 0;
            _stream = null;
            _CheckClassMethods();
        }

        public FileObject(string fileName)
        {
            _fileName = fileName;
            _count = 0;
            _stream = null;
            _CheckClassMethods();
            if (_fileName != "")
            {
                _UpdateFileName();
                _count = _ReadFileHeader();
            }
        }

        ~FileObject()
        {
            if (_stream != null)
            {
                _stream.Close();
            }
        }

        public void AddCell(T instance)
        {
            MethodInfo GetDataBytes = typeof(T).GetMethod("GetDataBytes", Type.EmptyTypes);
            MethodInfo GetDataLength = typeof(T).GetMethod("GetDataLength", Type.EmptyTypes);

            object teData = GetDataBytes.Invoke(instance, null);
            byte[] data = (byte[])teData;
            object teLen = GetDataLength.Invoke(instance, null);
            byte[] len = (byte[])teLen;

            _count += 1;
            _UpdateFileHeader();

            _stream.Seek(0, SeekOrigin.End);
            _stream.Write(len, 0, 4);
            _stream.Write(data, 0, data.Length);
        }

        public T ReadCell()
        {
            byte[] buffer;

            buffer = _ReadCellRawData();

            T teObj = default(T);
            Type tempT = typeof(T);
            ConstructorInfo ctor = tempT.GetConstructor(Type.EmptyTypes);
            object instance = ctor.Invoke(new object[] { });

            MethodInfo GetDataBytes = typeof(T).GetMethod("GetDataBytes", new Type[] { typeof(byte[]) });
            object teData = GetDataBytes.Invoke(instance, new object[] { buffer });

            teObj = (T)instance;

            return teObj;
        }

        public T ReadCell(uint index)
        {
            byte[] buffer;

            _SetPosition(index);

            buffer = _ReadCellRawData();

            T teObj = default(T);
            Type tempT = typeof(T);
            ConstructorInfo ctor = tempT.GetConstructor(Type.EmptyTypes);
            object instance = ctor.Invoke(new object[] { });

            MethodInfo GetDataBytes = typeof(T).GetMethod("GetDataBytes", new Type[] { typeof(byte[]) });
            object teData = GetDataBytes.Invoke(instance, new object[] {buffer});

            teObj = (T)instance;

            return teObj;
        }

        private void _UpdateFileName()
        {
            if (_stream != null)
            {
                _stream.Close();
            }
            _stream = File.Open(_fileName, FileMode.OpenOrCreate);
        }

        private void _UpdateFileHeader()
        {
            byte[] buffUInt;
            if (_CallIfFileIsEmpty())
            {
                buffUInt = new byte[4] { 0, 0, 0, 0 };
                _stream.Write(buffUInt, 0, 4);
            }
            else
            {
                buffUInt = BitConverter.GetBytes(_count);
                _GetTheRightOrder(ref buffUInt);
                _stream.Position = 0;
                _stream.Write(buffUInt, 0, 4);
            }
        }

        private uint _ReadFileHeader()
        {
            byte[] buffUInt = new byte[4];
            if (_CallIfFileIsEmpty())
            {
                _UpdateFileHeader();
            }
            _stream.Seek(0, SeekOrigin.Begin);
            _stream.Read(buffUInt, 0, 4);
            _GetTheRightOrder(ref buffUInt);

            return BitConverter.ToUInt32(buffUInt, 0);
        }

        protected virtual void _CheckClassMethods()
        {
            Type tempType = typeof(T);
            if ((!_HasMethod(tempType, "GetDataLength")) && (!_HasMethod(tempType, "GetDataBytes")))
            {
                throw new System.InvalidOperationException("You added a invalid class!");
            }
        }

        protected bool _HasMethod(Type _theType, string methodName)
        {
            return _theType.GetMethod(methodName) != null;
        }

        private void _GetTheRightOrder(ref byte[] buff)
        {
            if (buff == null)
            {
                return;
            }
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(buff);
            }
        }

        private bool _CallIfFileIsEmpty()
        {
            return (_stream.Length == 0);
        }

        private void _SetPosition(uint index)
        {
            byte[] buffUInt = new byte[4];
            uint cellLen = 0;

            if ((_count - 1) < index)
            {
                throw new Exception("The index is bigger than the file counter.");
            }
            
            _stream.Position = 4;
            for (int i = 0; i < index; i++)
            {
                _stream.Read(buffUInt, 0, 4);
                _GetTheRightOrder(ref buffUInt);
                cellLen = BitConverter.ToUInt32(buffUInt, 0);
                _stream.Position += cellLen;
            }
        }

        private byte[] _ReadCellRawData()
        {
            byte[] buffer;
            byte[] buffUInt = new byte[4];

            _stream.Read(buffUInt, 0, 4);
            _GetTheRightOrder(ref buffUInt);
            buffer = new byte[BitConverter.ToUInt32(buffUInt, 0)];
            _stream.Read(buffer, 0, buffer.Length);

            return buffer;
        }
    }
}
