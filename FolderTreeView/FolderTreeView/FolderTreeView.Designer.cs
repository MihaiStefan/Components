namespace DirectoryLister
{
    partial class FolderTreeView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderTreeView));
            this.dirsTreeView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // dirsTreeView
            // 
            this.dirsTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dirsTreeView.Location = new System.Drawing.Point(3, 3);
            this.dirsTreeView.Name = "dirsTreeView";
            this.dirsTreeView.Size = new System.Drawing.Size(408, 395);
            this.dirsTreeView.TabIndex = 1;
            this.dirsTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.dirsTreeView_BeforeExpand);
            this.dirsTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.dirsTreeView_NodeMouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "1346238561_folder_classic.png");
            this.imageList1.Images.SetKeyName(1, "1346238604_folder_classic_opened.png");
            this.imageList1.Images.SetKeyName(2, "1346228331_drive.png");
            this.imageList1.Images.SetKeyName(3, "1346228337_drive_cd.png");
            this.imageList1.Images.SetKeyName(4, "1346228356_drive_cd_empty.png");
            this.imageList1.Images.SetKeyName(5, "1346228364_drive_disk.png");
            this.imageList1.Images.SetKeyName(6, "1346228591_drive_network.png");
            this.imageList1.Images.SetKeyName(7, "1346228618_drive_link.png");
            this.imageList1.Images.SetKeyName(8, "1346228623_drive_error.png");
            this.imageList1.Images.SetKeyName(9, "1346228633_drive_go.png");
            this.imageList1.Images.SetKeyName(10, "1346228636_drive_delete.png");
            this.imageList1.Images.SetKeyName(11, "1346228639_drive_burn.png");
            this.imageList1.Images.SetKeyName(12, "1346238642_folder_classic_locked.png");
            // 
            // FolderTreeView
            // 
            this.Controls.Add(this.dirsTreeView);
            this.Name = "FolderTreeView";
            this.Size = new System.Drawing.Size(414, 401);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TreeView dirsTreeView;
        private System.Windows.Forms.ImageList imageList1;
    }
}
