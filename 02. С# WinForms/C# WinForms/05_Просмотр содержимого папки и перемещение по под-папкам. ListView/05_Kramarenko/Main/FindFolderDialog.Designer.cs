namespace Main
{
    partial class FindFolderDialog
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
            this.textNewPathTextBox = new System.Windows.Forms.TextBox();
            this.newPathTextBox = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.chooseFolserButton = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textNewPathTextBox
            // 
            this.textNewPathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textNewPathTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textNewPathTextBox.Location = new System.Drawing.Point(9, 10);
            this.textNewPathTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textNewPathTextBox.Name = "textNewPathTextBox";
            this.textNewPathTextBox.ReadOnly = true;
            this.textNewPathTextBox.Size = new System.Drawing.Size(64, 16);
            this.textNewPathTextBox.TabIndex = 1;
            this.textNewPathTextBox.Text = "New path:";
            this.textNewPathTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // newPathTextBox
            // 
            this.newPathTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.newPathTextBox.Location = new System.Drawing.Point(78, 7);
            this.newPathTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.newPathTextBox.Name = "newPathTextBox";
            this.newPathTextBox.Size = new System.Drawing.Size(505, 23);
            this.newPathTextBox.TabIndex = 0;
            // 
            // chooseFolserButton
            // 
            this.chooseFolserButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chooseFolserButton.Location = new System.Drawing.Point(9, 41);
            this.chooseFolserButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chooseFolserButton.Name = "chooseFolserButton";
            this.chooseFolserButton.Size = new System.Drawing.Size(104, 44);
            this.chooseFolserButton.TabIndex = 2;
            this.chooseFolserButton.Text = "Choose folder";
            this.chooseFolserButton.UseVisualStyleBackColor = true;
            this.chooseFolserButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chooseFolserButton_MouseUp);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.Location = new System.Drawing.Point(512, 41);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(70, 44);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOK.Location = new System.Drawing.Point(427, 41);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(70, 44);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "Ok";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // FindFolderDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(600, 91);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.chooseFolserButton);
            this.Controls.Add(this.newPathTextBox);
            this.Controls.Add(this.textNewPathTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindFolderDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Choose folder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindFolderDialog_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textNewPathTextBox;
        private System.Windows.Forms.TextBox newPathTextBox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button chooseFolserButton;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
    }
}