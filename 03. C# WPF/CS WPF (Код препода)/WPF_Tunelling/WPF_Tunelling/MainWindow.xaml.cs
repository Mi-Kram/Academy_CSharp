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

namespace WPF_Tunelling
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

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            listBox1.Items.Add("Inner button click");
            e.Handled = true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            listBox1.Items.Add("Outer button click");
        }

        private void button1_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            listBox1.Items.Add("Outer preview mouse right button down");
            //e.Handled = true;
        }

        private void button2_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            listBox1.Items.Add("Inner preview mouse right button down");
        }

        private void button1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            listBox1.Items.Add("Outer mouse right button down");
        }

        private void button2_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            listBox1.Items.Add("Inner mouse right button down");

            // Остановить прохождение сообщения для остальных адресатов
            e.Handled = true;
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            listBox1.Items.Add("Window mouse right button down");
        }

        private void Window_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            listBox1.Items.Add("Window preview mouse right button down");
            //e.Handled = true;
        }
    }
}
