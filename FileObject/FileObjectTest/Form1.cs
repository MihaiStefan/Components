using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using FileObj;

namespace FileObjectTest
{
    public partial class Form1 : Form
    {
        FileObject<Class2> fobj;
        ImageFileObject<Class1> ifobj;

        Class1 c1_1;
        Class2[] c2_l = new Class2[4] { new Class2(1, "1234567890a", 3.1), 
                                        new Class2(2, "1234567890ab", 4.2),
                                        new Class2(3, "1234567890abc", 5.3),
                                        new Class2(4, "1234567890abcd", 6.4)};
        byte[] buffer1, buffer2;
        int index = 0;

        public Form1()
        {
            InitializeComponent();
            if (BitConverter.IsLittleEndian)
            {
                label1.BackColor = Color.Red;
            }
            else
            {
                label1.BackColor = Color.Blue;
            }
            fobj = new FileObject<Class2>();
            fobj.FileName = @"c:\Temp\aa";
            c1_1 = new Class1();
            ifobj = new ImageFileObject<Class1>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fobj.FileName = @"c:\Temp\aa";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fobj.FileName = @"c:\Temp\aa";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int tempInt;

            Int32.TryParse(textBox1.Text, out tempInt);
            buffer1 = BitConverter.GetBytes(tempInt);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer1);
            //textBox2.Text = System.Text.Encoding.Default.GetString(BitConverter.GetBytes(tempInt));
            textBox2.Text = BitConverter.ToString(buffer1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer1);
            textBox4.Text = textBox2.Text;
            textBox3.Text = BitConverter.ToInt32(buffer1, 0).ToString();
        }

        //private void button6_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog od = new OpenFileDialog();
        //    Image teImg;


        //    if (od.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        teImg = Image.FromFile(od.FileName);
        //        c1_1.Img = Class1.ResizeToCell(teImg, pictureBox1.Width, pictureBox1.Height);
        //        pictureBox1.Image = c1_1.Img;
        //        //textBox5.Text = BitConverter.ToString(c1_1.ImageBytes);
        //        File.WriteAllBytes("teimg.jpg", c1_1.ImageBytes);
        //        textBox5.Text = System.Text.Encoding.Default.GetString(c1_1.ImageBytes);
        //    }
        //}

        private void button7_Click(object sender, EventArgs e)
        {
            double td1;

            double.TryParse(textBox6.Text, out td1);
            buffer2 = BitConverter.GetBytes(td1);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer2);
            //textBox2.Text = System.Text.Encoding.Default.GetString(BitConverter.GetBytes(tempInt));
            textBox7.Text = BitConverter.ToString(buffer2);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer2);
            textBox4.Text = textBox7.Text;
            textBox3.Text = BitConverter.ToDouble(buffer2, 0).ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            byte[] buff;
            Class2 c2_1 = new Class2(10, "1234567890abcdef", 3.14);
            buff = c2_1.GetDataBytes();
            textBox5.Text = BitConverter.ToString(buff);
            c2_1.GetDataBytes(buff);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            fobj.AddCell(c2_l[index]);
            index += 1;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Class2 c2_1;

            //c2_1 = fobj.ReadCell();
            c2_1 = fobj.ReadCell(1);
            //fobj._SetPosition(2);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();

            if (od.ShowDialog() == DialogResult.OK)
            {
                c1_1.FileName = od.FileName;
            }
            
        }

        private void button13_Click(object sender, EventArgs e)
        {

        }
    }
}
