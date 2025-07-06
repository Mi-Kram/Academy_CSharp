using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rand;
        DispatcherTimer timer;

        List<Ellipse> ellipsesList;
        List<Rectangle> wallsList;

        public MainWindow()
        {
            InitializeComponent();

            ellipsesList = new List<Ellipse>();
            wallsList = new List<Rectangle>();

            rand = new Random();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += Timer_Tick;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < ellipsesList.Count; i++)
            {
                Ellipse currentEllipse = ellipsesList[i];
                ElemMove elemMove = currentEllipse.Tag as ElemMove;

                if (elemMove.X <= 0 || elemMove.X + currentEllipse.Width >= canvas.ActualWidth)
                    elemMove.StepX = -elemMove.StepX;

                if (elemMove.Y <= 0 || elemMove.Y + currentEllipse.Height >= canvas.ActualHeight)
                    elemMove.StepY = -elemMove.StepY;

                elemMove.X += elemMove.StepX;
                elemMove.Y += elemMove.StepY;



                Rect ellipseRect = new Rect(elemMove.X, elemMove.Y, currentEllipse.Width, currentEllipse.Height);


                foreach (Rectangle currentWall in wallsList)
                {
                    Rect wallRect = new Rect(Canvas.GetLeft(currentWall), Canvas.GetTop(currentWall), 
                        currentWall.Width, currentWall.Height);

                    if (wallRect.IntersectsWith(ellipseRect))
                    {
                        ellipsesList.Remove(currentEllipse);

                        i--;
                        canvas.Children.Remove(currentEllipse);
                    }
                }


                Canvas.SetLeft(currentEllipse, elemMove.X);
                Canvas.SetTop(currentEllipse, elemMove.Y);
            }
        }


        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double diag = rand.Next(50, 150);
                
                Point point = e.GetPosition(canvas);
                Size size = new Size(diag, diag);

                if(point.X+size.Width < canvas.ActualWidth &&
                    point.Y + size.Height < canvas.ActualHeight)
                    CreateEllipse(point, size);
            }
            else if(e.RightButton == MouseButtonState.Pressed)
            {
                CreateWall(e.GetPosition(canvas));
            }
        }


        void CreateEllipse(Point point, Size size)
        {
            Ellipse ellipse = new Ellipse();

            ellipse.Width = size.Width;
            ellipse.Height = size.Height;

            ellipse.Stroke = Brushes.Black;
            ellipse.StrokeThickness = 5;
            ellipse.Fill = Brushes.Blue;

            (double X, double Y) steps = stepCoords();
            ellipse.Tag = new ElemMove(point.X, point.Y, steps.X, steps.Y);

            Canvas.SetLeft(ellipse, point.X);
            Canvas.SetTop(ellipse, point.Y);

            canvas.Children.Add(ellipse);
            ellipsesList.Add(ellipse);

            if (ellipsesList.Count > 0 && !timer.IsEnabled) timer.Start();
        }
        void CreateWall(Point point)
        {
            Rectangle rectangle = new Rectangle();

            rectangle.Width = 60;
            rectangle.Height = 40;

            rectangle.Stroke = Brushes.Black;
            rectangle.StrokeThickness = 5;
            rectangle.Fill = Brushes.Orange;

            rectangle.MouseDown += Rectangle_MouseDown;

            Canvas.SetLeft(rectangle, point.X);
            Canvas.SetTop(rectangle, point.Y);

            canvas.Children.Add(rectangle);
            wallsList.Add(rectangle);
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            wallsList.Remove(sender as Rectangle);
            canvas.Children.Remove(sender as Rectangle);

            e.Handled = true;
        }

        (double, double) stepCoords()
        {
            (double X, double Y) steps = (rand.NextDouble(), rand.NextDouble());

            if (rand.Next(2) == 0) steps.X = -steps.X;
            if (rand.Next(2) == 0) steps.Y = -steps.Y;

            return steps;
        }
    }
}
