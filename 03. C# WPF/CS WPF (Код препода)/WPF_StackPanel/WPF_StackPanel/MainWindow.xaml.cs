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

namespace WPF_StackPanel
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // добавление элемента в innerStackPanel
            Button b = new Button();
            b.Content = "Hello!!!";
            innerStackPanel.Children.Add(b);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            // перебрать все дочерние элементы управления
            for (int i = 0; i < innerStackPanel.Children.Count; i++)
            {
                // получить текущий элемент
                var current = innerStackPanel.Children[i];

                // если это Border, то удалить его из innerStackPanel
                if (current is Border)
                    innerStackPanel.Children.Remove((Border)current);
            }
        }
    }
}
