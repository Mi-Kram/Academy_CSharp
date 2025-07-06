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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_User_Control
{
    /// <summary>
    /// Interaction logic for UserTextControl.xaml
    /// </summary>
    public partial class UserTextControl : UserControl
    {
        public string Title { get; set; }

        public int MaxLength { get; set; }

        public UserTextControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
