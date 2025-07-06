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
            this.pencilWidthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.changeColorButton = new System.Windows.Forms.Button();
            this.tools = new System.Windows.Forms.ComboBox();
            this.board = new System.Windows.Forms.Panel();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pencilWidthNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Controls.Add(this.pencilWidthNumericUpDown);
            this.menu.Controls.Add(this.changeColorButton);
            this.menu.Controls.Add(this.tools);
            this.menu.Dock = System.Windows.Forms.DockStyle.Top;
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(600, 29);
            this.menu.TabIndex = 0;
            // 
            // pencilWidthNumericUpDown
            // 
            this.pencilWidthNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pencilWidthNumericUpDown.Location = new System.Drawing.Point(136, 5);
            this.pencilWidthNumericUpDown.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.pencilWidthNumericUpDown.Size = new System.Drawing.Size(36, 20);
            this.pencilWidthNumericUpDown.TabIndex = 2;
            this.pencilWidthNumericUpDown.TabStop = false;
            this.pencilWidthNumericUpDown.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // changeColorButton
            // 
            this.changeColorButton.BackColor = System.Drawing.Color.Black;
            this.changeColorButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.changeColorButton.FlatAppearance.BorderSize = 3;
            this.changeColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.changeColorButton.Location = new System.Drawing.Point(112, 5);
            this.changeColorButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.changeColorButton.Name = "changeColorButton";
            this.changeColorButton.Size = new System.Drawing.Size(20, 20);
            this.changeColorButton.TabIndex = 1;
            this.changeColorButton.UseVisualStyleBackColor = false;
            this.changeColorButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.changeColorButton_MouseUp);
            // 
            // tools
            // 
            this.tools.FormattingEnabled = true;
            this.tools.Items.AddRange(new object[] {
            "Карандаш",
            "Линия",
            "Прямоугольник",
            "Эллипс"});
            this.tools.Location = new System.Drawing.Point(2, 5);
            this.tools.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tools.Name = "tools";
            this.tools.Size = new System.Drawing.Size(107, 21);
            this.tools.TabIndex = 1;
            this.tools.Text = "Карандаш";
            this.tools.TextChanged += new System.EventHandler(this.tools_TextChanged);
            // 
            // board
            // 
            this.board.BackColor = System.Drawing.Color.White;
            this.board.Dock = System.Windows.Forms.DockStyle.Fill;
            this.board.Location = new System.Drawing.Point(0, 29);
            this.board.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.board.Name = "board";
            this.board.Size = new System.Drawing.Size(600, 337);
            this.board.TabIndex = 0;
            this.board.Paint += new System.Windows.Forms.PaintEventHandler(this.board_Paint);
            this.board.MouseDown += new System.Windows.Forms.MouseEventHandler(this.board_MouseDown);
            this.board.MouseMove += new System.Windows.Forms.MouseEventHandler(this.board_MouseMove);
            this.board.MouseUp += new System.Windows.Forms.MouseEventHandler(this.board_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.board);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Paint";
            this.menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pencilWidthNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel menu;
        private System.Windows.Forms.ComboBox tools;
        private System.Windows.Forms.Panel board;
        private System.Windows.Forms.Button changeColorButton;
        private System.Windows.Forms.NumericUpDown pencilWidthNumericUpDown;
    }
}

