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

namespace WPF_CombineGeometry
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
            EllipseGeometry eg = new EllipseGeometry(new Rect(10, 10, 100, 100));
            RectangleGeometry rg = new RectangleGeometry(new Rect(30, 30, 200, 100));
            CombinedGeometry cg = new CombinedGeometry(GeometryCombineMode.Xor, eg, rg, new RotateTransform(30, 10, 10));

            Path myPath = new Path();

            // Задание цветов
            myPath.Stroke = Brushes.Black;
            myPath.Fill = Brushes.Beige;

            // Толщина контура
            myPath.StrokeThickness = 1;

            myPath.Data = cg;
            myPath.Opacity = 1;

            canvas1.Children.Add(myPath);
        }
    }
}
