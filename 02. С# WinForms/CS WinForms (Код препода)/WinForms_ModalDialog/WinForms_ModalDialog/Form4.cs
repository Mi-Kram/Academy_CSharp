using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_ModalDialog
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        public Form4(string title, Color backColor, int width, int height)
        {
            InitializeComponent();

            Text = title;
            BackColor = backColor;
            Width = width;
            Height = height;
        }
    }
}
