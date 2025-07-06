using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Main
{
    public partial class history : Window
    {
        bool close = false;
        public history()
        {
            InitializeComponent();
        }


        public void Add(string s)
        {
            listBox.Items.Add(s);
        }

        private void historyForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!close)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        public void CL()
        {
            close = true;
            historyForm_Closing(null, new System.ComponentModel.CancelEventArgs());
        }
    }
}
