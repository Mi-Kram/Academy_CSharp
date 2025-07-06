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
using System.Timers;

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        (DateTime startTime, FrameworkElement elem) removePath;
        Random rand;

        FrameworkElement movingElement;
        Point elementCoords;

        public MainWindow()
        {
            InitializeComponent();

            removePath.startTime = new DateTime();
            removePath.elem = null;

            movingElement = null;
            rand = new Random();
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(canvas);
            Title = $"{point.X}   {point.Y}";

            switch (rand.Next(2))
            {
                case 0: CreateDiamond(point); break;
                case 1: CreateHeart(point); break;
                default: break;
            }
        }

        private void CreateDiamond(Point point)
        {
            double x = point.X, y = point.Y;

            PathFigure myPathFigure = new PathFigure();

            myPathFigure.StartPoint = new Point(0,0);

            QuadraticBezierSegment Segment1 = new QuadraticBezierSegment(
                new Point(-10, -35),
                new Point(-30, -50), true);

            QuadraticBezierSegment Segment2 = new QuadraticBezierSegment(
                new Point(-10, -65),
                new Point(0, -100), true);

            QuadraticBezierSegment Segment3 = new QuadraticBezierSegment(
                new Point(10, -65),
                new Point(30, -50), true);

            QuadraticBezierSegment Segment4 = new QuadraticBezierSegment(
                new Point(10, -35),
                new Point(0,0), true);


            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(Segment1);
            myPathSegmentCollection.Add(Segment2);
            myPathSegmentCollection.Add(Segment3);
            myPathSegmentCollection.Add(Segment4);

            // Добавление коллекции сегметов в фигуру
            myPathFigure.Segments = myPathSegmentCollection;

            // Создание коллекции фигур контура, в которую можно добавлять любое количество фигур
            PathFigureCollection myPathFigureCollection = new PathFigureCollection();
            myPathFigureCollection.Add(myPathFigure);

            // Создание геометрии контура на основе коллекции фигур контура
            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures = myPathFigureCollection;

            // Создание контура с указанной геометрией контура
            Path myPath = new Path();
            myPath.Stroke = Brushes.Blue;
            myPath.Fill = Brushes.Blue;
            myPath.StrokeThickness = 3;
            myPath.StrokeStartLineCap = PenLineCap.Round;
            myPath.StrokeEndLineCap = PenLineCap.Round;
            myPath.StrokeDashCap = PenLineCap.Round;
            myPath.Data = myPathGeometry;

            myPath.MouseDown += MyPath_MouseDown;

            Canvas.SetLeft(myPath, x);
            Canvas.SetTop(myPath, y);

            canvas.Children.Add(myPath);
        }
        private void CreateHeart(Point point)
        {
            double x = point.X, y = point.Y;

            PathFigure myPathFigure = new PathFigure();
            PathFigure myPathFigure2 = new PathFigure();

            myPathFigure.StartPoint = new Point(0,0);
            myPathFigure2.StartPoint = new Point(0,0);

            LineSegment myLineSegment = new LineSegment();
            myLineSegment.Point = new Point(200, 70);

            BezierSegment BSegment1 = new BezierSegment(
                new Point(-40, -50), new Point(-50, -110),
                new Point(0, -80), true);

            BezierSegment BSegment2 = new BezierSegment(
                new Point(40, -50), new Point(50, -110),
                new Point(0, -80), true);

            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(BSegment1);

            PathSegmentCollection myPathSegmentCollection2 = new PathSegmentCollection();
            myPathSegmentCollection2.Add(BSegment2);

            myPathFigure.Segments = myPathSegmentCollection;
            myPathFigure2.Segments = myPathSegmentCollection2;

            PathFigureCollection myPathFigureCollection = new PathFigureCollection();
            myPathFigureCollection.Add(myPathFigure);
            myPathFigureCollection.Add(myPathFigure2);

            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures = myPathFigureCollection;

            Path myPath = new Path();
            myPath.Stroke = Brushes.Red;
            myPath.Fill = Brushes.Red;
            myPath.StrokeThickness = 3;
            myPath.StrokeStartLineCap = PenLineCap.Round;
            myPath.Data = myPathGeometry;
            myPath.MouseDown += MyPath_MouseDown;

            Canvas.SetLeft(myPath, x);
            Canvas.SetTop(myPath, y);

            canvas.Children.Add(myPath);
        }

        private void MyPath_MouseDown(object sender, MouseButtonEventArgs e)
        {
            movingElement = (FrameworkElement)sender;
            elementCoords = e.GetPosition(movingElement);
            Canvas.SetZIndex(movingElement, 10);

            if (removePath.elem != null)
            {
                DateTime endTime = DateTime.Now;
                TimeSpan timeSpan = endTime - removePath.startTime;

                if (timeSpan.TotalMilliseconds < 500 &&
                    (FrameworkElement)sender == removePath.elem)
                {
                    removePath.elem = null;
                    canvas.Children.Remove((FrameworkElement)sender);
                }
                else
                {
                    removePath.startTime = DateTime.Now;
                    removePath.elem = (FrameworkElement)sender;
                }
            }
            else
            {
                removePath.startTime = DateTime.Now;
                removePath.elem = sender as FrameworkElement;
            }

            e.Handled = true;
        }

        private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (movingElement != null)
            {
                Canvas.SetZIndex(movingElement, 0);
                movingElement = null;
            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (movingElement != null)
            {
                Point coords = e.GetPosition(canvas);

                Canvas.SetLeft(movingElement, coords.X - elementCoords.X);
                Canvas.SetTop(movingElement, coords.Y - elementCoords.Y);
            }
        }
    }
}
