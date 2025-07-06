using System.Windows.Forms;
using System.IO;

namespace Main
{
    public partial class FindFolderDialog : Form
    {
        public FindFolderDialog()
        {
            InitializeComponent();
        }

        private bool IsOK()
        {
            return Directory.Exists(newPathTextBox.Text);
        }

        private void FindFolderDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool isOK = IsOK();

            if (!isOK && DialogResult == DialogResult.OK)
            {
                if (MessageBox.Show("Папка не найдена.\nПопробовать ещё раз?", "INFO",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    e.Cancel = true;
                    newPathTextBox.Focus();
                    newPathTextBox.SelectAll();
                }
                else DialogResult = DialogResult.Cancel;
            }
        }

        private void chooseFolserButton_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if(folderBrowserDialog == null) folderBrowserDialog = new FolderBrowserDialog();

                if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    newPathTextBox.Text = folderBrowserDialog.SelectedPath;
                    if (IsOK()) DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }
    }
}
