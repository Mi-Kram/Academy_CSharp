using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public delegate void ReplaceSubStrings(string find, string replace);
    
    public partial class FormReplace : Form
    {
        public event ReplaceSubStrings replaceSubStringsEvent;

        public FormReplace()
        {
            InitializeComponent();
        }

        private void replaceButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.X > 0 && e.X < replaceButton.Width &&
                e.Y > 0 && e.Y < replaceButton.Height)
            {
                replaceSubStringsEvent?.Invoke(findTextBox.Text, replaceTextBox.Text);
            }
        }
    }
}
