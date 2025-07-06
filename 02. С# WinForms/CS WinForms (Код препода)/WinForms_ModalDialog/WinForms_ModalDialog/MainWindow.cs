using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinForms_ModalDialog
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2("Hello world");
            form2.BackColor = Color.Gray;
            form2.Owner = this;

            form2.OnColorChanged += Form2_OnColorChanged;

            DialogResult res = form2.ShowDialog();
            if (res == DialogResult.OK)
            {
                MessageBox.Show(form2.CurrentColor.ToString());
            }
        }

        private void Form2_OnColorChanged(object sender, Color currentColor)
        {
            try
            {
                this.BackColor = currentColor;
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.BackColor = Color.Gray;
            form2.OnColorChanged += new ColorChangedEvent(Form2_OnColorChanged);
            form2.Show(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3("New dialog!!!", Color.Beige);

            // показать модальное окно
            DialogResult result = form3.ShowDialog();

            if(result == DialogResult.OK)
            {
                MessageBox.Show("OK!!!");
            }
            else
            {
                MessageBox.Show("Cancel!!!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4("My COOL window!!!", Color.WhiteSmoke, 640, 480);

            // показать модальное окно
            DialogResult result = form4.ShowDialog();

            if (result == DialogResult.OK)
            {
                MessageBox.Show("OK!!!");
            }
            else
            {
                MessageBox.Show("Cancel!!!");
            }
        }
    }
}