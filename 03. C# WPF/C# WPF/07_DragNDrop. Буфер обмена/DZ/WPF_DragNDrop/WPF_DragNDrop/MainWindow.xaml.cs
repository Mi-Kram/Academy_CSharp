using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_DragNDrop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод запускается, когда пользователь затащил что-то в область
        /// </summary>
        /// <param name="sender">Объект-источник</param>
        /// <param name="e">параметры</param>
        private void ListBox_DragEnter(object sender, DragEventArgs e)
        {
            // Если пользователь копирует объект перетаскиванием и это список файлов и это не перетаскивание из listBox в него же
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && 
                (e.AllowedEffects & DragDropEffects.Copy) != 0 && 
                !e.Data.GetDataPresent("Myappformat"))
            {
                // Разрешить копирование
                e.Effects = DragDropEffects.Copy;
            }
        }

        /// <summary>
        /// Метод запускается в ответ на отпускание кнопки мыши при перетаскивании
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_Drop(object sender, DragEventArgs e)
        {
            // Если перетаскивается список файлов
            if (e.Data.GetDataPresent(DataFormats.FileDrop) &&
                (e.AllowedEffects & DragDropEffects.Copy) != 0 &&
                !e.Data.GetDataPresent("Myappformat"))
            {
                // Получить и напечатать список файлов
                string[] str = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (var item in str)
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            if ((e.AllowedEffects & DragDropEffects.Copy) != 0)
            {
                e.Effects = DragDropEffects.Copy;
            }
        }

        private void pictureBox1_Drop(object sender, DragEventArgs e)
        {
            // Если перетаскивается картинка
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                // Показать картинку
                BitmapSource bmp = (BitmapSource)e.Data.GetData(DataFormats.Bitmap);
                pictureBox1.Source = bmp;
            }
        }

        private void listBox1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // получить координаты мыши в listBox
            System.Windows.Point pt = e.GetPosition(this);

            // выяснить, над каким контролом находится курсор мыши
            HitTestResult res = System.Windows.Media.VisualTreeHelper.HitTest(this, pt);

            // если контрол не TextBlock, то ничего не делать
            if (!(res.VisualHit is TextBlock))
                return;

            // получить TextBlock, соответствующий пункту, над которым находится курсор мыши
            TextBlock tb = (TextBlock)res.VisualHit;

            // получить надпись из пункта listBox
            string selectedFileName = (string)(tb).DataContext;

            // выяснить номер пункта listBox, над которым находится курсор мыши
            int itemIndex = listBox1.Items.IndexOf(selectedFileName);
            if (itemIndex >= 0)
            {
                // программно выделить найденным пункт
                listBox1.SelectedIndex = itemIndex;
            }

            // Создать контейнер для хранения данных
            DataObject data1 = new DataObject();

            // Положить содержимое выделенной в списке строки
            StringCollection col = new StringCollection();
            col.Add(selectedFileName);
            data1.SetFileDropList(col);

            // Если выделено имя файла картинки - положить картинку в контейнер
            string ext = System.IO.Path.GetExtension(selectedFileName);
            if (ext == ".bmp" || ext == ".jpg" || ext == ".gif" || ext == ".png")
            {
                BitmapSource bSource = new BitmapImage(new Uri(selectedFileName));
                data1.SetImage(bSource);
            }

            // Добавить признак пользовательского формата в контейнер
            data1.SetData("Myappformat", 0);

            // НАЧАТЬ перетаскивание программно
            DragDropEffects dde = DragDrop.DoDragDrop(this, data1, DragDropEffects.Copy);
        }

        private void TextBlock_DragOver(object sender, DragEventArgs e)
        {
            if ((e.AllowedEffects & DragDropEffects.Copy) != 0)
            {
                e.Effects = DragDropEffects.Copy;
            }
        }

        private void TextBlock_Drop(object sender, DragEventArgs e)
        {
            // Если перетащили файл - показать его содержимое
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] str = (string[])e.Data.GetData(DataFormats.FileDrop);
                string ext = System.IO.Path.GetExtension(str[0]);
                if (ext == ".txt")
                {
                    string txt = File.ReadAllText(str[0]);
                    textBlock1.Text = txt;
                }
            }

            // Если перетащили текст - показать текст
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string str = (string)e.Data.GetData(DataFormats.StringFormat);
                string s2 = str + "\r\n";
                textBlock1.Text += s2;
            }
        }
    }
}
