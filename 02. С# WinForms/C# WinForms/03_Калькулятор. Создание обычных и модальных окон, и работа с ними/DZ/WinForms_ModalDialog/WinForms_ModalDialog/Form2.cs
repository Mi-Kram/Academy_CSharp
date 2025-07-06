using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinForms_ModalDialog
{
    // Тип данных, ссылка на метод, принимающий 4 параметра
    public delegate void ColorChangedEvent(object sender, int r, int g, int b);

    public partial class Form2 : Form
    {
        // Ссылка на метод, принимающий 4 параметра
        public event ColorChangedEvent OnColorChanged;

        public Color CurrentColor {
            get => Color.FromArgb(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
        }

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(string title)
        {
            InitializeComponent();
            this.Text = title;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainWindow f = (MainWindow)this.Owner;
            f.BackColor = Color.FromArgb(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OnColorChanged?.Invoke(this, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
        }
    }

}