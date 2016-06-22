using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileObjectTest
{
    public class Class2
    {
        public int int1;
        public string str1;
        public double dbl1;

        private uint _dataLength;

        public Class2()
        {
            int1 = default(int);
            str1 = default(string);
            dbl1 = default(double);
        }

        public Class2(int i1, string s1, double d1)
        {
            int1 = i1;
            str1 = s1;
            dbl1 = d1;
        }

        public byte[] GetDataLength()
        {
            byte[] buffUint;

            buffUint =BitConverter.GetBytes(_dataLength);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(buffUint);
            }
            return buffUint;
        }

        public byte[] GetDataBytes()
        {
            List<byte> bufferFinal = new List<byte>();
            byte[] bufferInt, bufferDbl, bufferStr;


            bufferInt = BitConverter.GetBytes(int1);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bufferInt);
            bufferFinal.AddRange(bufferInt);

            bufferInt = BitConverter.GetBytes(str1.Length);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bufferInt);
            bufferFinal.AddRange(bufferInt);

            bufferStr = System.Text.Encoding.ASCII.GetBytes(str1);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bufferStr);
            bufferFinal.AddRange(bufferStr);
            
            bufferDbl = BitConverter.GetBytes(dbl1);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bufferDbl);
            bufferFinal.AddRange(bufferDbl);

            _dataLength = (uint)bufferFinal.Count;

            return bufferFinal.ToArray();
        }

        public void GetDataBytes(byte[] buff)
        {
            byte[] bufferInt1, bufferInt2, bufferDbl, bufferStr;
            int strLen = 0;

            bufferInt1 = new byte[4];
            bufferInt2 = new byte[4];
            bufferDbl = new byte[8];

            if (buff.Length > 16)
            {
                Array.Copy(buff, 0, bufferInt1, 0, 4);
                Array.Copy(buff, 4, bufferInt2, 0, 4);
                Array.Copy(buff, buff.Length - 8, bufferDbl, 0, 8);
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(bufferDbl);
                    Array.Reverse(bufferInt1);
                    Array.Reverse(bufferInt2);
                }
                int1 = 0;
                dbl1 = 0;
                str1 = "";
                int1 = BitConverter.ToInt32(bufferInt1, 0);
                dbl1 = BitConverter.ToDouble(bufferDbl, 0);
                strLen = BitConverter.ToInt32(bufferInt2, 0);
                bufferStr = new byte[strLen];
                Array.Copy(buff, 8, bufferStr, 0, strLen);
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(bufferStr);
                }
                str1 = System.Text.Encoding.Default.GetString(bufferStr);
            }
        }
    }
}
