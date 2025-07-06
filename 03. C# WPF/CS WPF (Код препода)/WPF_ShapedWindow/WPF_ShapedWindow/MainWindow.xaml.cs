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

namespace WPF_ShapedWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        bool flag = true;

        private void captionEllipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // организация перемещения окна
            DragMove();
        }

        private void ellipse3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void ellipse4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (flag)
            {
                this.WindowState = WindowState.Maximized;
                flag = false;
            }
            else
            {
                this.WindowState = WindowState.Normal;
                flag = true;
            }

        }
    }
}
