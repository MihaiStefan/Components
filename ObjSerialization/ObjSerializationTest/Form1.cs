using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ObjSerialization;

namespace ObjSerializationTest
{
    public partial class Form1 : Form
    {
        ObjFile file = null;

        Class1 c1 = new Class1(0, "zero", 0.01);
        Class1 c2 = new Class1(1, "one", 0.02);
        Class1 c3 = new Class1(2, "two", 0.03);


        public Form1()
        {
            InitializeComponent();
            file = new ObjFile("c:\\temp\\ser.dtbs");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            file.SaveObject<Class1>(c1);
            file.SaveObject<Class1>(c2);
            file.SaveObject<Class1>(c3);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Class1 cf = null;
            cf = file.LoadOnce<Class1>();
        }
    }
}
