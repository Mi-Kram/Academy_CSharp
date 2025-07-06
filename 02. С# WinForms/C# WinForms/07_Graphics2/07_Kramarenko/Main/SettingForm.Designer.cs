namespace Main
{
    partial class SettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.tablePanel = new System.Windows.Forms.Panel();
            this.fillFigureCheckBox = new System.Windows.Forms.CheckBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.tablePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tablePanel
            // 
            this.tablePanel.BackColor = System.Drawing.Color.White;
            this.tablePanel.Controls.Add(this.fillFigureCheckBox);
            this.tablePanel.Location = new System.Drawing.Point(12, 12);
            this.tablePanel.Name = "tablePanel";
            this.tablePanel.Size = new System.Drawing.Size(205, 302);
            this.tablePanel.TabIndex = 0;
            // 
            // fillFigureCheckBox
            // 
            this.fillFigureCheckBox.AutoSize = true;
            this.fillFigureCheckBox.Checked = true;
            this.fillFigureCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fillFigureCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fillFigureCheckBox.Location = new System.Drawing.Point(15, 15);
            this.fillFigureCheckBox.Name = "fillFigureCheckBox";
            this.fillFigureCheckBox.Size = new System.Drawing.Size(158, 24);
            this.fillFigureCheckBox.TabIndex = 0;
            this.fillFigureCheckBox.Text = "Залить фигуру";
            this.fillFigureCheckBox.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(685, 335);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(110, 35);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "Подтвердить";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // SettingForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 374);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tablePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SettingForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingForm_FormClosing);
            this.tablePanel.ResumeLayout(false);
            this.tablePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel tablePanel;
        private System.Windows.Forms.CheckBox fillFigureCheckBox;
        private System.Windows.Forms.Button buttonOK;
    }
}