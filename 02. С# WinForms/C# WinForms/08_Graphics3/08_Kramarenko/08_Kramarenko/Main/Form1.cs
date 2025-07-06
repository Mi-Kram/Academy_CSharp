using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void OkButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.X > 0 && e.X < OkButton.Width &&
                e.Y > 0 && e.Y < OkButton.Height)
            {
                try
                {
                    if (!Directory.Exists(folderTextBox.Text)) 
                        throw new Exception($"Путь {folderTextBox.Text} недействителен");
                    if (!File.Exists(pictureTextBox.Text)) 
                        throw new Exception($"Путь {pictureTextBox.Text} недействителен");
                    if (!Directory.Exists(resultFolderTextBox.Text))
                    {
                        Directory.CreateDirectory(resultFolderTextBox.Text);
                        if (!Directory.Exists(resultFolderTextBox.Text)) 
                            throw new Exception($"Путь {resultFolderTextBox.Text} недействителен");
                    }

                    string ext = Path.GetExtension(pictureTextBox.Text).ToLower();
                    if (ext != ".png" && ext != ".bmp" && ext != ".jpg" && ext != ".jpeg") 
                        throw new Exception("Недопустимое расширение для картинки");

                    AddLogo(folderTextBox.Text, pictureTextBox.Text, resultFolderTextBox.Text, new string[]{ "*.png", "*.jpg", "*.jpeg", "*.bmp" });
                    MessageBox.Show("Успешно обработано", "INFO", MessageBoxButtons.OK);

                    folderTextBox.Text = "";
                    pictureTextBox.Text = "";
                    resultFolderTextBox.Text = "";

                    folderTextBox.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "INFO", MessageBoxButtons.OK);
                }
            }
        }


        private void AddLogo(string folder, string img, string resultFolder, string[] fileExts)
        {
            DirectoryInfo dInfo = new DirectoryInfo(folder);
            Image logo = new Bitmap(img);

            foreach (string ext in fileExts)
            {
                FileInfo[] files = dInfo.GetFiles(ext);
                foreach (FileInfo file in files)
                {
                    Image image = new Bitmap(file.FullName);
                    Graphics imageGraphics = Graphics.FromImage(image);

                    int w = (image.Width / 10) * 3;
                    int h = image.Height / 4;

                    if (logo.Width > w)
                        h = (int)(logo.Height / ((double)logo.Width / w));
                    if(logo.Height > h)
                        w = (int)(logo.Width / ((double)logo.Height / h));


                    Rectangle rectangle = new Rectangle(image.Width-(w+10), image.Height-(h+7), w, h);

                    imageGraphics.DrawImage(logo, rectangle);
                    image.Save(resultFolder + '\\' +file.Name);

                    imageGraphics.Dispose();
                }
            }
        }


        private void chooseFolderButton_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left && e.X > 0 && e.X < chooseFolderButton.Width &&
                e.Y > 0 && e.Y < chooseFolderButton.Height)
            {
                if (chooseFolderDialog.ShowDialog() == DialogResult.OK)
                {
                    folderTextBox.Text = chooseFolderDialog.SelectedPath;
                }
                folderTextBox.Focus();
                folderTextBox.SelectionStart = folderTextBox.TextLength;
            }
        }
        private void choosePictureButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.X > 0 && e.X < choosePictureButton.Width &&
                e.Y > 0 && e.Y < choosePictureButton.Height)
            {
                chooseFileDialog.Filter = "PNG, BMP, JPG, JPEG|*.png;*.bmp;*.jpg;*.jpeg";
                chooseFileDialog.FilterIndex = 1;
                chooseFileDialog.Multiselect = false;
                chooseFileDialog.CheckFileExists = true;
                chooseFileDialog.Title = "Opening";
                chooseFileDialog.FileName = "";

                if (chooseFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureTextBox.Text = chooseFileDialog.FileName;
                }
                pictureTextBox.Focus();
                pictureTextBox.SelectionStart = pictureTextBox.TextLength;
            }
        }
        private void chooseResultFolderButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.X > 0 && e.X < chooseResultFolderButton.Width &&
                e.Y > 0 && e.Y < chooseResultFolderButton.Height)
            {
                if (chooseFolderDialog.ShowDialog() == DialogResult.OK)
                {
                    resultFolderTextBox.Text = chooseFolderDialog.SelectedPath;
                }
                resultFolderTextBox.Focus();
                resultFolderTextBox.SelectionStart = resultFolderTextBox.TextLength;
            }
        }
    }
}
