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

namespace WPF_TextBlock
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // Разрешить перенос на следующую строку
            textBlock2.TextWrapping = TextWrapping.Wrap;
            textBlock2.Background = Brushes.AntiqueWhite;

            // выравнивание текста по центру
            textBlock2.TextAlignment = TextAlignment.Center;

            // Добавление жирного текста в блок текста
            textBlock2.Inlines.Add(new Bold(new Run("TextBlock")));

            // Добавление обычного текста в блок текста
            textBlock2.Inlines.Add(new Run(" is designed to be "));

            // Добавление перехода на следующую строку
            textBlock2.Inlines.Add(new LineBreak());

            // Добавление наклонного текста в блок текста
            textBlock2.Inlines.Add(new Bold(new Italic(new Run("lightweight"))));
            textBlock2.Inlines.Add(new Run(", and is geared specifically at integrating "));
            textBlock2.Inlines.Add(new Underline(new Run("small")));
            textBlock2.Inlines.Add(new Run(" portions of flow content into a UI."));

        }
    }
}
