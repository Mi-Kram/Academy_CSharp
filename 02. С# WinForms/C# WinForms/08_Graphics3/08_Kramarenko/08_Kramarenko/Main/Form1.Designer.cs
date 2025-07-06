namespace Main
{
    partial class Form
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
            this.folderLabel = new System.Windows.Forms.Label();
            this.folderTextBox = new System.Windows.Forms.TextBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.pictureLabel = new System.Windows.Forms.Label();
            this.resultFolderLabel = new System.Windows.Forms.Label();
            this.pictureTextBox = new System.Windows.Forms.TextBox();
            this.resultFolderTextBox = new System.Windows.Forms.TextBox();
            this.chooseFolderButton = new System.Windows.Forms.Button();
            this.choosePictureButton = new System.Windows.Forms.Button();
            this.chooseResultFolderButton = new System.Windows.Forms.Button();
            this.chooseFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.chooseFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // folderLabel
            // 
            this.folderLabel.AutoSize = true;
            this.folderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.folderLabel.Location = new System.Drawing.Point(12, 31);
            this.folderLabel.Name = "folderLabel";
            this.folderLabel.Size = new System.Drawing.Size(136, 25);
            this.folderLabel.TabIndex = 4;
            this.folderLabel.Text = "Путь к папке:";
            // 
            // folderTextBox
            // 
            this.folderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.folderTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.folderTextBox.Location = new System.Drawing.Point(36, 59);
            this.folderTextBox.Name = "folderTextBox";
            this.folderTextBox.Size = new System.Drawing.Size(722, 30);
            this.folderTextBox.TabIndex = 0;
            // 
            // OkButton
            // 
            this.OkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OkButton.Location = new System.Drawing.Point(644, 260);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(144, 48);
            this.OkButton.TabIndex = 3;
            this.OkButton.Text = "ОК";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OkButton_MouseUp);
            // 
            // pictureLabel
            // 
            this.pictureLabel.AutoSize = true;
            this.pictureLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pictureLabel.Location = new System.Drawing.Point(12, 109);
            this.pictureLabel.Name = "pictureLabel";
            this.pictureLabel.Size = new System.Drawing.Size(169, 25);
            this.pictureLabel.TabIndex = 5;
            this.pictureLabel.Text = "Путь к картинке:";
            // 
            // resultFolderLabel
            // 
            this.resultFolderLabel.AutoSize = true;
            this.resultFolderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resultFolderLabel.Location = new System.Drawing.Point(12, 185);
            this.resultFolderLabel.Name = "resultFolderLabel";
            this.resultFolderLabel.Size = new System.Drawing.Size(302, 25);
            this.resultFolderLabel.TabIndex = 6;
            this.resultFolderLabel.Text = "Путь к результирующей папке:";
            // 
            // pictureTextBox
            // 
            this.pictureTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pictureTextBox.Location = new System.Drawing.Point(36, 137);
            this.pictureTextBox.Name = "pictureTextBox";
            this.pictureTextBox.Size = new System.Drawing.Size(722, 30);
            this.pictureTextBox.TabIndex = 1;
            // 
            // resultFolderTextBox
            // 
            this.resultFolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultFolderTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resultFolderTextBox.Location = new System.Drawing.Point(36, 213);
            this.resultFolderTextBox.Name = "resultFolderTextBox";
            this.resultFolderTextBox.Size = new System.Drawing.Size(722, 30);
            this.resultFolderTextBox.TabIndex = 2;
            // 
            // chooseFolderButton
            // 
            this.chooseFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chooseFolderButton.Location = new System.Drawing.Point(760, 58);
            this.chooseFolderButton.Name = "chooseFolderButton";
            this.chooseFolderButton.Size = new System.Drawing.Size(38, 35);
            this.chooseFolderButton.TabIndex = 7;
            this.chooseFolderButton.TabStop = false;
            this.chooseFolderButton.Text = "...";
            this.chooseFolderButton.UseVisualStyleBackColor = true;
            this.chooseFolderButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chooseFolderButton_MouseUp);
            // 
            // choosePictureButton
            // 
            this.choosePictureButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.choosePictureButton.Location = new System.Drawing.Point(760, 136);
            this.choosePictureButton.Name = "choosePictureButton";
            this.choosePictureButton.Size = new System.Drawing.Size(38, 35);
            this.choosePictureButton.TabIndex = 8;
            this.choosePictureButton.TabStop = false;
            this.choosePictureButton.Text = "...";
            this.choosePictureButton.UseVisualStyleBackColor = true;
            this.choosePictureButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.choosePictureButton_MouseUp);
            // 
            // chooseResultFolderButton
            // 
            this.chooseResultFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chooseResultFolderButton.Location = new System.Drawing.Point(760, 212);
            this.chooseResultFolderButton.Name = "chooseResultFolderButton";
            this.chooseResultFolderButton.Size = new System.Drawing.Size(38, 35);
            this.chooseResultFolderButton.TabIndex = 9;
            this.chooseResultFolderButton.TabStop = false;
            this.chooseResultFolderButton.Text = "...";
            this.chooseResultFolderButton.UseVisualStyleBackColor = true;
            this.chooseResultFolderButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chooseResultFolderButton_MouseUp);
            // 
            // chooseFileDialog
            // 
            this.chooseFileDialog.Title = "Opening";
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 315);
            this.Controls.Add(this.chooseResultFolderButton);
            this.Controls.Add(this.choosePictureButton);
            this.Controls.Add(this.chooseFolderButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.resultFolderTextBox);
            this.Controls.Add(this.pictureTextBox);
            this.Controls.Add(this.folderTextBox);
            this.Controls.Add(this.resultFolderLabel);
            this.Controls.Add(this.pictureLabel);
            this.Controls.Add(this.folderLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form";
            this.Text = "AddLogo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label folderLabel;
        private System.Windows.Forms.TextBox folderTextBox;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Label pictureLabel;
        private System.Windows.Forms.Label resultFolderLabel;
        private System.Windows.Forms.TextBox pictureTextBox;
        private System.Windows.Forms.TextBox resultFolderTextBox;
        private System.Windows.Forms.Button chooseFolderButton;
        private System.Windows.Forms.Button choosePictureButton;
        private System.Windows.Forms.Button chooseResultFolderButton;
        private System.Windows.Forms.FolderBrowserDialog chooseFolderDialog;
        private System.Windows.Forms.OpenFileDialog chooseFileDialog;
    }
}

