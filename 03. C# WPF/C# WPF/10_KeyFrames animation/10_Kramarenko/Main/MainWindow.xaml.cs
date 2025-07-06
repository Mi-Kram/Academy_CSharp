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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<EllipseInfo> ellipseLst;
        public MainWindow()
        {
            InitializeComponent();

            //planeImage.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Resources\\plane.png", UriKind.Absolute));

            ellipseLst = new List<EllipseInfo>();
        }

        Point GetMiddlePoint(Point startPoint, Size size) => new Point(startPoint.X + size.Width / 2, startPoint.Y + size.Height / 2);
        Point GetStartPoint(Point middlePoint, Size size) => new Point(middlePoint.X - size.Width / 2, middlePoint.Y - size.Height / 2);
        
        (DoubleAnimationUsingKeyFrames, DoubleAnimationUsingKeyFrames) GetAnimation()
        {
            DoubleAnimationUsingKeyFrames dx = new DoubleAnimationUsingKeyFrames();
            DoubleAnimationUsingKeyFrames dy = new DoubleAnimationUsingKeyFrames();


            return (dx, dy);
        }

        double GetTime(Point point)
        {
            double max = Math.Max(Math.Abs(point.X - Canvas.GetLeft(planeImage)),
                                  Math.Abs(point.Y - Canvas.GetTop(planeImage)));

            return max / 200;
        }
        double GetTime(Point point1, Point point2)
        {
            double max = Math.Max(Math.Abs(point1.X - point2.X),
                                  Math.Abs(point1.Y - point2.Y));

            return max / 200;
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (startButton.IsEnabled)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Height = 20;
                ellipse.Width = 20;
                ellipse.Fill = Brushes.Blue;
                ellipse.MouseDown += Ellipse_MouseDown;

                Point middlePoint = e.GetPosition(canvas);
                Point startPoint = GetStartPoint(middlePoint, new Size(20, 20));

                Canvas.SetLeft(ellipse, startPoint.X);
                Canvas.SetTop(ellipse, startPoint.Y);
                Canvas.SetZIndex(ellipse, 0);

                canvas.Children.Add(ellipse);
                ellipseLst.Add(new EllipseInfo(ellipse, middlePoint));
            }
        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            canvas.Children.Remove(sender as Ellipse);

            for (int i = 0; i < ellipseLst.Count; i++)
            {
                if(ellipseLst[i].Ellipse == sender as Ellipse)
                {
                    ellipseLst.RemoveAt(i);
                    break;
                }
            }

            e.Handled = true;
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            StartFly();

            #region t

            //if (ellipseLst.Count > 0)
            //{
            //    DoubleAnimationUsingKeyFrames dx = new DoubleAnimationUsingKeyFrames();
            //    DoubleAnimationUsingKeyFrames dy = new DoubleAnimationUsingKeyFrames();

            //    Size plS = new Size(planeImage.ActualWidth, planeImage.ActualHeight);
            //    Point curPoint = GetMiddlePoint(new Point(planeImage.ActualWidth, planeImage.ActualHeight), plS);
            //    Point startP = GetStartPoint(curPoint, plS);
            //    double allTime = 0, time = 0;

            //    dx.KeyFrames.Add(new LinearDoubleKeyFrame(startP.X, TimeSpan.FromSeconds(time)));
            //    dy.KeyFrames.Add(new LinearDoubleKeyFrame(startP.Y, TimeSpan.FromSeconds(time)));

            //    for (int i = 0; i < ellipseLst.Count; i++)
            //    {
            //        time = GetTime(curPoint, ellipseLst[i].Point);

            //        startP = GetStartPoint(ellipseLst[i].Point, plS);
            //        dx.KeyFrames.Add(new LinearDoubleKeyFrame(startP.X, TimeSpan.FromSeconds(time)));
            //        dy.KeyFrames.Add(new LinearDoubleKeyFrame(startP.Y, TimeSpan.FromSeconds(time)));

            //        allTime += time;
            //        curPoint = ellipseLst[i].Point;
            //    }

            //    startP = GetStartPoint(new Point(canvas.ActualWidth / 2, canvas.ActualHeight / 2), plS);
            //    time = GetTime(curPoint, startP);

            //    dx.KeyFrames.Add(new LinearDoubleKeyFrame(startP.X, TimeSpan.FromSeconds(time)));
            //    dy.KeyFrames.Add(new LinearDoubleKeyFrame(startP.Y, TimeSpan.FromSeconds(time)));

            //    allTime += time;

            //    dx.Duration = TimeSpan.FromSeconds(allTime);
            //    dy.Duration = TimeSpan.FromSeconds(allTime);

            //    planeImage.BeginAnimation(Canvas.LeftProperty, dx);
            //    planeImage.BeginAnimation(Canvas.TopProperty, dy);
            //}

            #endregion
        }
        void StartFly()
        {
            double time;
            Point planePoint;
            DoubleAnimation dx, dy;

            if (ellipseLst.Count > 0)
            {
                startButton.IsEnabled = false;

                EllipseInfo info = ellipseLst[0];
                time = GetTime(info.Point);
                planePoint = GetStartPoint(info.Point, new Size(planeImage.ActualWidth, planeImage.ActualHeight));

                dx = new DoubleAnimation(planePoint.X, TimeSpan.FromSeconds(time));
                dy = new DoubleAnimation(planePoint.Y, TimeSpan.FromSeconds(time));

                dx.AutoReverse = false;
                dy.AutoReverse = false;

                dx.Completed += Dx_Completed;

                // GetAngle and rotate

                planeImage.BeginAnimation(Canvas.LeftProperty, dx);
                planeImage.BeginAnimation(Canvas.TopProperty, dy);
            }
            else if(!startButton.IsEnabled)
            {
                Point middlePoint = new Point(canvas.ActualWidth / 2, canvas.ActualHeight / 2);
                planePoint = GetStartPoint(middlePoint, new Size(planeImage.ActualWidth, planeImage.ActualHeight));

                time = GetTime(middlePoint);

                dx = new DoubleAnimation(planePoint.X, TimeSpan.FromSeconds(time));
                dy = new DoubleAnimation(planePoint.Y, TimeSpan.FromSeconds(time));

                dx.AutoReverse = false;
                dy.AutoReverse = false;

                planeImage.BeginAnimation(Canvas.LeftProperty, dx);
                planeImage.BeginAnimation(Canvas.TopProperty, dy);

                startButton.IsEnabled = true;
            }
        }
        double GetAngle(Point point)
        {
            Point planePoint = GetMiddlePoint(
                new Point(Canvas.GetLeft(planeImage), Canvas.GetTop(planeImage)), 
                new Size(planeImage.ActualWidth, planeImage.ActualHeight));

            return Math.Atan(Math.Abs(point.X - planePoint.X) / Math.Abs(point.Y - planePoint.Y));
        }

        private void Dx_Completed(object sender, EventArgs e)
        {
            canvas.Children.Remove(ellipseLst[0].Ellipse);
            ellipseLst.RemoveAt(0);
            StartFly();
        }
    }

    class EllipseInfo
    {
        public Ellipse Ellipse{ get; set; }
        public Point Point{ get; set; }

        public EllipseInfo(Ellipse ellipse, Point point)
        {
            Ellipse = ellipse;
            Point = point;
        }

    }
}
