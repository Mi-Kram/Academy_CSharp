namespace WinForms_Clipboard
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.clipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyBitmapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyVectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteVectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(12, 41);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(481, 220);
            this.textBox1.TabIndex = 2;
            this.textBox1.TabStop = false;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clipboardToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(505, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // clipboardToolStripMenuItem
            // 
            this.clipboardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyTextToolStripMenuItem,
            this.copyBitmapToolStripMenuItem,
            this.pasteTextToolStripMenuItem,
            this.copyHTMLToolStripMenuItem,
            this.pasteHTMLToolStripMenuItem,
            this.copyVectorToolStripMenuItem,
            this.pasteVectorToolStripMenuItem,
            this.pasteFilesToolStripMenuItem,
            this.copyFilesToolStripMenuItem});
            this.clipboardToolStripMenuItem.Name = "clipboardToolStripMenuItem";
            this.clipboardToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.clipboardToolStripMenuItem.Text = "Clipboard";
            this.clipboardToolStripMenuItem.Click += new System.EventHandler(this.clipboardToolStripMenuItem_Click);
            // 
            // copyTextToolStripMenuItem
            // 
            this.copyTextToolStripMenuItem.Name = "copyTextToolStripMenuItem";
            this.copyTextToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.copyTextToolStripMenuItem.Text = "Copy text";
            this.copyTextToolStripMenuItem.Click += new System.EventHandler(this.button1_Click);
            // 
            // copyBitmapToolStripMenuItem
            // 
            this.copyBitmapToolStripMenuItem.Name = "copyBitmapToolStripMenuItem";
            this.copyBitmapToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.copyBitmapToolStripMenuItem.Text = "Copy bitmap";
            this.copyBitmapToolStripMenuItem.Click += new System.EventHandler(this.button2_Click);
            // 
            // pasteTextToolStripMenuItem
            // 
            this.pasteTextToolStripMenuItem.Name = "pasteTextToolStripMenuItem";
            this.pasteTextToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.pasteTextToolStripMenuItem.Text = "Paste text";
            this.pasteTextToolStripMenuItem.Click += new System.EventHandler(this.button3_Click);
            // 
            // copyHTMLToolStripMenuItem
            // 
            this.copyHTMLToolStripMenuItem.Name = "copyHTMLToolStripMenuItem";
            this.copyHTMLToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.copyHTMLToolStripMenuItem.Text = "Copy HTML";
            this.copyHTMLToolStripMenuItem.Click += new System.EventHandler(this.button4_Click);
            // 
            // pasteHTMLToolStripMenuItem
            // 
            this.pasteHTMLToolStripMenuItem.Name = "pasteHTMLToolStripMenuItem";
            this.pasteHTMLToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.pasteHTMLToolStripMenuItem.Text = "Paste HTML";
            this.pasteHTMLToolStripMenuItem.Click += new System.EventHandler(this.pasteHTMLToolStripMenuItem_Click);
            // 
            // copyVectorToolStripMenuItem
            // 
            this.copyVectorToolStripMenuItem.Name = "copyVectorToolStripMenuItem";
            this.copyVectorToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.copyVectorToolStripMenuItem.Text = "Copy vector";
            this.copyVectorToolStripMenuItem.Click += new System.EventHandler(this.copyVectorToolStripMenuItem_Click);
            // 
            // pasteVectorToolStripMenuItem
            // 
            this.pasteVectorToolStripMenuItem.Name = "pasteVectorToolStripMenuItem";
            this.pasteVectorToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.pasteVectorToolStripMenuItem.Text = "Paste vector";
            this.pasteVectorToolStripMenuItem.Click += new System.EventHandler(this.pasteVectorToolStripMenuItem_Click);
            // 
            // pasteFilesToolStripMenuItem
            // 
            this.pasteFilesToolStripMenuItem.Name = "pasteFilesToolStripMenuItem";
            this.pasteFilesToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.pasteFilesToolStripMenuItem.Text = "Paste files";
            this.pasteFilesToolStripMenuItem.Click += new System.EventHandler(this.pasteFilesToolStripMenuItem_Click);
            // 
            // copyFilesToolStripMenuItem
            // 
            this.copyFilesToolStripMenuItem.Name = "copyFilesToolStripMenuItem";
            this.copyFilesToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.copyFilesToolStripMenuItem.Text = "Copy files";
            this.copyFilesToolStripMenuItem.Click += new System.EventHandler(this.copyFilesToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 273);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem clipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyBitmapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyHTMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteHTMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyVectorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteVectorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyFilesToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

