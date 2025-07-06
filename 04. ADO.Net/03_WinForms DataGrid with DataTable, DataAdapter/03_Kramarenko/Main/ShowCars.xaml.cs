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
    /// <summary>
    /// Логика взаимодействия для ShowCars.xaml
    /// </summary>
    public partial class ShowCars : Window
    {
        public string Own { get; set; }

        public List<string> Cars { get; set; }

        public ShowCars()
        {
            InitializeComponent();

            Own = "";
            Cars = new List<string>();
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OwnTxtBlock.Text = Own;

            foreach (string car in Cars)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.FontSize = 20;
                textBlock.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
                textBlock.Padding = new Thickness(10, 0, 10, 0);
                textBlock.Margin = new Thickness(0, 5, 0, 0);
                textBlock.Text = car;
                stackPanel.Children.Add(textBlock);
            }
        }
    }
}
