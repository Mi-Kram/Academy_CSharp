using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ClassLibrary1
{
    public class Class1
    {
        public void print(string str)
        {
            MessageBox.Show(str);
        }

        public void SetBg(Form f)
        {
            f.BackColor = Color.Blue;
        }
    }

    public class SecondClass
    {
        public void print2(string str)
        {
            MessageBox.Show(str);
        }

        public void SetBg(Form f)
        {
            f.BackColor = Color.Red;
        }
    }
}
