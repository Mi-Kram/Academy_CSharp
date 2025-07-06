using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinForms_ModalDialog
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        // пользовательский конструктор
        public Form3(string title, Color bgColor)
        {
            InitializeComponent();

            this.Text = title;
            this.BackColor = bgColor;
        }
    }
}
