namespace Main
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Folders", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Files", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("folder");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "file.txt",
            "77777",
            "01.01.2000",
            ".txt"}, -1);
            this.mainPanel = new System.Windows.Forms.Panel();
            this.findFolderButton = new System.Windows.Forms.Button();
            this.BackArrowPictureBox = new System.Windows.Forms.PictureBox();
            this.textPATHTextBox = new System.Windows.Forms.TextBox();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.folderViewerListBox = new System.Windows.Forms.ListView();
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sizeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dateCreationColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.extensionColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BackArrowPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.findFolderButton);
            this.mainPanel.Controls.Add(this.BackArrowPictureBox);
            this.mainPanel.Controls.Add(this.textPATHTextBox);
            this.mainPanel.Controls.Add(this.pathTextBox);
            this.mainPanel.Controls.Add(this.folderViewerListBox);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(694, 453);
            this.mainPanel.TabIndex = 0;
            // 
            // findFolderButton
            // 
            this.findFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.findFolderButton.BackColor = System.Drawing.Color.Transparent;
            this.findFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.findFolderButton.Location = new System.Drawing.Point(620, 14);
            this.findFolderButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.findFolderButton.Name = "findFolderButton";
            this.findFolderButton.Size = new System.Drawing.Size(65, 24);
            this.findFolderButton.TabIndex = 1;
            this.findFolderButton.Text = "Overview";
            this.findFolderButton.UseVisualStyleBackColor = false;
            this.findFolderButton.Click += new System.EventHandler(this.findFolderButton_Click);
            // 
            // BackArrowPictureBox
            // 
            this.BackArrowPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("BackArrowPictureBox.Image")));
            this.BackArrowPictureBox.Location = new System.Drawing.Point(9, 43);
            this.BackArrowPictureBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BackArrowPictureBox.Name = "BackArrowPictureBox";
            this.BackArrowPictureBox.Size = new System.Drawing.Size(52, 57);
            this.BackArrowPictureBox.TabIndex = 3;
            this.BackArrowPictureBox.TabStop = false;
            this.BackArrowPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BackArrowPictureBox_MouseDown);
            // 
            // textPATHTextBox
            // 
            this.textPATHTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.textPATHTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textPATHTextBox.Font = new System.Drawing.Font("Calibri", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textPATHTextBox.Location = new System.Drawing.Point(9, 16);
            this.textPATHTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textPATHTextBox.Name = "textPATHTextBox";
            this.textPATHTextBox.ReadOnly = true;
            this.textPATHTextBox.Size = new System.Drawing.Size(57, 22);
            this.textPATHTextBox.TabIndex = 2;
            this.textPATHTextBox.Text = "Path:";
            this.textPATHTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pathTextBox
            // 
            this.pathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathTextBox.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pathTextBox.Location = new System.Drawing.Point(70, 14);
            this.pathTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.ReadOnly = true;
            this.pathTextBox.Size = new System.Drawing.Size(546, 25);
            this.pathTextBox.TabIndex = 4;
            this.pathTextBox.Text = "C:\\temp";
            // 
            // folderViewerListBox
            // 
            this.folderViewerListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.folderViewerListBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader,
            this.sizeColumnHeader,
            this.dateCreationColumnHeader,
            this.extensionColumnHeader});
            this.folderViewerListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.folderViewerListBox.FullRowSelect = true;
            listViewGroup1.Header = "Folders";
            listViewGroup1.Name = "foldersListViewGroup";
            listViewGroup2.Header = "Files";
            listViewGroup2.Name = "filesListViewGroup";
            this.folderViewerListBox.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.folderViewerListBox.HideSelection = false;
            listViewItem1.Group = listViewGroup1;
            listViewItem2.Group = listViewGroup2;
            this.folderViewerListBox.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.folderViewerListBox.Location = new System.Drawing.Point(70, 43);
            this.folderViewerListBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.folderViewerListBox.MultiSelect = false;
            this.folderViewerListBox.Name = "folderViewerListBox";
            this.folderViewerListBox.Size = new System.Drawing.Size(615, 400);
            this.folderViewerListBox.TabIndex = 0;
            this.folderViewerListBox.UseCompatibleStateImageBehavior = false;
            this.folderViewerListBox.View = System.Windows.Forms.View.Details;
            this.folderViewerListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.folderViewerListBox_KeyDown);
            this.folderViewerListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.folderViewerListBox_MouseDoubleClick);
            // 
            // nameColumnHeader
            // 
            this.nameColumnHeader.Text = "Name";
            this.nameColumnHeader.Width = 180;
            // 
            // sizeColumnHeader
            // 
            this.sizeColumnHeader.Text = "Size";
            this.sizeColumnHeader.Width = 100;
            // 
            // dateCreationColumnHeader
            // 
            this.dateCreationColumnHeader.Text = "Date creation";
            this.dateCreationColumnHeader.Width = 115;
            // 
            // extensionColumnHeader
            // 
            this.extensionColumnHeader.Text = "Extension";
            this.extensionColumnHeader.Width = 85;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 453);
            this.Controls.Add(this.mainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Files viewer";
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BackArrowPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ListView folderViewerListBox;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.TextBox textPATHTextBox;
        private System.Windows.Forms.PictureBox BackArrowPictureBox;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader sizeColumnHeader;
        private System.Windows.Forms.ColumnHeader dateCreationColumnHeader;
        private System.Windows.Forms.ColumnHeader extensionColumnHeader;
        private System.Windows.Forms.Button findFolderButton;
    }
}

