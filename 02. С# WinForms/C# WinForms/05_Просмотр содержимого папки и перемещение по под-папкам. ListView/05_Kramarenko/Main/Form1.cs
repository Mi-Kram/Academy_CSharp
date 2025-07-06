using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Main
{
    public partial class Form1 : Form
    {
        string currentPath = @"C:\";
        public Form1()
        {
            while (!Directory.Exists(currentPath))
            {
                if (MessageBox.Show("Папки по умолчанию не существует.\nВыбрать папку вручную?", "INFO",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (folderBrowserDialog == null)
                        folderBrowserDialog = new FolderBrowserDialog();

                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        currentPath = folderBrowserDialog.SelectedPath;
                }
                else Environment.Exit(0);
            }

            InitializeComponent();

            folderViewerListBox.SmallImageList = new ImageList();
            folderViewerListBox.SmallImageList.Images.Add(Properties.Resources.directoryIcon);

            pathTextBox.Text = currentPath;
            ListViewApdate();
        }

        private void ListViewApdate()
        {
            folderViewerListBox.Items.Clear();

            try
            {
                DirectoryInfo dInfo = new DirectoryInfo(currentPath);

                DirectoryInfo[] folders = dInfo.GetDirectories();
                foreach (DirectoryInfo folder in folders)
                {
                    ListViewItem item = new ListViewItem(folder.Name, folderViewerListBox.Groups[0]);

                    item.ImageIndex = 0;

                    folderViewerListBox.Items.Add(item);
                }

                FileInfo[] files = dInfo.GetFiles();
                foreach (FileInfo file in files)
                {
                    ListViewItem item = new ListViewItem(file.Name);

                    item.ImageIndex = GetIcon(Icon.ExtractAssociatedIcon(file.FullName));

                    item.SubItems.Add(file.Length.ToString());
                    item.SubItems.Add(file.CreationTime.ToString("d"));
                    item.SubItems.Add(file.Extension);
                    item.Group = folderViewerListBox.Groups[1];

                    folderViewerListBox.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INFO", MessageBoxButtons.OK);
            }
        }

        private int GetIcon(Icon icon)
        {
            for (int i = 0; i < folderViewerListBox.SmallImageList.Images.Count; i++)
            {
                if (folderViewerListBox.SmallImageList.Images[i] == icon.ToBitmap()) return i;
            }

            folderViewerListBox.SmallImageList.Images.Add(icon);
            return folderViewerListBox.SmallImageList.Images.Count - 1;
        }

        private void folderViewerListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter && folderViewerListBox.SelectedItems.Count > 0 &&
                folderViewerListBox.SelectedItems[0].Group == folderViewerListBox.Groups[0])
            {
                currentPath += "\\" + folderViewerListBox.SelectedItems[0].Text;
                pathTextBox.Text = currentPath;
                ListViewApdate();
            }
            else if(e.KeyCode == Keys.Back)
            {
                string newPath = Path.GetDirectoryName(currentPath);
                
                if (newPath != null && newPath != "")
                {
                    currentPath = newPath;
                    pathTextBox.Text = currentPath;
                    ListViewApdate();
                }
            }
        }

        private void findFolderButton_Click(object sender, EventArgs e)
        {
            FindFolderDialog findFolderDialog = new FindFolderDialog();

            if (findFolderDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (object item in findFolderDialog.Controls)
                {
                    if (item is TextBox && (item as TextBox).Name == "newPathTextBox")
                    {
                        currentPath = (item as TextBox).Text;
                        pathTextBox.Text = currentPath;
                        ListViewApdate();
                        break;
                    }
                }
            }

            findFolderDialog.Dispose();
        }

        private void BackArrowPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                string newPath = Path.GetDirectoryName(currentPath);

                if (newPath != null && newPath != "")
                {
                    currentPath = newPath;
                    pathTextBox.Text = currentPath;
                    ListViewApdate();
                }
            }
        }

        private void folderViewerListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if (folderViewerListBox.SelectedItems.Count > 0 &&
                    folderViewerListBox.SelectedItems[0].Group == folderViewerListBox.Groups[0])
                {
                    currentPath += (currentPath[currentPath.Length-1] == '\\' ? "" : "\\") + folderViewerListBox.SelectedItems[0].Text;
                    pathTextBox.Text = currentPath;
                    ListViewApdate();
                }
            }
        }
    }
}
