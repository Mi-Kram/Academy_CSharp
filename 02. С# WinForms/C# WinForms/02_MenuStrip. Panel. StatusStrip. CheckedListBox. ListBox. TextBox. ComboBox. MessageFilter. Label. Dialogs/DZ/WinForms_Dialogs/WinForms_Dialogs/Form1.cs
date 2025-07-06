using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinForms_Dialogs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Открыть полную версию диалога
            colorDialog1.FullOpen = true;

            // Разместить кнопку открытия полной версии
            colorDialog1.AllowFullOpen = true;

            // Открытие диалога
            DialogResult result = colorDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                // Применить выбранный цвет
                this.BackColor = colorDialog1.Color;
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            // Заголовок диалогового окна
            openFileDialog1.Title = "Hello world!!!";

            // Фильтры файлов в диалоге
            openFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|PowerPoint Presentations|*.ppt" +
             "|Office Files|*.doc;*.xls;*.ppt" + "|All Files|*.*";

            // Номер выбранного по умолчанию фильтра
            openFileDialog1.FilterIndex = 3;

            // Проверка существования выбранного файла
            openFileDialog1.CheckFileExists = true;

            // Разрешить выбор нескольких файлов
             openFileDialog1.Multiselect = false;

            // Открытие диалога
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Полный путь к выбранному файлу
                MessageBox.Show(openFileDialog1.FileName);
                //openFileDialog1.FileNames

                // Короткое имя выбранного файла
                MessageBox.Show(openFileDialog1.SafeFileName);

                // Вытащить икону из выбранного файла
                this.Icon = Icon.ExtractAssociatedIcon(openFileDialog1.FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Hello world!!!";
            saveFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|PowerPoint Presentations|*.ppt" +
             "|Office Files|*.doc;*.xls;*.ppt" +
             "|All Files|*.*";
            saveFileDialog1.FilterIndex = 3;
            saveFileDialog1.CheckFileExists = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(saveFileDialog1.FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Задание начальной папки для диалога
            //folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;
            
            folderBrowserDialog1.SelectedPath = @"c:\temp\";

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(folderBrowserDialog1.SelectedPath);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowEffects = true;
            fontDialog1.ShowColor = true;
            fontDialog1.Color = Color.Red;
            fontDialog1.MinSize = 20;
            fontDialog1.MaxSize = 50;

            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                label1.Font = fontDialog1.Font;
                label1.ForeColor = fontDialog1.Color;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(e.CloseReason.ToString(), "Вы уверены, что хотите выйти?", MessageBoxButtons.YesNo);
            
            // если пользователь нажал на кнопку 'No'
            if (result == DialogResult.No)
                e.Cancel = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //MessageBox.Show("Form1_FormClosed");
        }
    }
}
