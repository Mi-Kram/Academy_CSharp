using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class Form1 : Form
    {
        private class boolValue
        {
            public boolValue(bool v)
            {
                Value = v;
            }

            public bool Value { get; set; }
        }

        Dictionary<Button, boolValue> pictures = new Dictionary<Button, boolValue>();
        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureTimer_Tick(object sender, EventArgs e)
        {
            foreach (KeyValuePair<Button, boolValue> item in pictures)
            {
                item.Key.Location = new Point(item.Key.Location.X, item.Key.Location.Y + 
                    (item.Value.Value == true ? 10 : -10));

                if (item.Key.Location.Y < 0 || item.Key.Location.Y + item.Key.Size.Height >
                    item.Key.Parent.Size.Height - (item.Key.Parent == this?40:0))
                {
                    item.Value.Value = !item.Value.Value;
                    Console.WriteLine("Change direction");
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            pictures.Remove(sender as Button);
            (sender as Button).Parent.Controls.Remove(sender as Button);
            Console.WriteLine("Removed");

            if (pictures.Count() == 0)
            {
                pictureTimer.Enabled = false;
                Console.WriteLine("Timer stoped");
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Bitmap bitmap = new Bitmap("picture.png");
                if (Size.Height - 40 - bitmap.Height < 0) return; 
                if (e.X + bitmap.Width > Size.Width) return; 

                Button button = new Button();

                button.Parent = this;
                button.Image = bitmap;

                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.BackColor = button.Parent.BackColor;

                button.Size = new Size(button.Image.Width, button.Image.Height);

                int y = button.Parent.Size.Height - (button.Parent == this?40:0) - button.Size.Height;
                if (e.Y <= y) y = e.Y-1;
                if (y < 0) y = 0;

                button.Location = new Point(e.X, y);

                button.Click += Button_Click;

                pictures[button] = new boolValue(Convert.ToBoolean(rand.Next(0, 2)));
                if (!pictureTimer.Enabled)
                {
                    pictureTimer.Enabled = true;
                    Console.WriteLine("Timer started");
                }

                Console.WriteLine("Create");
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if(pictures.Count() != 0)
            {
                List<Button> lst = new List<Button>();
                foreach (KeyValuePair<Button, boolValue> item in pictures)
                {
                    if (item.Key.Location.X + item.Key.Size.Width > Size.Width)
                        lst.Add(item.Key);
                }
                foreach (Button item in lst)
                {
                    pictures.Remove(item);
                    Controls.Remove(item);
                    Console.WriteLine("Removed");
                }

                if (pictures.Count() == 0)
                {
                    pictureTimer.Enabled = false;
                    Console.WriteLine("Timer stoped");
                    return;
                }
                if (Size.Height - 40 - pictures.First().Key.Size.Height < 0)
                {
                    if (pictureTimer.Enabled)
                    {
                        pictureTimer.Enabled = false;
                        Console.WriteLine("Timer stoped");
                    }
                    return;
                }

                if (!pictureTimer.Enabled)
                {
                    pictureTimer.Enabled = true;
                    Console.WriteLine("Timer started");
                }
                foreach (KeyValuePair<Button, boolValue> item in pictures)
                {
                    if (item.Key.Location.Y + item.Key.Size.Height > Size.Height - 40)
                    {
                        item.Key.Location = new Point(item.Key.Location.X,
                            Size.Height - 40 - item.Key.Size.Height);
                    }
                }

            }
        }
    }
}
