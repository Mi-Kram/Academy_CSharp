using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MaxPosition.Text = $"Max position: X = {Size.Width - 20 - picture.Size.Width}; Y = {Size.Height - 40 - picture.Size.Height};";
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.X + picture.Size.Width > Size.Width-20 || e.Y + picture.Size.Height > Size.Height-40)
            {
                MessageBox.Show("Действие не будет выполнено.\nКартинка вылазит за границы окна.", 
                    "INFO", MessageBoxButtons.OK);
                return;
            }

            int n = 10;

            int xStep = (e.X - picture.Location.X) / n;
            int yStep = (e.Y - picture.Location.Y) / n;

            if (xStep != 0 || yStep != 0)
            {
                for (int i = 0; i < n; i++)
                {
                    picture.Location = new Point(
                        picture.Location.X + xStep,
                        picture.Location.Y + yStep);

                    Thread.Sleep(100);
                }
            }

            picture.Location = new Point(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            CursorPosition.Text = $"Cursot position: X = {e.X}; Y = {e.Y};";
        }

        private void picture_MouseMove(object sender, MouseEventArgs e)
        {
            CursorPosition.Text = $"Cursot position: X = {picture.Location.X + e.X}; Y = {picture.Location.Y + e.Y};";
        }

        private void statusStrip1_MouseMove(object sender, MouseEventArgs e)
        {
            CursorPosition.Text = "Cursot position: X = n; Y = n;";
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            MaxPosition.Text = $"Max position: X = {Size.Width-20-picture.Size.Width}; Y = {Size.Height-40-picture.Size.Height};";
        }

        private void statusStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Информация о координатах курсора.", "INFO", MessageBoxButtons.OK);
        }

        private void CursorPosition_MouseDown(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Текущие координаты курсора.", "INFO", MessageBoxButtons.OK);
        }

        private void MaxPosition_MouseDown(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Максимальные координаты курсора.", "INFO", MessageBoxButtons.OK);
        }
    }
}
