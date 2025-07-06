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
            this.menu = new System.Windows.Forms.Panel();
            this.settingButton = new System.Windows.Forms.Button();
            this.pencilWidthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeColorButton = new System.Windows.Forms.Button();
            this.tools = new System.Windows.Forms.ComboBox();
            this.board = new System.Windows.Forms.Panel();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pencilWidthNumericUpDown)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.menu.Controls.Add(this.settingButton);
            this.menu.Controls.Add(this.pencilWidthNumericUpDown);
            this.menu.Controls.Add(this.menuStrip);
            this.menu.Controls.Add(this.changeColorButton);
            this.menu.Controls.Add(this.tools);
            this.menu.Dock = System.Windows.Forms.DockStyle.Top;
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(882, 36);
            this.menu.TabIndex = 0;
            // 
            // settingButton
            // 
            this.settingButton.BackColor = System.Drawing.Color.White;
            this.settingButton.FlatAppearance.BorderSize = 0;
            this.settingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.settingButton.Location = new System.Drawing.Point(336, 3);
            this.settingButton.Name = "settingButton";
            this.settingButton.Size = new System.Drawing.Size(139, 30);
            this.settingButton.TabIndex = 4;
            this.settingButton.Text = "Настройки";
            this.settingButton.UseVisualStyleBackColor = false;
            this.settingButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.settingButton_MouseUp);
            // 
            // pencilWidthNumericUpDown
            // 
            this.pencilWidthNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pencilWidthNumericUpDown.Location = new System.Drawing.Point(272, 2);
            this.pencilWidthNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pencilWidthNumericUpDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.pencilWidthNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pencilWidthNumericUpDown.Name = "pencilWidthNumericUpDown";
            this.pencilWidthNumericUpDown.Size = new System.Drawing.Size(60, 30);
            this.pencilWidthNumericUpDown.TabIndex = 2;
            this.pencilWidthNumericUpDown.TabStop = false;
            this.pencilWidthNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.pencilWidthNumericUpDown.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.White;
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(5, 4);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(67, 28);
            this.menuStrip.TabIndex = 3;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(166, 26);
            this.openToolStripMenuItem.Text = "Открыть";
            this.openToolStripMenuItem.MouseUp += new System.Windows.Forms.MouseEventHandler(this.openToolStripMenuItem_MouseUp);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(166, 26);
            this.saveToolStripMenuItem.Text = "Сохранить";
            this.saveToolStripMenuItem.MouseUp += new System.Windows.Forms.MouseEventHandler(this.saveToolStripMenuItem_MouseUp);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(166, 26);
            this.clearToolStripMenuItem.Text = "Очистить";
            this.clearToolStripMenuItem.MouseUp += new System.Windows.Forms.MouseEventHandler(this.clearToolStripMenuItem_MouseUp);
            // 
            // changeColorButton
            // 
            this.changeColorButton.BackColor = System.Drawing.Color.Black;
            this.changeColorButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.changeColorButton.FlatAppearance.BorderSize = 3;
            this.changeColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.changeColorButton.Location = new System.Drawing.Point(232, 2);
            this.changeColorButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.changeColorButton.Name = "changeColorButton";
            this.changeColorButton.Size = new System.Drawing.Size(33, 31);
            this.changeColorButton.TabIndex = 1;
            this.changeColorButton.UseVisualStyleBackColor = false;
            this.changeColorButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.changeColorButton_MouseUp);
            // 
            // tools
            // 
            this.tools.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tools.FormattingEnabled = true;
            this.tools.Items.AddRange(new object[] {
            "Карандаш",
            "Линия",
            "Прямоугольник",
            "Эллипс"});
            this.tools.Location = new System.Drawing.Point(84, 2);
            this.tools.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tools.Name = "tools";
            this.tools.Size = new System.Drawing.Size(141, 30);
            this.tools.TabIndex = 1;
            this.tools.Text = "Карандаш";
            this.tools.TextChanged += new System.EventHandler(this.tools_TextChanged);
            // 
            // board
            // 
            this.board.BackColor = System.Drawing.Color.White;
            this.board.Dock = System.Windows.Forms.DockStyle.Fill;
            this.board.Location = new System.Drawing.Point(0, 36);
            this.board.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.board.Name = "board";
            this.board.Size = new System.Drawing.Size(882, 517);
            this.board.TabIndex = 0;
            this.board.Paint += new System.Windows.Forms.PaintEventHandler(this.board_Paint);
            this.board.MouseDown += new System.Windows.Forms.MouseEventHandler(this.board_MouseDown);
            this.board.MouseMove += new System.Windows.Forms.MouseEventHandler(this.board_MouseMove);
            this.board.MouseUp += new System.Windows.Forms.MouseEventHandler(this.board_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 553);
            this.Controls.Add(this.board);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Paint";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pencilWidthNumericUpDown)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel menu;
        private System.Windows.Forms.ComboBox tools;
        private System.Windows.Forms.Panel board;
        private System.Windows.Forms.Button changeColorButton;
        private System.Windows.Forms.NumericUpDown pencilWidthNumericUpDown;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.Button settingButton;
    }
}

