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

namespace WPF_Animation_EasingFunc
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
            DoubleAnimation leftAnimation = new DoubleAnimation();
            leftAnimation.From = 100;
            leftAnimation.To = 600;
            leftAnimation.Duration = TimeSpan.FromSeconds(5);

            //rect1.BeginAnimation(Canvas.LeftProperty, leftAnimation);

            BackEase ease = new BackEase();
            ease.EasingMode = EasingMode.EaseInOut;
            ease.Amplitude = 1.3;

            DoubleAnimation topAnimation = new DoubleAnimation();
            topAnimation.From = 40;
            topAnimation.To = 400;
            topAnimation.EasingFunction = ease;
            topAnimation.Duration = TimeSpan.FromSeconds(3);

            rect1.BeginAnimation(Rectangle.HeightProperty, topAnimation);
        }

        Random rand = new Random();
        SolidColorBrush pointSolidBrush = null;

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            pointSolidBrush = new SolidColorBrush(Color.FromRgb((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256)));

            // анимировать свойство Top из XAML
            DoubleAnimation da = (DoubleAnimation)storyboard1.Children[1];

            EasingMode easingMode = EasingMode.EaseIn;

            switch (((ListBoxItem)easingModeChooser.SelectedItem).Content)
            {
                case "EaseIn":
                    easingMode = EasingMode.EaseIn;
                    break;

                case "EaseOut":
                    easingMode = EasingMode.EaseOut;
                    break;

                case "EaseInOut":
                    easingMode = EasingMode.EaseInOut;
                    break;
                default:
                    break;
            }

            EasingFunctionBase easingFunction = new BackEase();
            easingFunction.EasingMode = easingMode;

            switch (((ListBoxItem)easeChooser.SelectedItem).Content)
            {
                case "BackEase":
                    easingFunction = new BackEase();

                    // амплитуда (сила) смещения назад
                    ((BackEase)easingFunction).Amplitude = 1.1;
                    break;

                case "ElasticEase":
                    easingFunction = new ElasticEase();
                    ((ElasticEase)easingFunction).Springiness = 1;
                    ((ElasticEase)easingFunction).Oscillations = 3;
                    break;

                case "SineEase":
                    easingFunction = new SineEase();
                    break;

                case "BounceEase":
                    easingFunction = new BounceEase();
                    // количество отскоков
                    ((BounceEase)easingFunction).Bounces = 3;

                    // амплитуда отскока
                    ((BounceEase)easingFunction).Bounciness = 2;
                    break;

                case "QuadraticEase":
                    easingFunction = new QuadraticEase();
                    break;

                case "CubicEase":
                    easingFunction = new CubicEase();
                    break;

                case "QuarticEase":
                    easingFunction = new QuarticEase();
                    break;

                case "QuinticEase":
                    easingFunction = new QuinticEase();
                    break;

                case "PowerEase":
                    easingFunction = new PowerEase();
                    ((PowerEase)easingFunction).Power = 10;
                    break;

                case "ExponentialEase":
                    easingFunction = new ExponentialEase();
                    ((ExponentialEase)easingFunction).Exponent = 7;
                    break;

                case "CircleEase":
                    easingFunction = new CircleEase();
                    break;

                default:
                    break;
            }

            easingFunction.EasingMode = easingMode;
            da.EasingFunction = easingFunction;

            // сообщение приходит, при каждом шаге анимации
            storyboard1.CurrentTimeInvalidated += Storyboard1_CurrentTimeInvalidated;

            storyboard1.Begin(this);
        }

        private void Storyboard1_CurrentTimeInvalidated(object sender, EventArgs e)
        {
            double x = Canvas.GetLeft(rect1);
            double y = Canvas.GetTop(rect1);

            //Console.WriteLine(@"x = {0}, y = {1}", x, y);

            Ellipse point = new Ellipse();
            point.StrokeThickness = 0;

            point.Fill = pointSolidBrush;
            point.Width = 3;
            point.Height = 3;
            Canvas.SetLeft(point, x);
            Canvas.SetTop(point, y);
            canvas1.Children.Add(point);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int index = canvas1.Children.Count - 1;
            while (index >= 0)
            {
                var item = canvas1.Children[index--];
                if (item is Ellipse && ((Ellipse)item).Width < 10)
                    canvas1.Children.Remove(item);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DoubleAnimationUsingKeyFrames dx1 = new DoubleAnimationUsingKeyFrames();

            // Задание длительности анимации
            dx1.Duration = TimeSpan.FromSeconds(8);

            // Задание линейных кадров (значение, время)
            dx1.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            dx1.KeyFrames.Add(new LinearDoubleKeyFrame(600, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(4))));
            dx1.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(8000))));

            // Запуск анимации
            rect2.BeginAnimation(Canvas.LeftProperty, dx1);

            // Анимация с использованием фреймов (кадров)
            DoubleAnimationUsingKeyFrames dy1 = new DoubleAnimationUsingKeyFrames();

            // Задание длительности анимации
            dy1.Duration = TimeSpan.FromSeconds(8);

            // Задание линейных кадров (значение, время)
            dy1.KeyFrames.Add(new LinearDoubleKeyFrame(300, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            dy1.KeyFrames.Add(new SplineDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2)), new KeySpline(0, 1, 1, 1)));

            //dy1.KeyFrames.Add(new SplineDoubleKeyFrame(300, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(4)), new KeySpline(1, 0, 1, 1)));

            ElasticEase elasticEase = new ElasticEase();
            elasticEase.Springiness = 1;
            elasticEase.Oscillations = 5;
            elasticEase.EasingMode = EasingMode.EaseIn;

            dy1.KeyFrames.Add(new EasingDoubleKeyFrame(300, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(4)), elasticEase));

            dy1.KeyFrames.Add(new SplineDoubleKeyFrame(600, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(6)), new KeySpline(0, 1, 1, 1)));
            dy1.KeyFrames.Add(new SplineDoubleKeyFrame(300, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(8)), new KeySpline(1, 0, 1, 1)));

            // Запуск анимации
            rect2.BeginAnimation(Canvas.TopProperty, dy1);
        }
    }
}
