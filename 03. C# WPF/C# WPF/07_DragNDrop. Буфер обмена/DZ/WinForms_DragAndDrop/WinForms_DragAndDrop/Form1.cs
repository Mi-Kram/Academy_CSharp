using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WinForms_DragAndDrop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // разрешить перетаскивание в pictureBox
            pictureBox1.AllowDrop = true;
        }

        /// <summary>
        /// Метод запускается в ответ на отпускание кнопки мыши при перетаскивании
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            // Если перетаскивается список файлов
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Получить и напечатать список файлов
                string[] str = (string[])e.Data.GetData(DataFormats.FileDrop);
                listBox1.Items.AddRange(str);
            }
        }

        /// <summary>
        /// Метод запускается, когда пользователь затащил что-то в область
        /// </summary>
        /// <param name="sender">Объект-источник</param>
        /// <param name="e">параметры</param>
        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            // Если пользователь копирует объект перетаскиванием и это список файлов и это не перетаскивание из listBox в него же
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && (e.AllowedEffect & DragDropEffects.Copy) != 0 && !e.Data.GetDataPresent("Myappformat"))
            {
                // Разрешить копирование
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void textBox1_DragOver(object sender, DragEventArgs e)
        {
            if ((e.AllowedEffect & DragDropEffects.Copy) != 0)
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            // Если перетащили файл - показать его содержимое
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] str = (string[])e.Data.GetData(DataFormats.FileDrop);
                string txt = File.ReadAllText(str[0]);
                textBox1.Text = txt;
            }

            // Если перетащили текст - показать текст
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string str = (string)e.Data.GetData(DataFormats.StringFormat);
                string s2 = str + "\r\n";
                textBox1.Text += s2;
            }
        }

        // Инициирование перетаскивания ИЗ listBox
        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // Если есть выделенные строки
            if (listBox1.SelectedIndex != -1)
            {
                // Получить выделенную строку
                string str = (string)listBox1.Items[listBox1.SelectedIndex];

                // Создать контейнер для хранения данных
                DataObject data1 = new DataObject();

                // Положить содержимое выделенной в списке строки
                StringCollection col = new StringCollection();
                col.Add(str);
                data1.SetFileDropList(col);

                // Если выделено имя файла картинки - положить картинку в контейнер
                string ext = Path.GetExtension(str);
                if (ext == ".bmp" || ext == ".jpg" || ext == ".gif" || ext == ".png")
                {
                    Image img = Bitmap.FromFile(str);
                    data1.SetImage(img);
                }

                // Добавить признак пользовательского формата в контейнер
                data1.SetData("Myappformat", 0);

                // НАЧАТЬ перетаскивание программно
                DragDropEffects dde = DoDragDrop(data1, DragDropEffects.Copy);
            }
        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            if ((e.AllowedEffect & DragDropEffects.Copy) != 0)
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            // Если перетаскивается картинка
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                // Показать картинку
                Image img = (Image)e.Data.GetData(DataFormats.Bitmap);
                pictureBox1.Image = img;
            }
            
        }
     }
}