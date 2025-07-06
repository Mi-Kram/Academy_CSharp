using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinForms_Buttons
{
    public partial class Form1 : Form
    {
        Button button3;
        int number = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            //button1.Text = "X: " + e.X.ToString() + "  Y: " + e.Y.ToString();

            // если sender это кнопка
            if (sender is Button)
                ((Button)sender).Text = "X: " + e.X.ToString() + "  Y: " + e.Y.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (((RadioButton)groupBox1.Controls[3]).Checked == true)
            {
                button1.FlatStyle = FlatStyle.Flat;
                button1.FlatAppearance.BorderSize = 2;
                button1.FlatAppearance.BorderColor = Color.Black;
                button1.BackColor = Color.FromArgb(200, 150, 150);
                button1.ForeColor = Color.Gold;
            }
            if (((RadioButton)groupBox1.Controls[2]).Checked)
            {
                button1.FlatStyle = FlatStyle.Standard;
                button1.BackColor = button2.BackColor;
                button1.ForeColor = Color.Black;
            }
            if (((RadioButton)groupBox1.Controls[0]).Checked == true)
            {
                button1.FlatStyle = FlatStyle.System;
                button1.BackColor = button2.BackColor;
                button1.ForeColor = Color.Black;
            }
            if (((RadioButton)groupBox1.Controls[1]).Checked == true)
            {
                button1.FlatStyle = FlatStyle.Popup;
                button1.BackColor = button2.BackColor;
                button1.ForeColor = Color.Black;
            }
            button1.Cursor = Cursors.SizeAll;
            button2.Parent.BackColor = Color.LightGray;

            // перебрать все элементы в главной форме
            foreach (var item in this.Controls)
            {
                Console.WriteLine(item.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button3 = new Button();

            // добавление в родителя
            button3.Parent = this;
            //this.Controls.Add(button3);
            
            button3.Location = new Point(button2.Location.X, button2.Location.Y + 40);
            button3.Size = new Size(button2.Size.Width, button2.Size.Height);

            //button3.Width = 34; button3.Height = 23; button3.Left = 23; button3.Top = 34;

            button3.Text = "Новая кнопка";

            button3.Click += new_button_Click;
        }

        private void new_button_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Дочерних элементов в groupBox1: " + groupBox1.Controls.Count.ToString(), "Нажатие новой кнопки");

            foreach (var item in this.Controls)
            {
                if(item is Button)
                {
                    Console.WriteLine($"Button: {((Button)item).Text}");
                } else
                Console.WriteLine(item);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            button1.TextAlign = ContentAlignment.TopLeft;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            button1.TextAlign = ContentAlignment.TopCenter;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            button1.TextAlign = ContentAlignment.TopRight;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            button1.TextAlign = ContentAlignment.MiddleLeft;
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            button1.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            button1.TextAlign = ContentAlignment.BottomLeft;
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            button1.TextAlign = ContentAlignment.BottomCenter;
        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
            button1.TextAlign = ContentAlignment.BottomRight;
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            button1.TextAlign = ContentAlignment.MiddleRight;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (number == 3) number = 1;
            else number++;
            string name = $"num{number}.png";
            button4.Image = new Bitmap(name);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            button4.Enabled = !button4.Enabled;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            button4.Visible = !button4.Visible;
        }
    }
}