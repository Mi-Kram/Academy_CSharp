using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Threading;

namespace WinForms_Controls
{
    public partial class Form1 : Form, IMessageFilter
    {


        public Form1()
        {
            InitializeComponent();

            // перехват ввода в любое текстовое поле
            Application.AddMessageFilter(this);
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 256)
            {
                Keys key = (Keys)(int)m.WParam;
                switch (key)
                {
                    case Keys.A:
                        MessageBox.Show("A");
                        break;
                    case Keys.B:
                        MessageBox.Show("B");
                        break;
                }
            }
            return false;   // true, если требуется "съесть" нажатия
        }


        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // добавление пункта в checkedListBox1
            checkedListBox1.Items.Add(textBox1.Text);
            toolStripProgressBar1.Value += 10;
            toolStripStatusLabel1.Text = "Количество строк в списке: " + checkedListBox1.Items.Count.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Удаление всех выделенных пунктов
            while (checkedListBox1.CheckedIndices.Count > 0)
            {
                // удление первого по счёту выделенного птичкой элемента
                checkedListBox1.Items.RemoveAt(checkedListBox1.CheckedIndices[0]);
            }
            toolStripStatusLabel1.Text = "Количество строк в списке: " + checkedListBox1.Items.Count.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Удаление выделенного элемента
            if (checkedListBox1.SelectedIndex >= 0)
                checkedListBox1.Items.RemoveAt(checkedListBox1.SelectedIndex);
            toolStripStatusLabel1.Text = "Количество строк в списке: " + checkedListBox1.Items.Count.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Копирование из checkedListBox1 в listBox1
            foreach (int index in checkedListBox1.CheckedIndices)
            {
                listBox1.Items.Add(checkedListBox1.Items[index]);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            toolStripStatusLabel1.Text = "Количество строк в списке: " + checkedListBox1.Items.Count.ToString();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) listBox1.Sorted = true;
            else listBox1.Sorted = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            foreach (int index in checkedListBox1.CheckedIndices)
            {
                //textBox2.Text += checkedListBox1.Items[index]+"\r\n";
                textBox2.AppendText(checkedListBox1.Items[index].ToString() + "\r\n");
            }
        }

        private void clearToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void getCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(listBox1.Items.Count.ToString());
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox3.Text);
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Insert(listBox1.SelectedIndex, textBox3.Text);
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            // копирование выделенного элемента выпадающего списка в текстовое поле
            if (listBox1.SelectedIndex != -1)
                textBox3.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel2.Text = "X: " + e.X.ToString() + " Y: " + e.Y.ToString();
        }

        private void clearToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            textBox2.Lines[0] = "hello";
            MessageBox.Show(textBox2.Lines[0]);
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Testing!!!");
            toolStripStatusLabel3.Text = "URRA!!!";
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                comboBox1.Items.Add(comboBox1.Text);
                comboBox1.Text = "";
            }
        }

        private void testToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("testing");
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            MessageBox.Show("Form key press");
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show("Form key down");
        }

        private void okToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Form key down");
        }

        private void testItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void submenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("sub item");
        }
    }
}