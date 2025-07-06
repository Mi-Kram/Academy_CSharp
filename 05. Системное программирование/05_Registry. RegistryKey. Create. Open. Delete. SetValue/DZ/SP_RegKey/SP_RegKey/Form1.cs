using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Runtime.InteropServices;
using Microsoft.Win32;
//using System.Diagnostics;
//using System.Threading;

namespace RegKey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // открыть ветку в определённом разделе по полному пути
            RegistryKey newkey = Registry.LocalMachine.OpenSubKey(@"Software", true);

            // получить названия подветок
            string[] names = newkey.GetSubKeyNames();

            listBox1.DataSource = names;

            newkey.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // создать поветку прямо в разделе реестра
            Registry.CurrentUser.CreateSubKey("Hello");

            // отрыть ветку
            RegistryKey newkey = Registry.LocalMachine.OpenSubKey("Software", true);

            // создать подветку
            newkey.CreateSubKey(textBox1.Text);

            button1_Click(null, null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // отрыть родительскую ветку
            RegistryKey newkey = Registry.LocalMachine.OpenSubKey("Software", true);

            // удалить подветку
            newkey.DeleteSubKey(textBox1.Text);

            button1_Click(null, null);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = (string)listBox1.SelectedItem;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // очистить текстовое поле с ключами
            listBox2.Items.Clear();

            // открыть родительскую ветку
            string keyname = "Software\\" + textBox1.Text;
            RegistryKey newkey = Registry.LocalMachine.OpenSubKey(keyname, true);

            // получить список значений отрытой ветки
            string[] names = newkey.GetValueNames();
            foreach (string str in names)
            {
                string str2 = str + "   " + newkey.GetValueKind(str).ToString() +"   " + newkey.GetValue(str);
                listBox2.Items.Add(str2);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // открыть родительскую ветку
            string keyname = "Software\\" + textBox1.Text;
            RegistryKey newkey = Registry.LocalMachine.OpenSubKey(keyname, true);

            // добавить значение в открытую ветку
            newkey.SetValue(textBox2.Text, textBox3.Text, RegistryValueKind.String);
        }
    }
}
