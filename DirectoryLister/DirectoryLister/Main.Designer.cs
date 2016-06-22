using System;
using System.Windows.Forms;

namespace DirectoryLister
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.folderTree1 = new DirectoryLister.FolderTree();
            this.SuspendLayout();
            // 
            // folderTree1
            // 
            this.folderTree1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.folderTree1.Location = new System.Drawing.Point(1, 2);
            this.folderTree1.Name = "folderTree1";
            this.folderTree1.Size = new System.Drawing.Size(421, 433);
            this.folderTree1.TabIndex = 0;
            this.folderTree1.FolderNodeDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(folderTree1_FolderNodeDoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 432);
            this.Controls.Add(this.folderTree1);
            this.Name = "MainForm";
            this.Text = "DirectoryLister";
            this.ResumeLayout(false);

        }

        #endregion

        private FolderTree folderTree1;
    }
}

