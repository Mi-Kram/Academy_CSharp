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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Animation_KeyFrames
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

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            // Простая анимация типа double
            DoubleAnimation dx = new DoubleAnimation(10, 200, TimeSpan.FromMilliseconds(8000));

            // Анимация с использованием фреймов (кадров)
            DoubleAnimationUsingKeyFrames dy1 = new DoubleAnimationUsingKeyFrames();

            // Задание длительности анимации
            dy1.Duration = TimeSpan.FromSeconds(8);

            // Задание линейных кадров (значение, время)
            dy1.KeyFrames.Add(new LinearDoubleKeyFrame(20, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            dy1.KeyFrames.Add(new LinearDoubleKeyFrame(100, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2))));
            dy1.KeyFrames.Add(new LinearDoubleKeyFrame(50, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(6000))));
            dy1.KeyFrames.Add(new LinearDoubleKeyFrame(200, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(3000))));

            // Запуск анимаций
            image1.BeginAnimation(Canvas.LeftProperty, dx);
            image1.BeginAnimation(Canvas.TopProperty, dy1);
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation dx1 = new DoubleAnimation(10, 100, TimeSpan.FromSeconds(2));
            dx1.Duration = TimeSpan.FromSeconds(2);

            DoubleAnimationUsingKeyFrames dy1 = new DoubleAnimationUsingKeyFrames();

            dy1.Duration = TimeSpan.FromSeconds(2);

            dy1.KeyFrames.Add(new DiscreteDoubleKeyFrame(100, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            dy1.KeyFrames.Add(new DiscreteDoubleKeyFrame(50, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.9))));
            dy1.KeyFrames.Add(new DiscreteDoubleKeyFrame(200, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.5))));

            image1.BeginAnimation(Canvas.LeftProperty, dx1);
            image1.BeginAnimation(Canvas.TopProperty, dy1);
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation dx1 = new DoubleAnimation(10, 300, TimeSpan.FromSeconds(2));
            dx1.Duration = TimeSpan.FromSeconds(2);

            // Задание поведения по окончании анимации
            dx1.FillBehavior = FillBehavior.HoldEnd;

            DoubleAnimationUsingKeyFrames dy1 = new DoubleAnimationUsingKeyFrames();

            // сохранить значение свойства после окончания анимации
            dy1.FillBehavior = FillBehavior.HoldEnd;
            dy1.Duration = TimeSpan.FromSeconds(2);

            dy1.KeyFrames.Add(new SplineDoubleKeyFrame(30, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)), new KeySpline(0.9, 0.1, 0.1, 0.9)));
            dy1.KeyFrames.Add(new SplineDoubleKeyFrame(300, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2)), new KeySpline(0.9, 0.1, 0.1, 0.9)));

            image1.BeginAnimation(Canvas.LeftProperty, dx1);
            image1.BeginAnimation(Canvas.TopProperty, dy1);
        }
    }
}
