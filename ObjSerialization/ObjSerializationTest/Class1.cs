using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjSerializationTest
{
    [Serializable()]
    public class Class1
    {
        int i1;
        string s1;
        double d1;

        public Class1()
        {
            i1 = 0;
            s1 = "";
            d1 = 0.0D;
        }

        public Class1(int i, string s, double d)
        {
            i1 = i;
            s1 = s;
            d1 = d;
        }
    }
}
