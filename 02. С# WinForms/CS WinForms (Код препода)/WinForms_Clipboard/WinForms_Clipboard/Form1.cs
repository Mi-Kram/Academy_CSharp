using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections.Specialized;

namespace WinForms_Clipboard
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Поместить текст в буфер обмена
            Clipboard.SetText(textBox1.SelectedText);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Загрузить изображение из файла
            Image b1 = Bitmap.FromFile("num1.png");

            // Поместить картинку в буфер обмена
            //Clipboard.SetImage(b1);

            // Создать контейнер, содержащий разные объекты
            DataObject obj = new DataObject();

            // Поместить текст в контейнер
            obj.SetData(DataFormats.Text, "My image");

            // Поместить изображение в контейнер
            obj.SetData(DataFormats.Bitmap, b1);

            // Поместить в буфер обмена контейнер
            Clipboard.SetDataObject(obj, true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Если в буфере обмена находится текст
            if (Clipboard.ContainsText())
            {
                // Получить текст из буфера обмена
                string str = Clipboard.GetText();
                textBox1.Text = str;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Поместить HTML-код в буфер обмена
            Clipboard.SetDataObject(new DataObject(DataFormats.Html, "<html><title>html page</title><body>hello world</body></html>"), true);
        }

        private void pasteHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Получить из буфера обмена контейнер с данными
            IDataObject obj = Clipboard.GetDataObject();

            // Проверить наличие HTML в контейнере
            if (obj.GetDataPresent(DataFormats.Html))
            {
                // Получить HTML из контейнера данных
                textBox1.Text = (string)obj.GetData(DataFormats.Html);
            }
        }

        private void copyVectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            // Создать контейнер для хранения информации
            DataObject obj = new DataObject();

            // Создать пользовательский объект
            Color cl = Color.FromArgb(255,238,8);

            // Задать ключ формата наших данных
            obj.SetData("Myappformat", cl);

            // Поместить контейнер в буфер обмена
            Clipboard.SetDataObject(obj, true);
            

            /*Vector v = new Vector(3);

            // Создать контейнер для хранения данных в буфере обмена
            DataObject obj = new DataObject();

            // Поместить данные в контейнер по определённому ключу
            obj.SetData("Myappformat", v);

            // Поместить контейнер в буфер обмена
            Clipboard.SetDataObject(obj, true);*/
        }

        private void pasteVectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Получить контейнер из буфера обмена
            IDataObject obj = Clipboard.GetDataObject();

            // Получить все содержащиеся в контейнере форматы данныз
            /*string[] str = obj.GetFormats();
            MessageBox.Show(str.Length.ToString());
            foreach (string s in str)
            {
                MessageBox.Show(s);
            }

            // Если контейнер содержит данные определённого формата
            if (obj.GetDataPresent("Myappformat"))
            {
                // Получить нужные данные, привести их к нужному типу и задействовать их в приложении
                Vector v = (Vector)Clipboard.GetData("Myappformat");
                v.print();
            }*/
            
            
            // Если в буфере находятся данные определённого формата
            if (obj.GetDataPresent("Myappformat"))
            {
                // Получить данные этого формата
                Color string1 = (Color)Clipboard.GetData("Myappformat");

                // Использовать полученные данные
                this.BackColor = string1;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void clipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pasteFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";

            // Получить из буфера обмена контейнер с данными
            IDataObject obj = Clipboard.GetDataObject();

            // Проверить наличие в контейнере списка файлов
            if (obj.GetDataPresent(DataFormats.FileDrop))
            {
                // Получить список файлов из контейнера
                StringCollection files = Clipboard.GetFileDropList();

                // Обработать файлы
                foreach (string str in files)
                {
                    textBox1.Text += str;
                    textBox1.Text += "\r\n";
                }
            }
        }

        private void copyFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Открыть диалог выбора файлов
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Создать пустой список строк
                StringCollection str = new StringCollection();

                // Поместить туда список выбранных пользователем имён файлов
                str.AddRange(openFileDialog1.FileNames);

                // Поместить список имён файлов в буфер обмена
                Clipboard.SetFileDropList(str);
            }
        }
    }

    [Serializable]
    public class Vector
    {
        int[] a = new int[5];
        public Vector(int k)
        {
            for (int i = 0; i < 5; i++)
                a[i] = k+i;
        }
        public void print()
        {
            for (int i = 0; i < 5; i++)
                MessageBox.Show(a[i].ToString());
        }
    }
}