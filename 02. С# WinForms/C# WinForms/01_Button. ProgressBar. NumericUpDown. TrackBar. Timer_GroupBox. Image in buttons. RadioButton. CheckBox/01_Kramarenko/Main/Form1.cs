using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Button picture = new Button();
                picture.FlatStyle = FlatStyle.Flat;
                picture.FlatAppearance.BorderSize = 0;


                //PictureBox picture = new PictureBox();


                picture.Parent = this;
                picture.Image = Image.FromStream(new FileStream("drop.png", FileMode.Open, FileAccess.Read));

                picture.Location = new Point(e.X, e.Y);
                picture.Size = new Size(50, 50);

                picture.Cursor = Cursor;
                if (!timer1.Enabled)
                {
                    Console.WriteLine("Timer enable");
                    timer1.Enabled = true;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (object item in Controls)
            {
                if (item is Button)
                {
                    if (Size.Height < (item as Button).Bottom + (item as Button).Height)
                    {
                        Controls.Remove(item as Control);
                        Console.WriteLine("Removed");

                        if (!IsElements())
                        {
                            timer1.Enabled = false;
                            Console.WriteLine("Timer disable");
                        }
                    }
                    else
                    {
                        (item as Button).Location = new Point((item as Button).Location.X, (item as Button).Location.Y + 5);
                    }
                }

                //if (item is PictureBox)
                //{
                //    if (Size.Height < (item as PictureBox).Bottom + (item as PictureBox).Height - 5)
                //    {
                //        Controls.Remove(item as Control);
                //        Console.WriteLine("Removed");
                //        if (!IsElements())
                //        {
                //            timer1.Enabled = false;
                //            Console.WriteLine("Timer disable");
                //        }
                //    }
                //    else
                //    {
                //        (item as PictureBox).Location = new Point((item as PictureBox).Location.X, (item as PictureBox).Location.Y + 5);
                //    }
                //}
            }
        }

        private bool IsElements()
        {
            foreach (object item in Controls)
            {
                if (item is Button) return true;
                //if (item is PictureBox) return true;
            }
            return false;
        }

    }
}
