using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageFile;
using DirectoryLister;

namespace ImageFileTest
{
    public partial class Form1 : Form
    {
        private string path = @"c:\Temp\0\";
        public List<PictureBox> ImgList;
        private ImageFile.ImageFile imgFile = null;

        public Form1()
        {
            InitializeComponent();
            pannel_1.ProcessLog = this.ProcessLogTODO;
            imgFile = new ImageFile.ImageFile("11.txt");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pannel_1.GeneratePatrare();
        }

        private void thumbnail1_Resize(object sender, EventArgs e)
        {

        }

        public void ProcessLogTODO(string Message)
        {
            this.textBox1.AppendText(Message + Environment.NewLine);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pannel_1.ObjectWidth = Convert.ToInt32(textBox2.Text);
            pannel_1.ObjectHeight = Convert.ToInt32(textBox3.Text);
            pannel_1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //pannel_1.Path = this.path;
            if (folderTreeView1.ReturnPresentNode() != null)
            {
                pannel_1.Path = folderTreeView1.ReturnPathFromPresentNode();
            }
        }

        private void folderTreeView1_FolderNodeDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            MessageBox.Show(folderTreeView1.ReturnPathFromNode(e.Node));
        }
    }
}
