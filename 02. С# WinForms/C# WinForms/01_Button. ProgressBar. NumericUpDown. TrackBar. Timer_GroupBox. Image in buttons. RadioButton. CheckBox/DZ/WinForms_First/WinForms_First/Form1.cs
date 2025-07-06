using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_First
{
    public partial class Form1 : Form
    {
        int num = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("button1 pressed!");
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Text = $"X = {e.X}, Y = {e.Y}";
        }

        Random rand = new Random();

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //MessageBox.Show($"{e.Button} button down. X = {e.X}, Y = {e.Y}");

            Button button = new Button();

            button.Text = $"{num++}";

            button.Parent = this;

            button.Size = new Size(50, 40);
            button.Location = new Point(e.X, e.Y);
            button.BackColor = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
            button.ForeColor = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));

            button.Click += Button_Click;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            // при нажатии на кнопку написать в заголовке окна её надпись
            //if(sender is Button)
            //    this.Text = $"Button: {((Button)sender).Text}";

            this.Controls.Remove((Control)sender);
        }
    }
}
