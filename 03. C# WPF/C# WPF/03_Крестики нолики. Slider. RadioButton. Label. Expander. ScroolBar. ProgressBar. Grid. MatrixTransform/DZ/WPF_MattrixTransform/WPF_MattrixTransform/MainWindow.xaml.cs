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

namespace WPF_MattrixTransform
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

        int angle = 0;

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            angle += 15;

            // Преобразование поворота
            // 1 - угол в градусах
            // 2, 3 - координаты точки, относительно которой происходит поворот
            RotateTransform rt = new RotateTransform(angle, 50, 20);
            button1.RenderTransform = rt;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            // Преобразование масштабирования
            // Параметры - коэффициенты масштабирования
            button2.RenderTransform = new ScaleTransform(2, 2);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            // Матричное преобразование
            Matrix m = new Matrix();
            m.RotateAt(45, 10, 10);
            m.Scale(0.5, 0.5);
            m.Translate(100, 0);
            MatrixTransform mt = new MatrixTransform(m);
            grid1.RenderTransform = mt;

            /*TransformGroup gr = new TransformGroup();
            RotateTransform rt = new RotateTransform(30, 50, 20);
            ScaleTransform st = new ScaleTransform(2, 2);
            gr.Children.Add(rt);
            gr.Children.Add(st);
            grid1.RenderTransform = gr;*/
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            // Преобразование сдвига (параметры - коэффициенты сдвига)
            button4.RenderTransform = new SkewTransform(10, 1);
        }
    }
}
