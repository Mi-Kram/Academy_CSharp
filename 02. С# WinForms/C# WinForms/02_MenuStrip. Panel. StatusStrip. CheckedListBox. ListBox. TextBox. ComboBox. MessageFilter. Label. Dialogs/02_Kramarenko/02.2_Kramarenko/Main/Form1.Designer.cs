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
            this.picture = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.MaxPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.CursorPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picture
            // 
            this.picture.AutoSize = true;
            this.picture.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picture.FlatAppearance.BorderSize = 0;
            this.picture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.picture.Image = global::Main.Properties.Resources.picture;
            this.picture.Location = new System.Drawing.Point(80, 80);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(88, 76);
            this.picture.TabIndex = 0;
            this.picture.UseVisualStyleBackColor = true;
            this.picture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picture_MouseMove);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MaxPosition,
            this.CursorPosition});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "Positions";
            this.statusStrip1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.statusStrip1_MouseDown);
            this.statusStrip1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.statusStrip1_MouseMove);
            // 
            // MaxPosition
            // 
            this.MaxPosition.Name = "MaxPosition";
            this.MaxPosition.Size = new System.Drawing.Size(195, 20);
            this.MaxPosition.Text = "Max position: X = 0; Y = 0;";
            this.MaxPosition.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MaxPosition_MouseDown);
            // 
            // CursorPosition
            // 
            this.CursorPosition.Name = "CursorPosition";
            this.CursorPosition.Size = new System.Drawing.Size(211, 20);
            this.CursorPosition.Text = "Cursot position: X = 0; Y = 0;";
            this.CursorPosition.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CursorPosition_MouseDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.picture);
            this.Name = "Form1";
            this.Text = "HomeWork №2 Kramarenko";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button picture;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel MaxPosition;
        private System.Windows.Forms.ToolStripStatusLabel CursorPosition;
    }
}

