namespace Main
{
    partial class FormReplace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReplace));
            this.visualFindTextBox = new System.Windows.Forms.TextBox();
            this.visualReplaceTextBox = new System.Windows.Forms.TextBox();
            this.findTextBox = new System.Windows.Forms.TextBox();
            this.replaceTextBox = new System.Windows.Forms.TextBox();
            this.replaceButton = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // visualFindTextBox
            // 
            this.visualFindTextBox.BackColor = System.Drawing.Color.White;
            this.visualFindTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.visualFindTextBox.Location = new System.Drawing.Point(15, 16);
            this.visualFindTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.visualFindTextBox.Name = "visualFindTextBox";
            this.visualFindTextBox.ReadOnly = true;
            this.visualFindTextBox.Size = new System.Drawing.Size(79, 21);
            this.visualFindTextBox.TabIndex = 0;
            this.visualFindTextBox.Text = "Find:";
            this.visualFindTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // visualReplaceTextBox
            // 
            this.visualReplaceTextBox.BackColor = System.Drawing.Color.White;
            this.visualReplaceTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.visualReplaceTextBox.Location = new System.Drawing.Point(15, 47);
            this.visualReplaceTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.visualReplaceTextBox.Name = "visualReplaceTextBox";
            this.visualReplaceTextBox.ReadOnly = true;
            this.visualReplaceTextBox.Size = new System.Drawing.Size(79, 21);
            this.visualReplaceTextBox.TabIndex = 0;
            this.visualReplaceTextBox.Text = "Replace:";
            this.visualReplaceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // findTextBox
            // 
            this.findTextBox.AcceptsTab = true;
            this.findTextBox.BackColor = System.Drawing.Color.White;
            this.findTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.findTextBox.Location = new System.Drawing.Point(102, 14);
            this.findTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.findTextBox.Name = "findTextBox";
            this.findTextBox.Size = new System.Drawing.Size(305, 28);
            this.findTextBox.TabIndex = 0;
            // 
            // replaceTextBox
            // 
            this.replaceTextBox.AcceptsTab = true;
            this.replaceTextBox.BackColor = System.Drawing.Color.White;
            this.replaceTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.replaceTextBox.Location = new System.Drawing.Point(102, 45);
            this.replaceTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.replaceTextBox.Name = "replaceTextBox";
            this.replaceTextBox.Size = new System.Drawing.Size(305, 28);
            this.replaceTextBox.TabIndex = 0;
            // 
            // replaceButton
            // 
            this.replaceButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.replaceButton.Location = new System.Drawing.Point(424, 14);
            this.replaceButton.Name = "replaceButton";
            this.replaceButton.Size = new System.Drawing.Size(98, 26);
            this.replaceButton.TabIndex = 1;
            this.replaceButton.Text = "Replace";
            this.replaceButton.UseVisualStyleBackColor = true;
            this.replaceButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.replaceButton_MouseUp);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.Location = new System.Drawing.Point(424, 47);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(98, 26);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Exit";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // FormReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(534, 86);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.replaceButton);
            this.Controls.Add(this.visualReplaceTextBox);
            this.Controls.Add(this.replaceTextBox);
            this.Controls.Add(this.findTextBox);
            this.Controls.Add(this.visualFindTextBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormReplace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Replace";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox visualFindTextBox;
        private System.Windows.Forms.TextBox visualReplaceTextBox;
        private System.Windows.Forms.TextBox findTextBox;
        private System.Windows.Forms.TextBox replaceTextBox;
        private System.Windows.Forms.Button replaceButton;
        private System.Windows.Forms.Button buttonCancel;
    }
}