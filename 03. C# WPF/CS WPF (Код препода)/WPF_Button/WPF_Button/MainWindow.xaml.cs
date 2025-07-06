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

namespace WPF_Button
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
            // Создание сплошной кисти
            SolidColorBrush brush2 = new SolidColorBrush(Colors.Red);
            button2.Background = brush2;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            // Кисть для линейного градиента (по прямой)
            LinearGradientBrush brush1 = new LinearGradientBrush(Colors.White, Colors.Black, 45);

            // Очистка старых градиентных цветов
            brush1.GradientStops.Clear();

            // Добавление новых градиентных цветов
            GradientStop stop1 = new GradientStop(Colors.Blue, 0);
            GradientStop stop2 = new GradientStop(Colors.Red, 0.5);
            GradientStop stop3 = new GradientStop(Colors.Pink, 1);

            brush1.GradientStops.Add(stop1);
            brush1.GradientStops.Add(stop2);
            brush1.GradientStops.Add(stop3);

            // Применение кисти
            button1.Background = brush1;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            listBox1.Items.Add(textBox1.Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void listBox1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // получение координат мышы относитено заданного элемента управления
            Point pos = e.GetPosition(listBox1);
            listBox1.Items.Add(pos);
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            Point pos = e.GetPosition(this);
            Title = pos.ToString();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            // Радиальная кисть (по кругу)
            /*RadialGradientBrush brush;

            // Указание цветов для градиента
            brush = new RadialGradientBrush(Colors.White, Colors.Red);

            // Радиусы градиента относительно размеров listBox1
            brush.RadiusX = 0.2;
            brush.RadiusY = 0.2;

            // Метод распространения градиента
            brush.SpreadMethod = GradientSpreadMethod.Pad;

            // Применение кисти
            listBox1.Background = brush;*/

            // https://docs.microsoft.com/ru-ru/dotnet/api/system.windows.media.tilebrush.viewport?view=netframework-4.7.2#System_Windows_Media_TileBrush_Viewport

            // Кисть на картинки (текстурная кисть)
            /*ImageBrush imgBrush = new ImageBrush();

            // Указание источника картинки
            imgBrush.ImageSource =
                new BitmapImage (
                    // 1 - параметр - путь к изображению, 2 - тип пути
                    new Uri(@"kopipi2.jpg", UriKind.Relative)
                );

            button4.Background = imgBrush;*/

            // Указание цветов для градиента
            RadialGradientBrush brush = new RadialGradientBrush();

            // Радиусы градиента относительно размеров listBox1
            brush.RadiusX = 0.2;
            brush.RadiusY = 0.2;

            Random random = new Random();

            for (int i = 0; i < 5; i++)
            {
                brush.GradientStops.Add(
                    new GradientStop(Color.FromRgb(
                        (byte)random.Next(256),
                        (byte)random.Next(256),
                        (byte)random.Next(256)),
                    1f / i));
            }

            // Метод распространения градиента
            brush.SpreadMethod = GradientSpreadMethod.Reflect;

            // Применение кисти
            listBox1.Background = brush;
        }

        private void W1_ChangeColor(byte R, byte G, byte B)
        {
            SolidColorBrush brush2 = new SolidColorBrush(Color.FromRgb(R, G, B));
            grid1.Background = brush2;
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(listBox1.Items[listBox1.SelectedIndex].ToString());
        }
    }
}
