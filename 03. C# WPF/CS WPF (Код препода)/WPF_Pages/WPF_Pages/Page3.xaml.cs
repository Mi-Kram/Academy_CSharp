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

namespace WPF_Pages
{
    /// <summary>
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
        }

        public Page3(string str)
        {
            InitializeComponent();
            TextBox1.Text = str;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // программный переход на другую страницу
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);
            nav.GoBack();
        }
    }
}
