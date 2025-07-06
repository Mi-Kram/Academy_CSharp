using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_Dialogs
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>

    // тип данных для передачи цвета из окна
    public delegate void ColorDelegate(byte R, byte G, byte B);

    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        // event для передачи цвета из окна
        public event ColorDelegate ChangeColor;

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeColor?.Invoke(Convert.ToByte(textBox1.Text), Convert.ToByte(textBox2.Text), Convert.ToByte(textBox3.Text));
            }
            catch
            {

            }
        }
    }
}
