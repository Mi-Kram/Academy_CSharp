using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_TabControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void addTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Создание новой вкладки с textBox внутри
            TabPage page = new TabPage("New page");

            // создание текстового поля внутри вкладки
            TextBox textBox = new TextBox();
            textBox.Text = "Hello\r\n world!!!";
            textBox.Dock = DockStyle.Fill;
            textBox.Multiline = true;
            textBox.Font = new Font("Times new roman", 16);
            textBox.TextChanged += TextBox_TextChanged;

            textBox.Parent = page;

            tabControl1.TabPages.Add(page);
        }

        // Обработчик запускается в ответ на внесение изменений в текстовое поле
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            // свойство Tag имеется почти у всех элементов управления WinForms
            tabControl1.SelectedTab.Tag = new int[] { 654, 87};
        }

        private void removeTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Удаление выделенной вкладки
            tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex);
        }

        private void setSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Программное выделение последней вкладки
            tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
        }

        // Метод запускается в ответ на переключение пользователем вкладки
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(String.Format(@"New selected tab: {0}", tabControl1.SelectedTab.Text));
        }
    }
}
