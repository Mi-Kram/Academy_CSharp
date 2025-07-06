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
            this.menuPanel = new System.Windows.Forms.Panel();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textPanel = new System.Windows.Forms.Panel();
            this.pages = new System.Windows.Forms.TabControl();
            this.firstTabPage = new System.Windows.Forms.TabPage();
            this.textBox = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuPanel.SuspendLayout();
            this.menu.SuspendLayout();
            this.textPanel.SuspendLayout();
            this.pages.SuspendLayout();
            this.firstTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuPanel
            // 
            this.menuPanel.BackColor = System.Drawing.Color.Transparent;
            this.menuPanel.Controls.Add(this.menu);
            this.menuPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuPanel.Location = new System.Drawing.Point(0, 0);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(800, 38);
            this.menuPanel.TabIndex = 0;
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.DarkGray;
            this.menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.findToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(800, 38);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 34);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(150, 26);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.MouseUp += new System.Windows.Forms.MouseEventHandler(this.newToolStripMenuItem_MouseUp);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(150, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.MouseUp += new System.Windows.Forms.MouseEventHandler(this.openToolStripMenuItem_MouseUp);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(150, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.MouseUp += new System.Windows.Forms.MouseEventHandler(this.saveToolStripMenuItem_MouseUp);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(150, 26);
            this.saveAsToolStripMenuItem.Text = "Save as...";
            this.saveAsToolStripMenuItem.MouseUp += new System.Windows.Forms.MouseEventHandler(this.saveAsToolStripMenuItem_MouseUp);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(150, 26);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.MouseUp += new System.Windows.Forms.MouseEventHandler(this.closeToolStripMenuItem_MouseUp);
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.replaceToolStripMenuItem});
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.Size = new System.Drawing.Size(51, 34);
            this.findToolStripMenuItem.Text = "Find";
            // 
            // replaceToolStripMenuItem
            // 
            this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
            this.replaceToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
            this.replaceToolStripMenuItem.Text = "Replace...";
            this.replaceToolStripMenuItem.MouseUp += new System.Windows.Forms.MouseEventHandler(this.replaceToolStripMenuItem_MouseUp);
            // 
            // textPanel
            // 
            this.textPanel.BackColor = System.Drawing.Color.Transparent;
            this.textPanel.Controls.Add(this.pages);
            this.textPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textPanel.Location = new System.Drawing.Point(0, 38);
            this.textPanel.Name = "textPanel";
            this.textPanel.Size = new System.Drawing.Size(800, 412);
            this.textPanel.TabIndex = 1;
            // 
            // pages
            // 
            this.pages.Controls.Add(this.firstTabPage);
            this.pages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pages.Location = new System.Drawing.Point(0, 0);
            this.pages.Name = "pages";
            this.pages.SelectedIndex = 0;
            this.pages.Size = new System.Drawing.Size(800, 412);
            this.pages.TabIndex = 0;
            // 
            // firstTabPage
            // 
            this.firstTabPage.AutoScroll = true;
            this.firstTabPage.Controls.Add(this.textBox);
            this.firstTabPage.Location = new System.Drawing.Point(4, 25);
            this.firstTabPage.Name = "firstTabPage";
            this.firstTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.firstTabPage.Size = new System.Drawing.Size(792, 383);
            this.firstTabPage.TabIndex = 0;
            this.firstTabPage.Text = "new1";
            this.firstTabPage.UseVisualStyleBackColor = true;
            // 
            // textBox
            // 
            this.textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox.Location = new System.Drawing.Point(3, 3);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox.Size = new System.Drawing.Size(786, 377);
            this.textBox.TabIndex = 0;
            this.textBox.WordWrap = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Title = "Openig";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "txt";
            this.saveFileDialog.Title = "Safekeeping";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textPanel);
            this.Controls.Add(this.menuPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "NotePad";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuPanel.ResumeLayout(false);
            this.menuPanel.PerformLayout();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.textPanel.ResumeLayout(false);
            this.pages.ResumeLayout(false);
            this.firstTabPage.ResumeLayout(false);
            this.firstTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel menuPanel;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceToolStripMenuItem;
        private System.Windows.Forms.Panel textPanel;
        private System.Windows.Forms.TabControl pages;
        private System.Windows.Forms.TabPage firstTabPage;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
    }
}

